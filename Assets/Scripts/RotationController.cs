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
        //Time.timeScale = 0.0f;
        //relativeRotation = (levelRotationSpeed - cameraRotationSpeed);
        //targetRotationSpeed = levelRotationSpeed;
        //updateRotation();



        //targetRotationSpeed = levelRotationSpeed;
        //targetRotationSpeed = 0f;
        //levelRotationSpeed = 0f;
        //relativeRotation = (cameraRotationSpeed - targetRotationSpeed);


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

        //Time.timeScale = 1.0f;
        if(_startGameUI != null) {
            Destroy(_startGameUI);
        }

        levelRotationSpeed = targetRotationSpeed;
        levelHasStarted = true;
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