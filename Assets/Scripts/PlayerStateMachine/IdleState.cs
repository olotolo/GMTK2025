using UnityEngine;

public class IdleState : PlayerState {
    public override void UpdateState() {
        // Transition to JumpingState
        if (Input.GetButtonDown("Jump")) {
            // The actual jump logic will be in the JumpingState's EnterState
        }
    }

    public override void EnterState(Rigidbody2D playerRb, Transform groundCheck, float groundCheckRadius) {
        
    }

    public override void ExitState() {
        // No specific exit logic for IdleState
    }
}