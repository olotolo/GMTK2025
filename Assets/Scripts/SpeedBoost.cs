using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] RotationController _rotationManager;
    [SerializeField] float _speedBoost;
    [SerializeField] float _fullBoostTime;
    private bool _currentlyBoosted;
    private GameObject _boostedObject;

    private void Start() {
        if (_rotationManager == null) {
            _rotationManager = FindFirstObjectByType<RotationController>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _boostedObject = collision.gameObject;
        if (_boostedObject.GetComponent<Player>().boostedFor <= 0.0f)
        {
            _rotationManager.levelRotationSpeed *= _speedBoost;
        }
        _boostedObject.GetComponent<Player>().boostedFor = _fullBoostTime;
        _currentlyBoosted = true;
    }

    private void Update()
    {
        if (_currentlyBoosted)
        {
            if (_boostedObject.GetComponent<Player>().boostedFor > 0.0f)
            {
                _boostedObject.GetComponent<Player>().boostedFor -= Time.deltaTime;
            }
            else
            {
                _rotationManager.levelRotationSpeed = _rotationManager.targetRotationSpeed * Mathf.Sign(_rotationManager.levelRotationSpeed);
                _currentlyBoosted = false;
            }
        }


        // Set treadmill sprite in the correct direction
        Vector3 _scale = transform.localScale;
        _scale.x = Mathf.Abs(transform.localScale.x) * Mathf.Sign(_rotationManager.levelRotationSpeed);
        transform.localScale = _scale;
    }
}