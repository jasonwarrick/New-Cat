using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Cannon : MonoBehaviour
{
    [SerializeField] float turnSpeed;
    [SerializeField] float turnMax;

    Camera cam;

    void Start() {
        cam = FindObjectOfType<Camera>();
    }

    void Update() {
        AimCannon();
    }

    void AimCannon() {
        if (InputReader.instance.fire) {
            Debug.Log("Fire");
        }
    }
}
