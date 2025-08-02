using UnityEngine;

public class JumpingState : PlayerState {
    private float jumpForce;

    public JumpingState(float jumpForce) {
        this.jumpForce = jumpForce;
    }

    public override void EnterState(Rigidbody2D playerRb, Transform groundCheck, float groundCheckRadius) {
        Debug.Log("jupming state");
        base.EnterState(playerRb, groundCheck, groundCheckRadius);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        playerRb.GetComponent<PlayerStateMachine>().DisableAllSpriteRenderers();
        playerRb.GetComponent<PlayerStateMachine>()._jumping.gameObject.SetActive(true);
    }

    public override void UpdateState() {
        // Transition to FallingState
        if (rb.linearVelocity.y < 0) {
            // Player has started to fall
        }
    }

    public override void ExitState() {
        // No specific exit logic for JumpingState
    }
}