using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public delegate void E_PauseGame(bool isPaused);
    public static E_PauseGame pauseGame;

    bool isPaused = false;
    GameObject heldObject = null;
    public GameObject HeldObject {
        get { return heldObject; }
        set { heldObject = value;}
    }

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

        if (isPaused) { // If the game is being paused:
            pauseGame.Invoke(isPaused); // Trigger the pause event

            Time.timeScale = 0f; // Pause the game
        } else { // If the game is being unpaused
            Time.timeScale = 1f; // Unpause the game

            pauseGame.Invoke(isPaused); // Trigger the pause event again
        }
    }

    public void StartGame() {
        SceneHandler.instance.LoadGame();
        UIManager.instance.ToggleCanvas("hud");
    }
}
