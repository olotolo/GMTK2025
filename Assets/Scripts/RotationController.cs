using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] GameObject _camera;
    [SerializeField] GameObject _level;
    public float cameraRotationSpeed;
    public float levelRotationSpeed;
    public float relativeRotation;
    public float targetRotationSpeed;

    [SerializeField] GameObject _startGameUI;
    [SerializeField] bool _skipStartUI;
 
    private void Start()
    {
        Time.timeScale = 0.0f;
        relativeRotation = (levelRotationSpeed - cameraRotationSpeed);
        targetRotationSpeed = levelRotationSpeed;
        updateRotation();
        if (_skipStartUI) {
            StartLevel();
        }
    }

    public void StartLevel() {
        Time.timeScale = 1.0f;
        if(_startGameUI != null) {
            Destroy(_startGameUI);
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown) {
            StartLevel();
        }
        updateRotation();
    }

    private void updateRotation()
    {
        cameraRotationSpeed = levelRotationSpeed - relativeRotation;
        _level.GetComponent<RotateCircle>().rotationSpeed = levelRotationSpeed;
        _camera.GetComponent<RotateCircle>().rotationSpeed = cameraRotationSpeed;
    }

}