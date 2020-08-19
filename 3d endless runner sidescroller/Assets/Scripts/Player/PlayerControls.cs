using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public CharacterController controller;

    public Vector3 moveDir;
    public float gravScale;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDir.y, Input.GetAxis("Vertical") * moveSpeed);

        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            moveDir.y = jumpSpeed;
        }

        moveDir.y = moveDir.y + (Physics.gravity.y * gravScale);
        controller.Move(moveDir * Time.deltaTime);  
    }

}

