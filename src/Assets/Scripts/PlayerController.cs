using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundLayer;
    
    public float speed = 8f;
    [Range(0, 1)] public float acceleration = 0.1f;

    public float gravity = 25f;
    public float jumpForce = 10f;
    public bool enableDoubleJump = true;
    public float secondJumpForce = 10f;

    private Vector3 direction;
    private bool ableToDoubleJump = true;
    private float currentAccelaration = 0f;

    void Start()
    {
        if (!enableDoubleJump)
            ableToDoubleJump = false;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        {
            currentAccelaration += acceleration * Time.deltaTime;
            if (currentAccelaration > 1)
                currentAccelaration = 1;

            direction.x = horizontalInput * speed * currentAccelaration;
        }
        if (horizontalInput == 0)
            currentAccelaration = 0;

        direction.y -= gravity * Time.deltaTime;

        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
                direction.y = jumpForce;
            else if (!ableToDoubleJump && enableDoubleJump)
                ableToDoubleJump = true;
        }
        else if (Input.GetButtonDown("Jump") && ableToDoubleJump)
        {
            direction.y = secondJumpForce;
            ableToDoubleJump = false;
        }

        controller.Move(direction * Time.deltaTime);
    }
}
