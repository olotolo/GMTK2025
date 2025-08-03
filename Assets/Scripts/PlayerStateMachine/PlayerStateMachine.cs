using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerStateMachine : MonoBehaviour {
    [Header("Jumping")]
    public float jumpForce = 10f;
    public float coyoteTime = 0.0f;
    public float gravityScale = 1.0f;
    public float fallingGravityScale = 1.5f;
    public float variableJumpGravity = 2.0f;
    public float allowedJumpInputTimeDiff = 0.01f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;


    private float timeSinceGrounded;

    private Rigidbody2D rb;
    private PlayerState currentState;

    private JumpingState jumpingState;
    private FallingState fallingState;
    private WalkingState walkingState;
    private RunningState runningState;
    private StandingState standingState;
    private BouncingState bouncingState;

    [SerializeField] public Player _player;
    [SerializeField] public SpriteRenderer _standing;
    [SerializeField] public SpriteRenderer _running;
    [SerializeField] public SpriteRenderer _walking;
    [SerializeField] public SpriteRenderer _jumping;
    [SerializeField] public SpriteRenderer _falling;
    [SerializeField] public SpriteRenderer _bouncing;

    private EarlyInputHandler earlyJumpInputHandler;

    [SerializeField] RotationController _rotationManager;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        timeSinceGrounded = 0.0f;

        // For handling delayed jump inputs
        earlyJumpInputHandler = new EarlyInputHandler("Jump", allowedJumpInputTimeDiff);

        // Find rotation manager
        if (_rotationManager == null)
        {
            _rotationManager = FindFirstObjectByType<RotationController>();
        }

        // Create instances of all states
        jumpingState = new JumpingState(jumpForce, gravityScale, variableJumpGravity);
        fallingState = new FallingState(gravityScale, fallingGravityScale);
        walkingState = new WalkingState();
        runningState = new RunningState();
        standingState = new StandingState();
        bouncingState = new BouncingState();

        // Set the initial state
        TransitionToState(standingState);

        var mobile = FindFirstObjectByType<Mobile>();
        if (mobile != null && mobile._jumpButtonForMobile != null) {
            Button jumpBtn = mobile._jumpButtonForMobile.GetComponent<Button>();
            if (jumpBtn != null) {
                jumpBtn.onClick.AddListener(() => {
                    if (IsGrounded() && walkingOrRunning()) {
                        TransitionToState(jumpingState);
                    }
                });
            }
        }

    }


    public void DisableAllSpriteRenderers() {
        _standing.gameObject.SetActive(false);
        _running.gameObject.SetActive(false);
        _walking.gameObject.SetActive(false);
        _jumping.gameObject.SetActive(false);
        _falling.gameObject.SetActive(false);
        _bouncing.gameObject.SetActive(false);
    }

    void Update() {
        UpdateCoyoteTimer();
        earlyJumpInputHandler.Update();

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
        bool isGrounded = IsGrounded();
        bool jumpWasPressed = earlyJumpInputHandler.WasPressed();

        if ((currentState == standingState) && _rotationManager.LevelHasStarted())
        {
            TransitionToState(walkingState);
        }

        if (walkingOrRunning() && jumpWasPressed && (isGrounded))
        {
            TransitionToState(jumpingState);
            return;
        }
        if ((currentState == fallingState) && jumpWasPressed && (timeSinceGrounded <= coyoteTime)) {
            TransitionToState(jumpingState);
            return;
        }
        if ((currentState == jumpingState) && rb.linearVelocity.y <= 0)
        {
            TransitionToState(fallingState);
            return;
        }
        if ((!isGrounded) && rb.linearVelocity.y <= 0)
        {
            TransitionToState(fallingState);
            return;
        }
        if (currentState == fallingState && isGrounded) {
            TransitionToState(walkingState);
            AudioController.instance.Play("Land");
            return;
        }
        if(currentState == walkingState && _player.boostedFor > 0.0f)
        {
            TransitionToState(runningState);
            return;
        }
        if (currentState == runningState && _player.boostedFor <= 0.0f)
        {
            TransitionToState(walkingState);
            return;
        }
        /*if(_rotationManager.isBouncing)
        {
            TransitionToState(bouncingState);
        }*/
        if (currentState == bouncingState &&!_rotationManager.isBouncing)
        {
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

    private void UpdateCoyoteTimer() {
        if (IsGrounded()) {
            timeSinceGrounded = 0.0f;
            return;
        }
        timeSinceGrounded += Time.deltaTime;
    }

    private void OnDrawGizmosSelected() {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}