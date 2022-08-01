using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Inputs
    Vector2 moveInput;

    // Components
    Rigidbody2D rb2D;

    // variables
    [SerializeField] float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Components
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Player moving input
    private void OnMove(InputValue value)
    {
        // get input vector2
        moveInput = value.Get<Vector2>();

        // move player
        Run();
    }

    // move player
    private void Run()
    {
        // update rigidbody velocity
        rb2D.velocity = moveInput * moveSpeed;
    }

    private void OnReplay()
    {
        FindObjectOfType<GameManager>().OnReplayLevel();
    }
}
