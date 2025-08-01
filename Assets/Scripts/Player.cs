using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] RotationController _rotationManager;

    private void OnTriggerEnter2D(Collider2D collision) {
        _rotationManager.levelRotationSpeed *= -1;
        //_rotationManager.cameraRotationSpeed =_rotationManager.levelRotationSpeed - _rotationManager.relativeRotation;
    }

}
