using UnityEngine;

public class PlattformJumpFromBelow : MonoBehaviour
{
    [SerializeField] Transform _player;
    
    void Start()
    {
        if(_player == null) {
            _player = FindFirstObjectByType<Player>().transform.GetComponent<SpriteRenderer>().transform;
        }    
    }

    private void Update() {
        if(_player.transform.position.y < transform.position.y) {
            GetComponent<Collider2D>().enabled = false;
        } else {
            GetComponent<Collider2D>().enabled = true;
        }
    }

}
