using System.Collections;
using UnityEngine;

public class Door_Controller : MonoBehaviour
{
    [SerializeField] private bool defaultIsOpen = false;
    private bool _isOpen;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private GameObject[] lights; // Assign in Inspector
    [SerializeField] private bool showLights = true; // Control whether light sprites are rendered

    [Header("Door Sprites")]
    [SerializeField] private Sprite doorClosedSprite;
    [SerializeField] private Sprite doorOpenSprite;

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
            UpdateSprite();
        }
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _isOpen = defaultIsOpen;
        UpdateSprite();
        UpdateLightVisibility();
    }

    private void UpdateSprite()
    {
        if (_spriteRenderer == null) return;
        _spriteRenderer.sprite = _isOpen ? doorOpenSprite : doorClosedSprite;
    }

    private void UpdateLightVisibility()
    {
        foreach (var lightObj in lights)
        {
            var sr = lightObj.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.enabled = showLights;
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
        IsOpen = true;

        if (showLights)
        {
            foreach (var lightObj in lights)
            {
                var sr = lightObj.GetComponent<SpriteRenderer>();
                if (sr != null)
                    sr.color = Color.yellow;
            }
        }

        float waitTime = (duration > 0 && lights.Length > 0) ? (float)duration / lights.Length : 1f;

        for (int i = 0; i < lights.Length; i++)
        {
            yield return new WaitForSeconds(waitTime);

            if (showLights)
            {
                var sr = lights[i].GetComponent<SpriteRenderer>();
                if (sr != null)
                    sr.color = Color.grey;
            }
        }

        IsOpen = false;
    }
}