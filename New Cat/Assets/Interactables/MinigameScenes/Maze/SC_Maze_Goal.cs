using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Maze_Goal : MonoBehaviour
{
    SC_Maze_Manager mazeManager;

    void Awake() {
        mazeManager = GetComponentInParent<SC_Maze_Manager>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Maze_Wall")) { return; } // Ignore the maze walls

        Debug.Log("Player reached maze goal");
        mazeManager.GoalReached();
    }
}
