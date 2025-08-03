using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] GameObject _camera;
    [SerializeField] GameObject _level;

    // This is now a bit confusing, but the ones that are hidden in the inspector should not be set by hand
    public float cameraRotationSpeed;
    public float levelRotationSpeed;
    [HideInInspector] public float relativeRotation;
    [HideInInspector] public float targetRotationSpeed;
    [SerializeField] GameObject _startGameUI;
    [SerializeField] bool _skipStartUI;
 
    private bool levelHasStarted;

    private void Start()
    {
        relativeRotation = (levelRotationSpeed - cameraRotationSpeed);
        targetRotationSpeed = levelRotationSpeed;
        levelRotationSpeed = 0f;

        levelHasStarted = false;

        if (_skipStartUI) {
            StartLevel();
        }
    }

    public void StartLevel() {
        if (levelHasStarted) return;

        if(_startGameUI != null) {
            Destroy(_startGameUI);
        }

        levelRotationSpeed = targetRotationSpeed;
        levelHasStarted = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            StartLevel();
        }
        updateRotation();
    }

    public bool LevelHasStarted() { return levelHasStarted; }

    private void updateRotation()
    {
        cameraRotationSpeed = levelRotationSpeed - relativeRotation;
        _level.GetComponent<RotateCircle>().rotationSpeed = levelRotationSpeed;
        _camera.GetComponent<RotateCircle>().rotationSpeed = cameraRotationSpeed;
    }

}