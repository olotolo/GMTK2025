using UnityEngine;

public class Mobile : MonoBehaviour
{
    [SerializeField] public GameObject _jumpButtonForMobile;
    void Start() {
        if (SystemInfo.deviceType == DeviceType.Handheld) {
            _jumpButtonForMobile.SetActive(true);
        } else {
            _jumpButtonForMobile.SetActive(false);

        }
    }



}
