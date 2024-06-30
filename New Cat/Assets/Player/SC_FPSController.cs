using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    Code adapted from: https://www.sharpcoderblog.com/blog/unity-3d-fps-controller
**/

[RequireComponent(typeof(CharacterController))]
public class SC_FPSController : MonoBehaviour
{
    public static SC_FPSController instance;

    [Header("Movement Variables")]
    [SerializeField] float walkingSpeed = 7.5f;
    [SerializeField] float runningSpeed = 11.5f;
    [SerializeField] float gravity = 20.0f;
    

    [Header("Camera Control Variables")]
    [SerializeField] float lookSpeed = 2.0f;
    [SerializeField] float lookXLimit = 45.0f;

    [Header("References")]
    public Camera playerCamera;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 adjustedMoveVector = Vector2.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    public bool canLook = true;
    bool moveState = true;
    bool lookState = true;

    void Awake() {
        instance = this;

        if (CameraManager.instance != null) {
            CameraManager.instance.DisableNPCamera();
        }

        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameManager.pauseGame += PauseHandler;
    }

    void OnEnable() {
        Debug.Log(InputReader.instance == null);
    }

    void OnDestroy() {
        GameManager.pauseGame -= PauseHandler;
    }

    void Update() {
        // Realign forward and right vectors based on current direction
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        
        bool isRunning = Input.GetKey(KeyCode.LeftShift); // Press Left Shift to run

        adjustedMoveVector = InputReader.instance.moveVector.normalized; // Normalize the movement vector

        // Set the current speed based on whether or not the player can move and is running (if can move => if is running => set walk or run speed => if not moving, set speed to zero)
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * adjustedMoveVector.y : 0; 
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * adjustedMoveVector.x : 0;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY); // Set the direction of movement to the proper direciton at the current speed

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded) {
            moveDirection.y -= gravity * Time.deltaTime;
        } else { // Stop y-movement if the player is grounded
            moveDirection.y = 0f;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canLook) {
            rotationX += -InputReader.instance.mouseVector.y * lookSpeed; // Adjust the mouse input by the look speed
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit); // Make sure the rotation doesn't exceed the look limit
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0); // Rotate the camera around the x axis
            transform.rotation *= Quaternion.Euler(0, InputReader.instance.mouseVector.x * lookSpeed, 0); // Rotate the player around the y axis
        }

        // Debug.Log(moveDirection);
    }

    // Store the player state and lock them
    public void LockPlayer() {
        if (canMove == false && canLook == false) { 
            UnlockPlayer();
            return;
        }
        moveState = canMove;
        lookState = canLook;

        canMove = false;
        canLook = false;
    }

    // Restore the player to their previous state
    void UnlockPlayer() {
        canMove = moveState;
        canLook = lookState;
    }

    void PauseHandler(bool isPaused) {
        if (isPaused) {
            LockPlayer();
        } else {
            UnlockPlayer();
        }
    }
}