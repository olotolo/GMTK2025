using UnityEngine;

public class Startup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("fadetonormal==");
        StartCoroutine(FindFirstObjectByType<SceneChanger>().FadeToNormal());
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
