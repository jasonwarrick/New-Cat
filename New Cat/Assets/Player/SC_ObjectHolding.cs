using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ObjectHolding : MonoBehaviour
{
    public GameObject heldObject;

    public void GrabObject(GameObject newObject) {
        if (newObject == null) { return; }

        heldObject = newObject;
        heldObject.transform.parent = transform;
        heldObject.transform.position = transform.position;
    }
}
