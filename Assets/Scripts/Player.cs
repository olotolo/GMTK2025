using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] RotationController _rotationManager;
    [SerializeField] GameObject _circle;
    [SerializeField] GameObject _camera;
    [SerializeField] GameObject _door;

    private void OnTriggerEnter2D(Collider2D collision) {
        //_rotationManager.levelRotationSpeed *= -1;
        //_rotationManager.cameraRotationSpeed =_rotationManager.levelRotationSpeed - _rotationManager.relativeRotation;
        if (collision.CompareTag("Button"))
        {
            _door.GetComponent<Door_Controller>().IsOpen = !_door.GetComponent<Door_Controller>().IsOpen;
        }
        if (collision.CompareTag("Switch")) 
        {
        _circle.GetComponent<RotateCircle>().rotationSpeed *= -1;
        _camera.GetComponent<RotateCircle>().rotationSpeed *= -1;
        }
    }

}
