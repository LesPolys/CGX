using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 2.0F;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Vector2 moveDirection = Vector2.zero;
    private bool jumpRequest;

    CharacterController2D controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update () {
 
    }

 

    private void FixedUpdate()
    {
            if (controller.isGrounded) {
            moveDirection = new Vector3(1, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
            
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
 

}
