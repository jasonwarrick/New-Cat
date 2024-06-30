using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [SerializeField] GameObject defaultCross;
    [SerializeField] GameObject availableCross;
    [SerializeField] GameObject unavailableCross;

    GameObject[] crosshairs = new GameObject[3];
    SC_PlayerInteract playerInteraction;

    void Awake() {
        crosshairs[0] = defaultCross;
        crosshairs[1] = availableCross;
        crosshairs[2] = unavailableCross;
    }

    void OnEnable() {
        instance = this;

        playerInteraction = FindObjectOfType<SC_PlayerInteract>();
    }

    void Update() {
        if (playerInteraction != null) {
            SetCrosshair(playerInteraction.isInRange, playerInteraction.isAvailable);
        } else {
            playerInteraction = FindObjectOfType<SC_PlayerInteract>();
        }
    }

    void SetCrosshair(bool inRange, bool isAvailable) {
        if (inRange && isAvailable) {
            ToggleCrosshairs(1);
        } else if (inRange) {
            ToggleCrosshairs(2);
        } else {
            ToggleCrosshairs(0);
        }
    }

    void ToggleCrosshairs(int index) {
        for (int i = 0; i < 3; i++) {
            if (i == index) {
                crosshairs[i].SetActive(true);
            } else {
                crosshairs[i].SetActive(false);
            }
        }
    }
}
