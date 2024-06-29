using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler instance;

    public List<string> loadedScenes = new List<string>();

    void Awake() {
        DontDestroyOnLoad(this);

        instance = this;
        
        AdditiveLoadScene("Scene_UI");
    }

    // Load the indicated scene additively, and add it to the list of loaded scenes
    void AdditiveLoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        if (SceneManager.GetSceneByName(sceneName) != null) {
            Debug.Log("[SCENE] Loaded " + sceneName);
        } else {
            Debug.Log("[SCENE] Failed to load " + sceneName);
        }

        loadedScenes.Add(sceneName);
    }

    void UnloadScene(string sceneName) {
        SceneManager.UnloadSceneAsync(sceneName);
        loadedScenes.Remove(sceneName);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
        UIManager.instance.ToggleCanvas("main menu");
    }

    public void LoadGame() {
        AdditiveLoadScene("TestLevel");
        AdditiveLoadScene("Scene_Player");
    }

    public void QuitGame() {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
