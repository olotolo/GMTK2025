using System.Collections;
using UnityEngine;

public class Door_Controller : MonoBehaviour
{
    bool _isOpen;
    SpriteRenderer _spriteRenderer;

    [SerializeField] GameObject[] lights; // Assign Light_01, Light_02, Light_03 in Inspector

    public bool IsOpen
    {
        get { return _isOpen; }
        set
        {
            _isOpen = value;
            UpdateColor();
            if (_isOpen)
            {
                StartCoroutine(HandleLightsSequence());
            }
        }
    }

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    void UpdateColor()
    {
        if (_spriteRenderer == null) return;

        _spriteRenderer.color = _isOpen ? Color.black : Color.grey;
    }

    IEnumerator HandleLightsSequence()
    {
        // Turn all lights yellow
        foreach (var lightObj in lights)
        {
            var sr = lightObj.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.color = Color.yellow;
        }

        // Wait and turn them off one by one
        for (int i = 0; i < lights.Length; i++)
        {
            yield return new WaitForSeconds(1f);

            var sr = lights[i].GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.color = Color.grey;
        }

        // After all are off, close the door
        IsOpen = false;
    }
}
