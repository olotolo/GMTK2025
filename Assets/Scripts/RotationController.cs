using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] GameObject _camera;
    [SerializeField] GameObject _level;
    public float cameraRotationSpeed;
    public float levelRotationSpeed;
    public float relativeRotation;
    private bool posRot;

    private void Start()
    {
        relativeRotation = (levelRotationSpeed - cameraRotationSpeed);
        updateRotation();
    }

    private void Update()
    {
        updateRotation();
    }

    private void updateRotation()
    {
        cameraRotationSpeed = levelRotationSpeed - relativeRotation;
        _level.GetComponent<RotateCircle>().rotationSpeed = levelRotationSpeed;
        _camera.GetComponent<RotateCircle>().rotationSpeed = cameraRotationSpeed;
    }

}