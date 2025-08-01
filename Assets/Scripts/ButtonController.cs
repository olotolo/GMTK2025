using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject _door;

    public enum Mode
    {
        Toggle,
        SetTimer
    }

    public Mode _mode = Mode.Toggle;

    [Tooltip("Time in seconds (only used when SetTimer is selected)")]
    public int _amountOfTime = 5;

    private bool isActivated = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered trigger with: " + collision.name);
        if (collision.CompareTag("Player") && isActivated)
        {
            Debug.Log("Button triggered!");
            var doorController = _door.GetComponent<Door_Controller>();

            if (_mode == Mode.Toggle)
            {
                doorController.Toggle();
            }
            else if (_mode == Mode.SetTimer)
            {
                doorController.OpenTemporarily(_amountOfTime);
            }

            StartCoroutine(HandleReactivationSequence());
        }
    }

    private IEnumerator HandleReactivationSequence()
    {
        isActivated = false;

        var sr = GetComponent<SpriteRenderer>();
        sr.color = Color.grey;

        Vector3 originalScale = transform.localScale;

        // Shrink the button height
        Vector3 stepScale = originalScale;
        stepScale.y = 0.005f; // starting small
        transform.localScale = stepScale;

        // Calculate Y increment per second
        float totalYChange = originalScale.y - stepScale.y;
        float stepAmount = totalYChange / _amountOfTime;

        for (int i = 0; i < _amountOfTime; i++)
        {
            yield return new WaitForSeconds(1f);

            stepScale.y += stepAmount;
            transform.localScale = new Vector3(originalScale.x, stepScale.y, originalScale.z);
        }

        // Final fix (in case of floating point drift)
        transform.localScale = originalScale;

        sr.color = Color.red;
        isActivated = true;
    }






    //// Turn all lights yellow
    //foreach (var lightObj in lights)
    //{
    //    var sr = lightObj.GetComponent<SpriteRenderer>();
    //    if (sr != null)
    //        sr.color = Color.yellow;
    //}

    //// Wait and turn them off one by one
    //for (int i = 0; i < lights.Length; i++)
    //{
    //    yield return new WaitForSeconds(1f);

    //    var sr = lights[i].GetComponent<SpriteRenderer>();
    //    if (sr != null)
    //        sr.color = Color.grey;
    //}

    //// After all are off, close the door
    //IsOpen = false;


}
