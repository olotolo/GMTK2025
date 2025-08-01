using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerStateMachine : MonoBehaviour {
    [Header("Jumping")]
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private PlayerState currentState;

    private IdleState idleState;
    private JumpingState jumpingState;
    private FallingState fallingState;

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        // Create instances of all states
        idleState = new IdleState();
        jumpingState = new JumpingState(jumpForce);
        fallingState = new FallingState();

        // Set the initial state
        TransitionToState(idleState);
    }

    void Update() {
        if (currentState != null) {
            currentState.UpdateState();
            CheckForStateTransition();
        }
    }

    private void CheckForStateTransition() {
        if (currentState == idleState && Input.GetButtonDown("Jump") && IsGrounded()) {
            TransitionToState(jumpingState);
        } else if (currentState == jumpingState && rb.linearVelocity.y < 0) {
            TransitionToState(fallingState);
        } else if (currentState == fallingState && IsGrounded()) {
            TransitionToState(idleState);
        }
    }

    private void TransitionToState(PlayerState newState) {
        if (currentState != null) {
            currentState.ExitState();
        }

        currentState = newState;
        currentState.EnterState(rb, groundCheck, groundCheckRadius);
    }

    private bool IsGrounded() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius);
        foreach (var col in colliders) {
            if (col.gameObject != this.gameObject) {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected() {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}