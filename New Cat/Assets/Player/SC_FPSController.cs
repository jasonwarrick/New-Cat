using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    Code adapted from: https://www.sharpcoderblog.com/blog/unity-3d-fps-controller
**/

// [RequireComponent(typeof(CharacterController))]
public class SC_FPSController : MonoBehaviour
{
    public static SC_FPSController instance;

    [Header("Movement Variables")]
    [SerializeField] float walkingSpeed = 7.5f;

    [Header("Camera Control Variables")]
    [SerializeField] float lookSpeed = 2.0f;
    [SerializeField] float lookXLimit = 45.0f;

    [Header("References")]
    public Camera playerCamera;
    Rigidbody rb;
    Vector3 moveDirection = Vector3.zero;
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

        rb = GetComponent<Rigidbody>();

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
        MovePlayer();
        RotatePlayer();
    }

    void RotatePlayer() {
        // Player and Camera rotation
        if (canLook) {
            rotationX += -InputReader.instance.mouseVector.y * lookSpeed; // Adjust the mouse input by the look speed
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit); // Make sure the rotation doesn't exceed the look limit
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0); // Rotate the camera around the x axis
            transform.rotation *= Quaternion.Euler(0, InputReader.instance.mouseVector.x * lookSpeed, 0); // Rotate the player around the y axis
        }
    }

    void MovePlayer() {
        Vector2 baseMoveVector = InputReader.instance.moveVector.normalized; // Normalize the input vector

        // Get the current forward and right vectors
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Get the intended x and (z) speeds
        float curSpeedX = walkingSpeed * baseMoveVector.y;
        float curSpeedY = walkingSpeed * baseMoveVector.x;

        // Align those speeds to the forward and right vectors, and maintain y velocity
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y = rb.velocity.y;

        // Set the current velocity to the move direction
        rb.velocity = moveDirection;
    }

    // Store the player state and lock them
    public void LockPlayer() {
        moveState = canMove;
        lookState = canLook;

        canMove = false;
        canLook = false;
    }

    // Restore the player to their previous state
    public void UnlockPlayer() {
        canMove = moveState;
        canLook = lookState;
    }

    void PauseHandler(bool isPaused) {
        if (GameManager.instance.isInMinigame) { return; }
        
        if (isPaused) {
            LockPlayer();
        } else {
            UnlockPlayer();
        }
    }
}