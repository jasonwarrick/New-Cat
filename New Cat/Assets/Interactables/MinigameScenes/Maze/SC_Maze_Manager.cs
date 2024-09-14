using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Maze_Manager : MonoBehaviour
{
    GameObject player;

    void Start() {
        player = FindObjectOfType<SC_Maze_Player>().gameObject;
    }

    public void PlayerHit() {
        if (SC_EntityBrain.instance != null) {
            SC_EntityBrain.instance.FailedMinigame();
        }
        // player.SetActive(false);
        player.transform.position = transform.position;
        // player.SetActive(true);
    }
}
