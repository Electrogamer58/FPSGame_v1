using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public CharacterController controller;
    public float _defaultMoveSpeed;
    public float _moveSpeed = 12f;
    public float _sprintSpeed = 18f;
    public float _gravity = -9.81f;
    public float _jumpHeight = 3f;

    public Transform groundCheck;
    public float _groundDistance = -0.4f; //radius of sphere
    public LayerMask groundMask; //checks for collision with the floor specifically, in case it catches player collision first, which it will

    Vector3 velocity;
    bool isGrounded;
    bool isSprinting;

    private void Start()
    {
        _defaultMoveSpeed = _moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, _groundDistance, groundMask); //checks for collision with floor using a small invisible sphere; returns true/false

        if (isGrounded && velocity.y < 0) //when touching the ground, AND when velocity is at all greater than 0 (meaning player is being pushed by gravity), reset the velocity
        {
            velocity.y = -2f; //sticks player to ground
        }

        sprint();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 _moveDirection = transform.right * x + transform.forward * z; //find the move direction based on axis buttons pressed times their respective transforms
        controller.Move(_moveDirection * _moveSpeed * Time.deltaTime);

        velocity.y += _gravity * Time.deltaTime; //need a velocity variable to simulate real gravity
        controller.Move(velocity * Time.deltaTime); //multiply times deltaTime twice, as is shown on velocity equation
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity); //force required to jump according to physics. (Square root of jump height x (-2) x gravity)
        }

        
    }

    void sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            Debug.Log("sprintin time!");
            _moveSpeed = _sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("walkin time!");
            _moveSpeed = _defaultMoveSpeed;
        }
    }
}
