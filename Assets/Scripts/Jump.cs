using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour {
    [Header("Jumping")]
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    // We no longer need the LayerMask variable
    // public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        // We now call a custom function to check for the ground
        CheckIfGrounded();

        // Allow jumping only when grounded
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void CheckIfGrounded() {
        // Get an array of all colliders that the ground check circle is overlapping with.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius);

        // Assume we are not grounded until we find a valid collider.
        isGrounded = false;

        // Loop through all the colliders found by the overlap circle.
        foreach (Collider2D col in colliders) {
            // **This is the crucial part:**
            // We check if the collider we found belongs to a DIFFERENT GameObject.
            // If we didn't do this, the player's own collider would always be detected,
            // making the player think it's always on the ground.
            if (col.gameObject != this.gameObject) {
                isGrounded = true;
                // Since we found a valid ground object, we can stop checking.
                break;
            }
        }
    }

    // Optional: Draw a gizmo in the editor to visualize the ground check radius
    private void OnDrawGizmosSelected() {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}