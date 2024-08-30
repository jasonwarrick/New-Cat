using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MinigameLightManager : MonoBehaviour
{
    public bool entityLight = true;
    [SerializeField] float lightHeight;
    
    bool forceOff = false;
    bool isNeeded = false;
    bool isRequiredObjectHeld = false;
    bool isEmptyObjectHeld = false;
    Vector3 location;
    bool isDim = false;

    public Transform minigame;
    public Transform pickup;
    [SerializeField] GameObject onLight;
    [SerializeField] GameObject dimLight;
    GameObject lightObject;

    void Awake() {
        lightObject = GetComponentInChildren<Light>().gameObject;
    }

    void Start() {
        SetLocation();
        ToggleLight();
    }

    public void ToggleLight() {
        isNeeded = minigame.GetComponent<SC_Minigame>().Needed;
        isRequiredObjectHeld = GameManager.instance.HeldObject != null && GameManager.instance.HeldObject.name == pickup.name;
        isEmptyObjectHeld = GameManager.instance.HeldObject != null && GameManager.instance.HeldObject.name == "EmptyHeldObject";

        SetLocation();

        isDim = isNeeded && isEmptyObjectHeld;

        if (!forceOff) {
            if (isNeeded) {
                if (isEmptyObjectHeld || isRequiredObjectHeld) {
                    dimLight.SetActive(false);
                    onLight.SetActive(true);
                } else {
                    dimLight.SetActive(true);
                    onLight.SetActive(false);
                }
            } else {
                dimLight.SetActive(false);
                onLight.SetActive(false);
            }
        }

        // Debug.Log("Needed = " + isNeeded + "; req held = " + isRequiredObjectHeld + "; empty held = " + isEmptyObjectHeld);
    }

    public void ToggleForceOff(bool isNeededOff) {
        forceOff = isNeededOff;

        ToggleLight();
    }

    public void SetLocation() {
        if (isNeeded) {
            if (!isRequiredObjectHeld) {
                location = pickup.position;
                // Debug.Log("Light at pickup");
            } else {
                location = minigame.position;
                // Debug.Log("Light at home");
            }

            location = new Vector3(location.x, location.y + lightHeight, location.z);
            transform.position = location;
        }
    }
}
