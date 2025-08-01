using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject _circle;
    [SerializeField] GameObject _camera;
    [SerializeField] GameObject _door;

    bool troll;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Button"))
        {
            _door.GetComponent<Door_Controller>().IsOpen = !_door.GetComponent<Door_Controller>().IsOpen;
        }
    }

}
