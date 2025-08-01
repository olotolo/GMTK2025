using UnityEngine;

public class ReverseRotation : MonoBehaviour
{
    [SerializeField] RotationController _rotationManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _rotationManager.levelRotationSpeed *= -1;
    }
}