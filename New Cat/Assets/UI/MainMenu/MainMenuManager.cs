using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void Pressed_PlayGame() {
        SceneHandler.instance.LoadGame();
    }

    public void Pressed_Settings() {
        Debug.Log("Settings pressed");
    }

    public void Pressed_Exit() {
        SceneHandler.instance.QuitGame();
    }
}
