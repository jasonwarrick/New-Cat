using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public delegate void E_PauseGame(bool isPaused);
    public static E_PauseGame pauseGame;

    public delegate void ChangedHeldItem();
    public static ChangedHeldItem changedHeldItem;

    public bool isPaused = false;
    public bool isInMinigame = false;
    GameObject heldObject = null;
    public GameObject HeldObject {
        get { return heldObject; }
    }

    public SC_PlayerInteract playerInteract;

    void Awake() {
        instance = this;
    }

    void Update() {
        if (InputReader.instance.pause) {
            PauseGame();
        }
    }

    public void PauseGame() {
        isPaused = !isPaused;
        SetPlayerCanInteract();

        if (isPaused) { // If the game is being paused:
            pauseGame.Invoke(isPaused); // Trigger the pause event

            Time.timeScale = 0f; // Pause the game
        } else { // If the game is being unpaused
            Time.timeScale = 1f; // Unpause the game

            pauseGame.Invoke(isPaused); // Trigger the pause event again
        }
    }

    public void UpdateHeldObject(GameObject newHeldObject) {
        heldObject = newHeldObject;
        changedHeldItem.Invoke();
    }

    void SetPlayerCanInteract() {
        playerInteract.SetCanInteract(!isInMinigame && !isPaused);
    }

    public void StartGame() {
        SceneHandler.instance.LoadGame();
        UIManager.instance.ToggleCanvas("hud");
    }

    public void StartMinigame() {
        isInMinigame = true;
        SetPlayerCanInteract();
        UIManager.instance.StoreUIState("");
    }

    public void StopMinigame() {
        isInMinigame = false;
        SetPlayerCanInteract();
        UIManager.instance.ResumeUIState();
    }
}
