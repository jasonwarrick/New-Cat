using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MinigameSetup : MonoBehaviour
{
    GameObject playerCam;
    public string minigameSceneName = "";

    void Awake() {
        playerCam = FindObjectOfType<Camera>().gameObject;

        SC_Minigame[] minigames = FindObjectsOfType<SC_Minigame>();

        if (minigames.Length == 0) { return; } // Break out of awake if the minigame is being tested in isolation

        foreach(SC_Minigame minigame in minigames) {
            if (minigame.minigameSceneName == minigameSceneName) {
                transform.parent = minigame.transform;
            }
        }

        gameObject.SetActive(false);
    }

    void OnEnable() {
        transform.position = playerCam.transform.position;
    }
}
