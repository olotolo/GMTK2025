using UnityEngine;

public class Player : MonoBehaviour
{
    public float boostedFor;
    [SerializeField] GameObject _sprite;

    public void MirrorSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
}
