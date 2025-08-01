using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject _circle;
    [SerializeField] GameObject _camera;

    bool troll;
    private void OnTriggerEnter2D(Collider2D collision) {
        _circle.GetComponent<RotateCircle>().rotationSpeed *= -1;
        _camera.GetComponent<RotateCircle>().rotationSpeed *= -1;
    }

}
