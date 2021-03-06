﻿using UnityEngine;
using System.Collections;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class playerController : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public bool respectPhysics;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            moveDirection *= speed;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);


        var list = GameObject.FindObjectsOfType<GameObject>();

        if (respectPhysics == false)
        {            
            foreach (var entry in list)
            {
                if (entry.name.ToLower().Contains("platform") == true)
                {
                    Physics.IgnoreCollision(characterController, entry.GetComponent<Collider>(), characterController.velocity.y > 0);                    
                }
            }
        }
        else
        {
            foreach (var entry in list)
            {
                if (entry.name.ToLower().Contains("platform") == true)
                {
                    Physics.IgnoreCollision(characterController, entry.GetComponent<Collider>(), false);                 
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Enter:" + other.name);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.LogWarning("Stay:" + other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.LogWarning("Exit:" + other.name);
    }
}