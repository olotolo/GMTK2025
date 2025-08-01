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

    private JumpingState jumpingState;
    private FallingState fallingState;
    private WalkingState walkingState;
    private RunningState runningState;

    [SerializeField] public SpriteRenderer _idle;
    [SerializeField] public SpriteRenderer _walking;
    [SerializeField] public SpriteRenderer _jumping;
    [SerializeField] public SpriteRenderer _falling;


    void Start() {
        rb = GetComponent<Rigidbody2D>();

        // Create instances of all states
        jumpingState = new JumpingState(jumpForce);
        fallingState = new FallingState();
        walkingState = new WalkingState();
        runningState = new RunningState();


        // Set the initial state
        TransitionToState(walkingState);
    }


    public void DisableAllSpriteRenderers() {
        _idle.gameObject.SetActive(false);
        _walking.gameObject.SetActive(false);
        _jumping.gameObject.SetActive(false);
        _falling.gameObject.SetActive(false);
    }

    void Update() {
        if (currentState != null) {
            currentState.UpdateState();
            CheckForStateTransition();
        }
    }

    private bool walkingOrRunning() {
        if (currentState == walkingState || currentState == runningState) {
            return true;
        }
        return false;
    }

    private void CheckForStateTransition() {
        if (walkingOrRunning() && Input.GetButtonDown("Jump") && IsGrounded()) {
            TransitionToState(jumpingState);
        }
        else if (walkingOrRunning() && !IsGrounded()) {
            TransitionToState(fallingState);
        }
        else if (currentState == jumpingState && rb.linearVelocity.y <= 0) {
            TransitionToState(fallingState);
        }
        else if (currentState == fallingState && IsGrounded()) {
            TransitionToState(walkingState);
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