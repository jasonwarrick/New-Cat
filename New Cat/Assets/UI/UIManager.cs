using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    string uiState = "";

    [SerializeField] List<GameObject> canvases = new List<GameObject>();
    [SerializeField] List<string> canvasNames = new List<string>();

    Dictionary<string, GameObject> canvasDict = new Dictionary<string, GameObject>();

    [SerializeField] string uiLoadTo = "";

    void Awake() {
        instance = this;

        // Store all of the canvases in the dictionary
        for (int i = 0; i < canvases.Count; i++) { 
            canvasDict.Add(canvasNames[i], canvases[i]);
        }

        GameManager.pauseGame += PauseHandler;

        if (uiLoadTo.Length <= 0) {
            ToggleCanvas("main menu");
        } else {
            ToggleCanvas(uiLoadTo);
        }
    }

    void OnDestroy() {
        GameManager.pauseGame -= PauseHandler;
    }

    public void ToggleCanvas(string canvasName) {
        bool flag = canvasName.Equals("") ? true : false; // Only set the flag to false if the parameter is not intentionally blank

        // Loop through all of the canvases in the dictionary and activate the given one
        foreach (KeyValuePair<string, GameObject> pair in canvasDict) {
            if (pair.Key.Equals(canvasName)) {
                flag = true;
                pair.Value.SetActive(true);
            } else {
                pair.Value.SetActive(false);
            }
        }

        // Set the canvas to the main menu if the correct canvas was not found
        if (!flag) {
            ToggleCanvas("main menu");
        }
    }

    // Activate the stored canvas
    public void ResumeUIState() {
        ToggleCanvas(uiState);
    }

    // Store the current activated canvas so it can be reactivated at a later time
    public void StoreUIState(string canvasName) {
        foreach (KeyValuePair<string, GameObject> pair in canvasDict) {
            if (pair.Value.activeInHierarchy) {
                uiState = pair.Key;
            }
        }

        ToggleCanvas(canvasName);
    }

    void PauseHandler(bool isPaused) {
        if (isPaused) { // If the game is paused:
            // Store the current UI state and show the pause canvas
            StoreUIState("pause");

            if (!InputReader.instance.usingJoystick) { // Unlock the cursor if the player isn't using a controller
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        } else { // If the game is resumed:
            ResumeUIState(); // Resume the stored UI state

            // Lock cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
