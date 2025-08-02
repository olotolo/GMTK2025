using UnityEngine;

public class RotateCircle : MonoBehaviour
{
    [Tooltip("Rotation speed in degrees per second")]
    public float rotationSpeed = 90f;


    void Update() {
        // Rotate around the Z-axis (blue axis) in world or local space
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
