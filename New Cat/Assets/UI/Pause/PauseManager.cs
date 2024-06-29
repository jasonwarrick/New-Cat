using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public void Pressed_Resume() {
        GameManager.instance.PauseGame();
    }

    public void Pressed_Settings() {
        Debug.Log("Settings pressed");
    }

    public void Pressed_MainMenu() {
        SceneHandler.instance.LoadMainMenu();
    }
}
