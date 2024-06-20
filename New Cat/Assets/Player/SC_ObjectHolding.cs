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
        if (inObject == null) { return; } // Catch any erroneous calls
        if (swapping) { SwapObjects(); } // If the items are already swapping, stop the current swap so the new one can start

        // Reset the parent of the currently held object to that of the gameObjects parent
        heldObject.transform.parent = gameObjects;
        heldObject.GetComponent<Collider>().enabled = true;

        // Do the same for the item that is being selected, as well as storing its position
        newObject = inObject;
        newObject.transform.parent = transform;
        newObject.GetComponent<Collider>().enabled = false;
        swapPosition = newObject.transform.position;

        // Start the swap
        swapping = true;
        moveRoutine = StartCoroutine("MoveObjects");
    }

    IEnumerator MoveObjects() {
        while(counter <= itemSwapSpeed) {
            counter += Time.deltaTime;
            float smoothedCounter = 1 - Mathf.Pow(1 - counter, 3); // Smooth the counter variable with a cubic function

            heldObject.transform.localPosition = Vector3.Lerp(heldObject.transform.localPosition, swapPosition, smoothedCounter / itemSwapSpeed); // Move the held object to where the selected object was taken from
            newObject.transform.localPosition = Vector3.Lerp(newObject.transform.localPosition, Vector3.zero, smoothedCounter / itemSwapSpeed); // Move the selected object to the hold point

            // Realign both objects rotations
            newObject.transform.localRotation = Quaternion.RotateTowards(newObject.transform.localRotation, Quaternion.identity, smoothedCounter / itemSwapSpeed);
            heldObject.transform.rotation = Quaternion.Slerp(heldObject.transform.rotation, Quaternion.identity, smoothedCounter / itemSwapSpeed);
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
