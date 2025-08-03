using UnityEngine;

public class RotateCanvas : MonoBehaviour
{
    [Tooltip("Rotation speed in degrees per second")]
    public float rotationSpeed = 90f;
    public RectTransform rectTransform;

    void Update() {
        // Rotate around the Z-axis (blue axis) in world or local space
        rectTransform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
