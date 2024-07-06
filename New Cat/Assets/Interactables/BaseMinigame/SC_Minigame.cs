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
    public string minigameSceneName;

    GameManager gameManager;
    Transform playerCamera;
    public SC_MinigameSetup minigameSetup;

    void Start() {
        playerCamera = FindObjectOfType<Camera>().transform;

        GameManager.instance.InitializeMinigame(minigameSceneName);
    }

    void Update() {
        available = GameManager.instance.HeldObject == requiredObject; // Set the minigame availability to true if the held object matches the required object
        // Might need to add functionality to change availability according to additional factors

        if (InputReader.instance.exit) {
            ExitMinigame();
        }
    }

    public bool Interact(SC_PlayerInteract playerInteract) {
        if (!available) { Debug.Log("Minigame is not available"); return available; }

        Debug.Log("Interacted");

        SC_FPSController.instance.LockPlayer();
        GameManager.instance.StartMinigame(minigameSceneName);

        if (minigameSetup != null) {
            minigameSetup.gameObject.SetActive(true);
        }

        return available;
    }

    void ExitMinigame() {
        if (GameManager.instance.isInMinigame) {
            SC_FPSController.instance.UnlockPlayer();
            GameManager.instance.StopMinigame(minigameSceneName);

            if (minigameSetup != null) {
                minigameSetup.TurnOffSetup();
            }
        }
    }
}
