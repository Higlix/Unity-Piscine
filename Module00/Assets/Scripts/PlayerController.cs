using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    bool isJumping;

    Vector3 moveDirection;

    Rigidbody rb;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true; 
        }
        else
        {
            moveDirection.y = 0;
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        Debug.Log(moveDirection);
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f);
        if (isJumping)
        {
            rb.AddForce(transform.up * 10f, ForceMode.Impulse);
            isJumping = false;

        }
    }
}