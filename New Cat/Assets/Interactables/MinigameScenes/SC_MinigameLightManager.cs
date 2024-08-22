using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MinigameLightManager : MonoBehaviour
{
    public bool entityLight = true;
    [SerializeField] float lightHeight;
    
    public bool forceOff = false;
    bool isNeeded = false;
    bool isRequiredObjectHeld = false;
    Vector3 location;

    public Transform minigame;
    public Transform pickup;
    GameObject lightObject;

    void Awake() {
        lightObject = GetComponentInChildren<Light>().gameObject;
    }

    void Start() {
        SetLocation();
        ToggleLight(false);
    }

    public void ToggleLight(bool turnOn) {
        isNeeded = minigame.GetComponent<SC_Minigame>().Needed;
        isRequiredObjectHeld = GameManager.instance.HeldObject != null && GameManager.instance.HeldObject.name == pickup.name;

        SetLocation();


        lightObject.SetActive(turnOn);
    }

    public void ToggleForceOff(bool isNeededOff) {
        forceOff = isNeededOff;

        ToggleLight(forceOff);
    }

    public void SetLocation() {
        if (isNeeded) {
            if (!isRequiredObjectHeld) {
                location = pickup.position;
                Debug.Log("Light at pickup");
            } else {
                location = minigame.position;
                Debug.Log("Light at home");
            }

            location = new Vector3(location.x, location.y + lightHeight, location.z);
            transform.position = location;
        }
    }
}
