using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Variables")]
    [SerializeField] float interactDistance;
    
    [Header("State Variables")]
    GameObject objectInRange = null;
    [HideInInspector]
    public bool isInRange = false;
    public bool isAvailable = false;
    bool canInteract = true;

    [Header("References")]
    [SerializeField] Transform holdPoint;
    [SerializeField] LayerMask interactLayerMask;
    [SerializeField] int interactLayer;
    Camera cam;
    SC_ObjectHolding objectHolding;
    
    void Start() {
        cam = GetComponentInChildren<Camera>();
        objectHolding = holdPoint.GetComponentInChildren<SC_ObjectHolding>();
    }

    public void SetCanInteract(bool newCanInteract) {
        canInteract = newCanInteract;
    }

    void Update() {
        if (canInteract) {
            RaycastHit hit;
            
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactDistance, interactLayerMask)) { // Shoot a ray out from the center of the screen and store what it hits
                ProcessRaycast(hit);
            } else { // If there isn't anything, empty the object in range and reset the crosshair
                objectInRange = null;
                isInRange = false;
                isAvailable = false;
            }
        }
        
        if (InputReader.instance.interact) {
            Interact();
        }
    }

    void ProcessRaycast(RaycastHit hit) { // Set the correct object references and flags based on what is hit by the raycast
        GameObject hitObject = hit.transform.gameObject;
        
        if (hitObject.layer == interactLayer && hitObject.GetComponent<I_Interact>() != null) { // If the hit object is interactable
            objectInRange = hitObject;

            if (hitObject.GetComponent<I_Interact>().Available) { // Set the crosshair according to interact availability
                isInRange = true;
                isAvailable = true;
            } else {
                isInRange = true;
                isAvailable = false;
            }
        } else { // If the object is not an interactable, empty the object in range and reset the crosshair
            objectInRange = null;
            isInRange = false;
            isAvailable = false;
        }
    }

    void Interact() {
        if (objectInRange != null) {
            if (objectInRange.GetComponent<I_Interact>().Available) { // Nested if statements for future implementation of fail noises
                switch (objectInRange.GetComponent<I_Interact>().Type) { // Perform the correct interaction method based on the object's type
                    case 1: // Pickup
                        objectHolding.GrabObject(objectInRange);
                        break;

                    case 2: // Minigame
                        break;
                    
                    case 3: // Environment object
                        break;

                    default:
                        Debug.Log("Not a valid interact type");
                        break;
                }
            } else {
                Debug.Log("Interact is not available");
            }
        } else {
            // Debug.Log("Nothing to interact with");
        }
    }
}
