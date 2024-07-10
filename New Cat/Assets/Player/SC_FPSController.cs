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
    [SerializeField] float walkingForce = 10f;
    [SerializeField] float walkingSpeed = 7.5f;

    [Header("Camera Control Variables")]
    [SerializeField] float lookSpeed = 2.0f;
    [SerializeField] float lookXLimit = 45.0f;

    [Header("References")]
    public Camera playerCamera;
    Vector3 moveDirection = Vector3.zero;
    CharacterController cc;
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

        cc = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameManager.pauseGame += PauseHandler;
    }

    void OnEnable() {
        // Debug.Log(InputReader.instance == null);
    }

    void OnDestroy() {
        GameManager.pauseGame -= PauseHandler;
    }

    void Update() {
        RotatePlayer();
    }

    void FixedUpdate() {
        MovePlayer();
    }

    void MovePlayer() {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? walkingSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? walkingSpeed * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Move the controller
        cc.SimpleMove(moveDirection);
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

