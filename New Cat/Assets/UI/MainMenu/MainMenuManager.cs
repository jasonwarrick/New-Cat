using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    UIManager uiManager;
    GameManager gameManager;
    
    void Start() {
        uiManager = transform.parent.GetComponent<UIManager>();
        gameManager = GameManager.instance;
    }

    public void Pressed_PlayGame() {
        gameManager.StartGame();
    }

    public void Pressed_Settings() {
        Debug.Log("Settings pressed");
    }

    public void Pressed_Exit() {
        SceneHandler.instance.QuitGame();
    }
}
