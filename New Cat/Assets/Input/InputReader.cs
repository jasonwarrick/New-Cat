using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

/**
    Mouse smoothing code adapted from: https://forum.unity.com/threads/need-help-smoothing-out-my-mouse-look-solved.543416/
**/

public class InputReader : MonoBehaviour
{
    public static InputReader instance;

    [Header("Default Input Variables")]
    public Vector2 moveVector = new Vector2(0f, 0f);
    public Vector2 mouseVector = new Vector2(0f, 0f);
    public bool interact = false;
    public bool pause = false;
    public bool exit = false;

    [Header("Work Input Variables")]
    public bool isAimRight = false;
    public bool isAimLeft = false;
    public bool fire = false;

    [Header("Input Behavior Variables")]
    [Tooltip("Filters and smooths mouse input; larger values = less filtering and jerkier movement")]
    [SerializeField] float snappiness = 10.0f; // larger values of this cause less filtering, more responsiveness
    [Tooltip("Increases the look speed when using a joystick")]
    [SerializeField] float joystickLookFactor;
    public bool usingJoystick = false;

    [Header("Rewired")]
    // The Rewired player id of this character
    public int playerId = 0;
    private Player player; // The Rewired Player

    void Awake() {
        

        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }

    void OnEnable() {
        instance = this;
        Debug.Log("Set instance");
    }

    void Update () {
        GetInput();
        ProcessInput();
        CheckController();
    }

    void CheckController() {
        Controller controller = player.controllers.GetLastActiveController();
        if (controller != null && controller.type == ControllerType.Joystick) {
            usingJoystick = true;
        } else { usingJoystick = false; }
    }

    void GetInput() {
        // Default actions
        moveVector.x = player.GetAxis("MoveHorizontal");
        moveVector.y = player.GetAxis("MoveVertical");

        mouseVector.x = player.GetAxis("MouseHorizontal");
        mouseVector.y = player.GetAxis("MouseVertical");

        if (usingJoystick) {
            mouseVector *= joystickLookFactor;
        }

        interact = player.GetButtonDown("Interact");
        pause = player.GetButtonDown("Pause");
        exit = player.GetButtonDown("Exit");

        // Work Actions
        isAimLeft = player.GetButtonDown("AimLeft");
        Debug.Log(moveVector);
        isAimRight = player.GetButtonDown("AimRight");
        fire = player.GetButtonDown("Fire");
    }

    void ProcessInput() {
        // Store the current mouse movement
        Vector2 input = new Vector2(mouseVector.x, mouseVector.y);
 
        // Smooth the mouse movement by the snappiness factor
        mouseVector = Vector2.Lerp(mouseVector, input, snappiness * Time.deltaTime);
    }
}
