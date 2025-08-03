using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door_Controller : MonoBehaviour, IActivatable
{
    [SerializeField] private bool defaultIsOpen = false;
    [SerializeField] RotationController _rotationManager;
    private bool _isOpen;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private GameObject[] lights; // Assign in Inspector
    [SerializeField] private bool showLights = true; // Control visibility of light objects

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
            UpdateDoorSprite();
        }
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _isOpen = defaultIsOpen;
        UpdateDoorSprite();
        UpdateLightVisibility();
        if (_rotationManager == null)
        {
            _rotationManager = FindFirstObjectByType<RotationController>();
        }
    }

    private void UpdateDoorSprite()
    {
        if (_spriteRenderer == null) return;
        _spriteRenderer.sprite = _isOpen ? doorOpenSprite : doorClosedSprite;
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

        if (showLights)
        {
            foreach (var lightObj in lights)
            {
                var controller = lightObj.GetComponent<LightController>();
                if (controller != null)
                    controller.SetLight(true);
            }
        }
        
    }

    public void OpenTemporarily(int duration)
    {
        StopAllCoroutines();
        StartCoroutine(HandleLightsSequence(duration));
    }

    private IEnumerator HandleLightsSequence(int duration)
    {
        bool targetState = !defaultIsOpen;  // Invert the default state
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

        IsOpen = defaultIsOpen;  // Return to default
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(IsOpen) {

            FindFirstObjectByType<SceneChanger>().LoadNextLevel();
            _rotationManager.levelRotationSpeed = 0.0f;
        }
    }

}
