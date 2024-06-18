using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactDistance;
    [SerializeField] Transform holdPoint;

    GameObject objectInRange = null;

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
                objectInRange = hit.transform.gameObject;
                ProcessRaycast(hit);
            } else {
                objectInRange = null;
                HUDManager.instance.SetCrosshair(false, false);
            }
        }
        
        if (InputReader.instance.interact) {
            Interact();
        }
    }

    void ProcessRaycast(RaycastHit hit) {
        GameObject hitObject = hit.transform.gameObject;
        
        if (hitObject.layer == interactLayer && hitObject.GetComponent<SC_Interact>() != null) {
            if (hitObject.GetComponent<SC_Interact>().Available) {
                HUDManager.instance.SetCrosshair(true, true);
            } else {
                HUDManager.instance.SetCrosshair(true, false);
            }
        } else {
            HUDManager.instance.SetCrosshair(false, false);
        }
    }

    void Interact() {
        if (objectInRange != null) {
            Debug.Log(objectInRange.name);
        } else {
            Debug.Log("nothing to interact with");
        }
    }
}
