using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump2D_ExplicitCheck : MonoBehaviour {
    [Header("Jumping")]
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        CheckIfGrounded();

        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void CheckIfGrounded() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius);

        isGrounded = false;
        foreach (Collider2D col in colliders) {
            if (col.gameObject != this.gameObject && col.enabled) {
                isGrounded = true;
                break;
            }
        }
    }

    /*private void OnDrawGizmosSelected() {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }*/
}