using UnityEngine;

public class RunningState : PlayerState
{
    public override void UpdateState() {
        
    }

    public override void EnterState(Rigidbody2D playerRb, Transform groundCheck, float groundCheckRadius) {

        playerRb.GetComponent<PlayerStateMachine>().DisableAllSpriteRenderers();
        playerRb.GetComponent<PlayerStateMachine>()._running.gameObject.SetActive(true);
    }

    public override void ExitState() {
        // No specific exit logic for IdleState
    }
}
