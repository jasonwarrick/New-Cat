using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Minigame : MonoBehaviour, I_Interact
{
    [SerializeField] bool available = false;
    [SerializeField] bool hasLight = true;
    bool needed = false;
    bool isInMinigame = false;
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
        if (isInMinigame && InputReader.instance.exit) {
            ExitMinigame();
        }
    }

    public void UpdateNeeded(bool isNeeded) {
        needed = isNeeded;
        UpdateAvailable();
    }

    void UpdateAvailable() {
        available = requiredObject == null || GameManager.instance.HeldObject.name != "EmptyHeldObject" && GameManager.instance.HeldObject == requiredObject && needed; // Set the minigame availability to true if the held object matches the required object
        // Might need to add functionality to change availability according to additional factors

        if (minigameLightManager == null || !hasLight) { return; }

        minigameLightManager.ToggleLight();
    }

    public bool Interact(SC_PlayerInteract playerInteract) {
        if (!available) { Debug.Log("Minigame is not available"); return available; }

        Debug.Log("Interacted");
        Debug.Log(gameObject.name);

        SC_FPSController.instance.LockPlayer();
        GameManager.instance.StartMinigame(minigameSceneName);

        if (minigameSetup != null) {
            minigameSetup.gameObject.SetActive(true);
        }

        isInMinigame = true;

        return available;
    }

    void ExitMinigame() {
        SC_FPSController.instance.UnlockPlayer();
        GameManager.instance.StopMinigame(minigameSceneName);

        if (minigameSetup != null) {
            minigameSetup.TurnOffSetup();
        }

        CompleteMinigame();
        isInMinigame = false;
    }

    void CompleteMinigame() {
        Debug.Log("Completed " + gameObject.name);
        SC_EntityBrain.instance.ResetNeed(needName);
        needed = false;

        if (hasLight) {
            minigameLightManager.ToggleLight();
        }

        Debug.Log("Minigame completed");
    }
}
