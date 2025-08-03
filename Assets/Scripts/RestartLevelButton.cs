using UnityEngine;
using UnityEngine.UI;

public class RestartLevelButton : MonoBehaviour
{
    public SceneChanger sceneChanger;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            sceneChanger.RestartScene();

        });

    }
}


