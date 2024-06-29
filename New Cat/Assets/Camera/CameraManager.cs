using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    GameObject npCam;

    void Awake() {
        instance = this;
        npCam = GetComponentInChildren<Camera>().gameObject;
    }

    public void DisableNPCamera() {
        npCam.SetActive(false);
    }

    public void EnableNPCamera() {
        npCam.SetActive(true);
    }
}
