using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MinigameLightManager : MonoBehaviour
{
    public bool entityLight = true;

    Vector3 location;

    public Transform minigame;
    public Transform pickup;

    void OnEnable() {
        SetLocation();
    }

    void SetLocation() {
        if (GameManager.instance.HeldObject == pickup) {
            location = pickup.position;
        } else {
            location = minigame.position;
        }
    }
}
