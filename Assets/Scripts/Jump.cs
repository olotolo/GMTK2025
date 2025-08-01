using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump2D : MonoBehaviour {
    [Header("Jumping")]
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        // Check if the player is on the ground using a circle cast
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Allow jumping only when grounded
        if (Input.GetButtonDown("Jump") && isGrounded) {
            // Use Vector2.up for 2D physics
            Debug.Log("Jump");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Optional: Draw a gizmo in the editor to visualize the ground check radius
    private void OnDrawGizmosSelected() {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}