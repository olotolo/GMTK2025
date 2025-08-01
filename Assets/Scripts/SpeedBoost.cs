using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] RotationController _rotationManager;
    [SerializeField] float _speedBoost;
    [SerializeField] float _fullBoostTime;
    private float _initalSpeed;
    private float _boostTime;
    private bool _currentlyBoosted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Do we make it here?");
        _initalSpeed = _rotationManager.levelRotationSpeed;
        _rotationManager.levelRotationSpeed *= _speedBoost;
        _boostTime = _fullBoostTime;
        _currentlyBoosted = true;
    }

    private void Update()
    {
        if (_boostTime > 0.0f && _currentlyBoosted)
        {
            _boostTime -= Time.deltaTime;
        }
        else if (_boostTime <= 0.0f && _currentlyBoosted)
        {
            _rotationManager.levelRotationSpeed = _initalSpeed;
            _currentlyBoosted = false;
        }
    }
}
