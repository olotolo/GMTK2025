using UnityEngine;

public class LightController : MonoBehaviour
{
    [Header("Light Sprites")]
    [SerializeField] private Sprite lightOffSprite;
    [SerializeField] private Sprite lightOnSprite;

    private bool _isOn = false;

    private SpriteRenderer _spriteRenderer;

    private bool IsOn
    {
        get => _isOn;
        set
        {
            _isOn = value;
            UpdateSprite();
        }
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (_spriteRenderer == null) return;
        _spriteRenderer.sprite = _isOn ? lightOnSprite : lightOffSprite;
    }

    public void SetLight(bool turnOn)
    {
        _isOn = turnOn;
        UpdateSprite();
    }
}
