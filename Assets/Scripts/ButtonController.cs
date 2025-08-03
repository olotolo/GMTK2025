using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject _targetObject;

    public enum Mode
    {
        Toggle,
        SetTimer
    }

    public Mode _mode = Mode.Toggle;

    [Tooltip("Time in seconds the door stays open (only used when SetTimer is selected)")]
    public int doorOpenTime = 5;

    [Tooltip("Time in seconds for button reactivation")]
    public int buttonReactivationTime = 3;

    private bool isActivated = true;

    [Header("Button Sprites")]
    [SerializeField] private Sprite buttonStage1;
    [SerializeField] private Sprite buttonStage2;
    [SerializeField] private Sprite buttonStage3;
    [SerializeField] private Sprite buttonStage4;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered trigger with: " + collision.name);
        if (collision.CompareTag("Player") && isActivated)
        {
            AudioController.instance.Play("ButtonClick");

            Debug.Log("Button triggered!");
            var activatable = _targetObject.GetComponent<IActivatable>();

            if (_mode == Mode.Toggle)
            {
                activatable.Toggle();
            }
            else if (_mode == Mode.SetTimer)
            {
                activatable.OpenTemporarily(doorOpenTime);
            }

            StartCoroutine(HandleReactivationSequence());
        }
    }

    private IEnumerator HandleReactivationSequence()
    {
        isActivated = false;

        var sr = GetComponent<SpriteRenderer>();

        if (buttonStage1 != null) sr.sprite = buttonStage1;
        yield return new WaitForSeconds(buttonReactivationTime / 3f);

        if (buttonStage2 != null) sr.sprite = buttonStage2;
        yield return new WaitForSeconds(buttonReactivationTime / 3f);

        if (buttonStage3 != null) sr.sprite = buttonStage3;
        yield return new WaitForSeconds(buttonReactivationTime / 3f);

        if (buttonStage4 != null) sr.sprite = buttonStage4;

        isActivated = true;
    }
}