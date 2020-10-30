using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundLayer;
    
    public float speed = 8f;
    public float gravity = 25f;
    public float jumpForce = 10f;
    public bool enableDoubleJump = true;
    public float secondJumpForce = 10f;

    private Vector3 direction;
    private bool ableToDoubleJump = true;


    // Start is called before the first frame update
    void Start()
    {
        if (!enableDoubleJump)
            ableToDoubleJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        direction.x = horizontalInput * speed;

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
