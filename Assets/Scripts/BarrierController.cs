using System.Collections;
using UnityEngine;

public class BarrierController : MonoBehaviour, IActivatable
{
    [SerializeField] private bool defaultIsOpen = false;
    private bool _isOpen;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    [SerializeField] private GameObject[] lights;
    [SerializeField] private bool showLights = true;

    [Header("Barrier Sprites")]
    [SerializeField] private Sprite barrierClosedSprite;
    [SerializeField] private Sprite barrierOpenSprite;

    public bool ShowLights
    {
        get => showLights;
        set
        {
            showLights = value;
            UpdateLightVisibility();
        }
    }

    public bool IsOpen
    {
        get => _isOpen;
        set
        {
            _isOpen = value;
            UpdateBarrierSprite();
            UpdateColliderState();  // Enable/disable collider when open/closed
        }
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();

        _isOpen = defaultIsOpen;
        UpdateBarrierSprite();
        UpdateLightVisibility();
        UpdateColliderState();  // Set initial collider state
    }

    private void UpdateBarrierSprite()
    {
        if (_spriteRenderer == null) return;
        _spriteRenderer.sprite = _isOpen ? barrierOpenSprite : barrierClosedSprite;
    }

    private void UpdateColliderState()
    {
        if (_boxCollider != null)
        {
            _boxCollider.enabled = !_isOpen;
        }
    }

    private void UpdateLightVisibility()
    {
        foreach (var lightObj in lights)
        {
            if (lightObj != null)
                lightObj.SetActive(showLights);
        }
    }

    public void Toggle()
    {
        IsOpen = !IsOpen;
    }

    public void OpenTemporarily(int duration)
    {
        StopAllCoroutines();
        StartCoroutine(HandleLightsSequence(duration));
    }

    private IEnumerator HandleLightsSequence(int duration)
    {
        bool targetState = !defaultIsOpen;
        IsOpen = targetState;

        if (showLights)
        {
            foreach (var lightObj in lights)
            {
                var controller = lightObj.GetComponent<LightController>();
                if (controller != null)
                    controller.SetLight(true);
            }
        }

        float waitTime = (duration > 0 && lights.Length > 0) ? (float)duration / lights.Length : 1f;

        for (int i = 0; i < lights.Length; i++)
        {
            yield return new WaitForSeconds(waitTime);

            if (showLights && lights[i] != null)
            {
                var controller = lights[i].GetComponent<LightController>();
                if (controller != null)
                    controller.SetLight(false);
            }
        }

        IsOpen = defaultIsOpen;
    }
}
