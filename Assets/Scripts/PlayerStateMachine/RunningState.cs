using UnityEngine;

public class RunningState : PlayerState
{
    public override void UpdateState() {
        
    }

    public override void EnterState(Rigidbody2D playerRb, Transform groundCheck, float groundCheckRadius) {
        Debug.Log("enter running state");
    }

    public override void ExitState() {
        // No specific exit logic for IdleState
    }
}
