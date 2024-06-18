using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactDistance;
    [SerializeField] Transform holdPoint;


    [SerializeField] LayerMask interactLayerMask;
    [SerializeField] int interactLayer;
    Camera cam;
    
    void Start() {
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update() {
        if (Time.timeScale != 0f) {
            RaycastHit hit;
            
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactDistance, interactLayerMask)) {
                ProcessRaycast(hit);
            } else {
                HUDManager.instance.SetCrosshair(false, false);
            }
        }
        
    }

    void ProcessRaycast(RaycastHit hit) {
        if (hit.transform.gameObject.layer == interactLayer) {
            HUDManager.instance.SetCrosshair(true, true);
        } else {
            HUDManager.instance.SetCrosshair(false, false);
        }
    }
}
