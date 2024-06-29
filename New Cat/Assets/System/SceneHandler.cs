using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler instance;

    void Start() {
        instance = this;

        DontDestroyOnLoad(this);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
        UIManager.instance.ToggleCanvas("main menu");
    }

    public void LoadGame() {
        SceneManager.LoadScene("SampleScene");
        UIManager.instance.ToggleCanvas("hud");
    }

    public void QuitGame() {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
