using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Cannon : MonoBehaviour
{
    Vector3 mouseScreenPos;
    Vector3 mouseWorldPos;

    Camera cam;

    void Start() {
        cam = FindObjectOfType<Camera>();
    }

    void Update() {
        AimCannon();
    }

    void AimCannon() {
        mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 1f;
        mouseWorldPos = cam.ScreenToWorldPoint(mouseScreenPos);
        transform.LookAt(mouseWorldPos, Vector3.left);
    }
}
