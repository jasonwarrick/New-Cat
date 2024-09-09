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
        // player.SetActive(false);
        player.transform.position = transform.position;
        // player.SetActive(true);
    }
}
