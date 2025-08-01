using UnityEngine;

public abstract class PlayerState {
    protected Rigidbody2D rb;
    protected Transform groundCheck;
    protected float groundCheckRadius;

    public virtual void EnterState(Rigidbody2D playerRb, Transform groundCheck, float groundCheckRadius) {
        this.rb = playerRb;
        this.groundCheck = groundCheck;
        this.groundCheckRadius = groundCheckRadius;
        Debug.Log("entering state");
    }

    public abstract void UpdateState();
    public abstract void ExitState();
}