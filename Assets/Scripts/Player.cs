using UnityEngine;

public class Player : MonoBehaviour
{
    public float boostedFor;
    [SerializeField] GameObject _sprite;

    public void MirrorSprite()
    {
        _sprite.transform.localScale = new Vector3(-1f, 1f, 1f);
    }
    
}
