using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Maze_Player : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    Vector2 currentInput = new Vector2(0f, 0f);

    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        GetInput();
    }

    void FixedUpdate() {
        MovePlayer();
    }

    void GetInput() {
        currentInput = InputReader.instance.maze_moveVector;
    }

    void MovePlayer() {
        rb.MovePosition((Vector2)transform.position + (currentInput * speed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles")) {
            Debug.Log("hit wall");
        }
    }
}
