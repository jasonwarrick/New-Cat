using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Minigame : MonoBehaviour, I_Interact
{
    [SerializeField] bool available = false;
    [Range(1, 3)]
    [SerializeField] int type = 2;
    
    public bool Available {
        get { return available; }
        set { available = value; }
    }

    public int Type {
        get { return type; }
        set { type = value; }
    }

    [SerializeField] GameObject requiredObject;

    GameManager gameManager;
    Transform playerCamera;
    [SerializeField] Transform minigameCameraPosition;
    float playerCameraHeight = 0f;

    void Start() {
        playerCamera = SC_FPSController.instance.transform.gameObject.GetComponentInChildren<Camera>().transform;
        playerCameraHeight = playerCamera.position.y;
        // Debug.Log(playerController);
    }

    void Update() {
        available = GameManager.instance.HeldObject == requiredObject; // Set the minigame availability to true if the held object matches the required object
        // Might need to add functionality to change availability according to additional factors
    }

    public bool Interact(SC_PlayerInteract playerInteract) {
        if (!available) { Debug.Log("returned from interact early"); return available; }

        Debug.Log("Interacted");

        SC_FPSController.instance.LockPlayer();
        // playerCamera.position = minigameCameraPosition.position;

        return available;
    }
}
