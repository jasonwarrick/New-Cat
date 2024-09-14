using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MinigameSetup : MonoBehaviour
{
    [SerializeField] float positionOffset = 2f;

    Transform playerCam;
    public string minigameSceneName = "";
    SC_Minigame minigameScript;

    void Awake() {
        playerCam = FindObjectOfType<Camera>().transform;

        SC_Minigame[] minigames = FindObjectsOfType<SC_Minigame>();

        if (minigames.Length == 0) { return; } // Break out of awake if the minigame is being tested in isolation

        gameObject.SetActive(false);
    }

    void OnEnable() { // Set parent to the camera and move it along the z-axis
        transform.parent = playerCam;
        transform.rotation = playerCam.rotation;
        transform.localPosition = new Vector3(0f, 0f, positionOffset);
    }

    public void TurnOffSetup() { // Reset the parent to the mingame object and turn the minigame off
        transform.parent = minigameScript.transform;
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }
}
