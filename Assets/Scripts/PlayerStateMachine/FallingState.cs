using UnityEngine;

public class FallingState : PlayerState {
    private float standardGravity;
    private float fallingGravity; 

    public FallingState(float standardGravity, float fallingGravity)
    {
        this.standardGravity = standardGravity;
        this.fallingGravity = fallingGravity;
    }

    public override void UpdateState() {
        // Transition to IdleState
        /*if (IsGrounded()) {
            // Player has landed
        }*/
    }

    public override void EnterState(Rigidbody2D playerRb, Transform groundCheck, float groundCheckRadius) {
        base.EnterState(playerRb, groundCheck, groundCheckRadius);
        playerRb.GetComponent<PlayerStateMachine>().DisableAllSpriteRenderers();
        playerRb.GetComponent<PlayerStateMachine>()._falling.gameObject.SetActive(true);

        rb.gravityScale = fallingGravity;
    }


    public override void ExitState() {
        rb.gravityScale = standardGravity;
    }

    /*private bool IsGrounded() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius);
        foreach (var col in colliders) {
            if (col.gameObject != rb.gameObject) {
                return true;
            }
        }
        return false;
    }*/
}