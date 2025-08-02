using UnityEngine;

public class EarlyInputHandler
{
    private float timeSinceInput;
    private string inputChannel;
    private float allowedTimeDiff;

    public EarlyInputHandler(string inputChannel, float allowedTimeDiff)
    {
        this.inputChannel = inputChannel;
        timeSinceInput = 999.0f;
        this.allowedTimeDiff = allowedTimeDiff;
    }

    public void Update() {
        if (Input.GetButtonDown(inputChannel))
        {
            timeSinceInput = 0.0f;
            return;
        }
        timeSinceInput += Time.deltaTime;
    }

    public bool WasPressed()
    {
        return timeSinceInput <= allowedTimeDiff;
    }

    //public void ResetInput()
    //{
    //    timeSinceInput = 999.0f;
    //}
}
