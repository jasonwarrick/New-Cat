using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class InputReader : MonoBehaviour
{
    public static InputReader instance;

    [Header("Input Variables")]
    public Vector2 moveVector = new Vector2(0f, 0f);
    public bool interact = false;
    public bool pause = false;

    [Header("Rewired")]
    // The Rewired player id of this character
    public int playerId = 0;
    private Player player; // The Rewired Player

    void Awake() {
        instance = this;

        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }

    void Update () {
        GetInput();
    }

    void GetInput() {
        moveVector.x = player.GetAxis("MoveHorizontal"); // get input by name or action id
        moveVector.y = player.GetAxis("MoveVertical");
        interact = player.GetButtonDown("Interact");
        pause = player.GetButtonDown("Pause");

        Debug.Log(moveVector);
    }
}
