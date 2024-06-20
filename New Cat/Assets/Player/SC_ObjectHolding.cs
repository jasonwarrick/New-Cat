using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ObjectHolding : MonoBehaviour
{
    public GameObject heldObject;

    [SerializeField] Transform gameObjects;

    public void GrabObject(GameObject newObject) {
        if (newObject == null) { return; }

        // Place the original held object where the new one is
        heldObject.transform.parent = gameObjects;
        heldObject.transform.position = newObject.transform.position;
        heldObject.GetComponent<Collider>().enabled = true;
        heldObject.transform.rotation = Quaternion.identity;

        // Hold the new object
        heldObject = newObject;
        heldObject.transform.parent = transform;
        heldObject.transform.position = transform.position;
        heldObject.GetComponent<Collider>().enabled = false;
        heldObject.transform.rotation = Quaternion.identity;
    }
}
