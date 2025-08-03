using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public bool Audio = true;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            if(Audio) {
                AudioController.instance.ChangeGeneralVolume(0);
                Audio = false;
            } else {
                AudioController.instance.ChangeGeneralVolume(1);
                Audio = true;
            }
        });

    }
}


