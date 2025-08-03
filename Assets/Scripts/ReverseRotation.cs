using UnityEngine;

public class ReverseRotation : MonoBehaviour
{
    [SerializeField] RotationController _rotationManager;

    private void Start() {
        if (_rotationManager == null) {
            _rotationManager = FindFirstObjectByType<RotationController>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _rotationManager.targetRotationSpeed *= -1;
        _rotationManager.levelRotationSpeed *= -1;
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().MirrorSprite();
            AudioController.instance.Play("ReverseDirection");
        }
    }
}