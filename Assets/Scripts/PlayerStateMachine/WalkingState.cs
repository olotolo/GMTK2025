using UnityEngine;

public class WalkingState : PlayerState
{
    public override void UpdateState() {
        
    }

    public override void EnterState(Rigidbody2D playerRb, Transform groundCheck, float groundCheckRadius) {
        Debug.Log("enter walking state");
        playerRb.GetComponent<PlayerStateMachine>().DisableAllSpriteRenderers();
        playerRb.GetComponent<PlayerStateMachine>()._walking.gameObject.SetActive(true);
    }

    public override void ExitState() {
        // No specific exit logic for IdleState
    }
}
