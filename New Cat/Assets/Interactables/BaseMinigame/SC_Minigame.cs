using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Minigame : MonoBehaviour, I_Interact
{
    [SerializeField] bool available = false;
    bool needed = false;
    public string needName;
    [Range(1, 3)]
    [SerializeField] int type = 2;
    
    public bool Available {
        get { return available; }
        set { available = value; }
    }

    public bool Needed {
        get { return needed; }
        set { needed = value; }
    }

    public int Type {
        get { return type; }
        set { type = value; }
    }

    [SerializeField] GameObject requiredObject;
    public string minigameSceneName;

    GameManager gameManager;
    SC_MinigameLightManager minigameLightManager;
    Transform playerCamera;
    public SC_MinigameSetup minigameSetup;

    void Start() {
        playerCamera = FindObjectOfType<Camera>().transform;
        minigameLightManager = GetComponentInChildren<SC_MinigameLightManager>();

        GameManager.instance.InitializeMinigame(minigameSceneName);
        GameManager.changedHeldItem += UpdateAvailable;
    }

    void Update() {
        if (InputReader.instance.exit) {
            ExitMinigame();
        }
    }

    public void UpdateNeeded(bool isNeeded) {
        needed = isNeeded;
        UpdateAvailable();
    }

    void UpdateAvailable() {
        available = GameManager.instance.HeldObject == requiredObject && needed; // Set the minigame availability to true if the held object matches the required object
        // Might need to add functionality to change availability according to additional factors

        if (minigameLightManager == null) { return; }

        minigameLightManager.ToggleLight(needed);
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

            CompleteMinigame();
        }
    }

    void CompleteMinigame() {
        SC_EntityBrain.instance.ResetNeed(needName);
        minigameLightManager.ToggleLight(false);
        needed = false;

        Debug.Log("Minigame completed");
    }
}
