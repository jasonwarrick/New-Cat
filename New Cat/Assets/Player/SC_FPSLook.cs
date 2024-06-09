using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FPSLook : MonoBehaviour
{
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    float rotationX = 0;

    [SerializeField] Transform cameraPoint;

    [HideInInspector]
    public bool canLook = true;

    // Start is called before the first frame update
    void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Player and Camera rotation
        if (canLook)
        {
            transform.position = cameraPoint.position;
            Debug.Log(cameraPoint.parent.rotation);
            transform.rotation = cameraPoint.parent.rotation;
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            // cameraPoint.parent.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
