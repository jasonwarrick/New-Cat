using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Maze_Manager : MonoBehaviour
{
    GameObject player;
    SC_Minigame minigameManager;

    void Start() {
        player = FindObjectOfType<SC_Maze_Player>().gameObject;
        minigameManager = GetComponentInParent<SC_Minigame>();
    }

    public void PlayerHit() {
        if (SC_EntityBrain.instance != null) {
            SC_EntityBrain.instance.FailedMinigame();
        }

        ResetMaze();
        minigameManager.FailMinigame();
    }

    public void GoalReached() {
        minigameManager.CompleteMinigame();
        ResetMaze();
    }

    void ResetMaze() {
        player.transform.position = transform.position;
    }
}
