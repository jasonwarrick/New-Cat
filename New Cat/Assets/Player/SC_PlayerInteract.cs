using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactDistance;
    [SerializeField] Transform holdPoint;


    [SerializeField] LayerMask interactLayer;
    Camera cam;
    
    void Start() {
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update() {
        if (Time.timeScale != 0f) {
            RaycastHit hit;
            
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactDistance, interactLayer)) {
                Debug.Log("object in range");
            } else {
                
            }
        }
        
    }
}
