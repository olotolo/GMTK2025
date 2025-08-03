using UnityEngine;

public class JumpingState : PlayerState {
    private float jumpForce;
    private float standardGravity;
    private float variableJumpGravity;

    public JumpingState(float jumpForce, float standardGravity, float variableJumpGravity) {
        this.jumpForce = jumpForce;
        this.standardGravity = standardGravity;
        this.variableJumpGravity = variableJumpGravity;
    }

    public override void EnterState(Rigidbody2D playerRb, Transform groundCheck, float groundCheckRadius) {
        base.EnterState(playerRb, groundCheck, groundCheckRadius);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        playerRb.GetComponent<PlayerStateMachine>().DisableAllSpriteRenderers();
        playerRb.GetComponent<PlayerStateMachine>()._jumping.gameObject.SetActive(true);

        playerRb.gravityScale = standardGravity;
    }

    public override void UpdateState() {
        // Variable jump height
        if (!Input.GetButton("Jump")) {
            rb.gravityScale = variableJumpGravity;
        }
    }

    public override void ExitState() {
        rb.gravityScale = standardGravity;
    }
}