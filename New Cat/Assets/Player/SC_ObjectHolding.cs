using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ObjectHolding : MonoBehaviour
{
    [SerializeField] float itemSwapSpeed;

    public GameObject heldObject;
    Vector3 swapPosition;
    GameObject newObject;
    bool swapping = false;
    float counter = 0f;

    [SerializeField] Transform gameObjects;
    Coroutine moveRoutine;

    public void GrabObject(GameObject inObject) {
        if (inObject == null) { return; }

        if (swapping) { SwapObjects(); }

        // Place the original held object where the new one is
        heldObject.transform.parent = gameObjects;
        // heldObject.transform.position = newObject.transform.position;
        heldObject.GetComponent<Collider>().enabled = true;
        // heldObject.transform.eulerAngles = Vector3.zero;

        // Hold the new object
        newObject = inObject;
        newObject.transform.parent = transform;
        swapPosition = newObject.transform.position;
        // newObject.transform.position = transform.position;
        newObject.GetComponent<Collider>().enabled = false;
        // newObject.transform.rotation = Quaternion.identity;

        swapping = true;
        moveRoutine = StartCoroutine("MoveObjects");
    }

    IEnumerator MoveObjects() {
        while(counter <= itemSwapSpeed) {
            counter += Time.deltaTime;
            heldObject.transform.localPosition = Vector3.Lerp(heldObject.transform.localPosition, swapPosition, counter / itemSwapSpeed);
            newObject.transform.localPosition = Vector3.Lerp(newObject.transform.localPosition, Vector3.zero, counter / itemSwapSpeed);

            heldObject.transform.rotation = Quaternion.Lerp(heldObject.transform.rotation, Quaternion.identity, counter / itemSwapSpeed);
            newObject.transform.rotation = Quaternion.Lerp(newObject.transform.rotation, Quaternion.identity, counter / itemSwapSpeed);
            yield return null;
        }

        SwapObjects();
    }

    void SwapObjects() {
        StopCoroutine(moveRoutine);
        swapping = false;
        counter = 0f;
        heldObject = newObject;
        newObject = null;
    }
}
