using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float jumpDuration = 0.5f;

    private Vector3 startPosition;
    private float jumpTimer = 0f;
    private bool isJumping = false;

    void Start() {
        startPosition = transform.position;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) {
            isJumping = true;
            jumpTimer = 0f;
        }

        if (isJumping) {
            jumpTimer += Time.deltaTime;
            float normalizedTime = jumpTimer / jumpDuration;

            // Simple parabola: y = 4h * t(1 - t)
            float height = 4 * jumpHeight * normalizedTime * (1 - normalizedTime);
            transform.position = startPosition + Vector3.up * height;

            if (normalizedTime >= 1f) {
                isJumping = false;
                transform.position = startPosition;
            }
        }
    }
}
