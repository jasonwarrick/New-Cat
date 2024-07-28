using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MinigameLightManager : MonoBehaviour
{
    public bool entityLight = true;
    [SerializeField] float lightHeight;

    Vector3 location;

    public Transform minigame;
    public Transform pickup;
    GameObject lightObject;

    void Awake() {
        lightObject = GetComponentInChildren<Light>().gameObject;
    }

    void Start() {
        SetLocation();
        // ToggleLight(false);
    }

    public void ToggleLight(bool turnOn) {
        SetLocation();

        lightObject.SetActive(turnOn);
    }

    public void SetLocation() {
        if (GameManager.instance.HeldObject == null || GameManager.instance.HeldObject.name != pickup.name) {
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
