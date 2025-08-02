using UnityEngine;

public class FallingState : PlayerState {
    public override void UpdateState() {
        // Transition to IdleState
        /*if (IsGrounded()) {
            // Player has landed
        }*/
    }

    public override void EnterState(Rigidbody2D playerRb, Transform groundCheck, float groundCheckRadius) {
        Debug.Log("enter falling state");
    }


    public override void ExitState() {
        // No specific exit logic for FallingState
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