using UnityEngine;

public class ReverseRotationSingleDirection : MonoBehaviour
{
    [SerializeField] RotationController _rotationManager;
    [SerializeField] bool _clockwise = true;

    private void Start()
    {
        if (_rotationManager == null)
        {
            _rotationManager = FindFirstObjectByType<RotationController>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && ((_rotationManager.targetRotationSpeed < 0 && _clockwise) || (_rotationManager.targetRotationSpeed > 0 && !_clockwise)))
        {
            collision.GetComponent<Player>().MirrorSprite();
            _rotationManager.targetRotationSpeed *= -1;
            _rotationManager.levelRotationSpeed *= -1;
        }
    }
}
