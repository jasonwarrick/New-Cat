using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler instance;

    public List<string> loadedScenes = new List<string>();

    [Header("Debugging")]
    [SerializeField] bool testObject;

    void Awake() {
        if(FindObjectsOfType<SceneHandler>().Length > 1 && testObject) { Debug.Log("found other system instances"); Destroy(gameObject); } 
        DontDestroyOnLoad(this);

        instance = this;
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

    IEnumerator UnloadScene(string sceneName) {
        Debug.Log("Start unload");
        yield return null;
        SceneManager.UnloadSceneAsync(sceneName);
        loadedScenes.Remove(sceneName);
        Debug.Log("Scene unloaded");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Scene_MainMenu");
        UIManager.instance.ToggleCanvas("main menu");
    }

    public void LoadGame() {
        AdditiveLoadScene("Scene_TestLevel");
        StartCoroutine(UnloadScene("Scene_MainMenu"));
    }

    public void QuitGame() {
        Debug.Log("Quit game");
        Application.Quit();
    }

    public void LoadMinigame(string minigameName) {
        AdditiveLoadScene(minigameName);
    }

    public void UnloadMinigame(string minigameName) {
        UnloadScene(minigameName);
    }
}
