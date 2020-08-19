using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float smoothTurning = 0.1f;
    float smoothTurningVelocity;
    public float gravity = -9.81f;
    Vector3 velocity;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public LayerMask wall;
    public float wallrunForce, maxWallrunTime, maxWallrunSpeed;
    bool isWallRight, isWallLeft;
    bool isWallrunning;
    public float maxWallrunCameraTilt, wallrunCameraTilt;
    public Rigidbody rb;
    public Transform playerCam;
    public Transform orientation;


    // Update is called once per frame
    void Update()
    {
        //CheckForWall();
      //  WallrunInput();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); 

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurningVelocity, smoothTurning);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * speed * Time.deltaTime);

            /*Walljump
            if(isWallLeft && !Input.GetKey(KeyCode.D) || isWallRight && !Input.GetKey(KeyCode.A))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            } */
        }
        
    }
    /*
    private void WallrunInput()
    {
        if (Input.GetKey(KeyCode.D) && isWallRight) StartWallrun();
        if (Input.GetKey(KeyCode.A) && isWallLeft) StartWallrun();

    }
    private void StartWallrun()
    {
        rb.useGravity = false;
        isWallrunning = true;

        if(rb.velocity.magnitude <= maxWallrunSpeed)
        {
            rb.AddForce(orientation.forward * wallrunForce * Time.deltaTime);

            if(isWallRight)
            {
                rb.AddForce(orientation.right * wallrunForce / 5 * Time.deltaTime);
            }
            else
            {
                rb.AddForce(-orientation.right * wallrunForce / 5 * Time.deltaTime);

            }
        }
    }
    private void StopWallrun()
    {
        rb.useGravity = true;
        isWallrunning = false;
    }
    private void CheckForWall()
    {
        isWallRight = Physics.Raycast(transform.position, orientation.right, 1f, wall);
        isWallLeft = Physics.Raycast(transform.position, -orientation.right, 1f, wall);

        if (!isWallRight && !isWallLeft) StopWallrun();
    }
    */
}
