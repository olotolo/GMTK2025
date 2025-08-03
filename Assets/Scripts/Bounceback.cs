using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] RotationController _rotationManager;
    [SerializeField] float _bounceBackTime;
    private bool _inBounceback = false;
    private float _bounceTime = 0.0f;
    private float _startSign;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _inBounceback = true;
    }

    private void Start() {
        if(_rotationManager == null) {
            _rotationManager = FindFirstObjectByType<RotationController>();
        }
    }

    private void Update()
    {
        if (_inBounceback && _bounceTime <= 0.0)
        {
            _bounceTime = _bounceBackTime;
            _startSign = Mathf.Sign(_rotationManager.levelRotationSpeed);
            _rotationManager.levelRotationSpeed *= -1;       
            _inBounceback = false;
        }

        if (_bounceTime > 0.0f && Mathf.Sign(_rotationManager.targetRotationSpeed) == _startSign)
        {
            _rotationManager.levelRotationSpeed = -2 * (_bounceTime - _bounceBackTime / 2) / _bounceBackTime * _rotationManager.targetRotationSpeed;
            _bounceTime -= Time.deltaTime;   
        }
        else if (_bounceTime > 0.0f && Mathf.Sign(_rotationManager.targetRotationSpeed) != _startSign)
        {
            _rotationManager.levelRotationSpeed = _rotationManager.targetRotationSpeed;
            _bounceTime = 0.0f;
        }

    }
}
