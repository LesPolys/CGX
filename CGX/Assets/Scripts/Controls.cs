using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    public float moveSpeed = 1.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector2 movement = Vector2.zero;
    private bool jumpRequest;

    CharacterController2D controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update () {
        movement.x = moveSpeed * Time.deltaTime;
        //re write jump
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.R) )
        {
            movement.y = jumpSpeed;
        }


        //add in falling property style gravity scale

        /*
        if (movement.y < 0) // if falling
        {
            rb2d.gravityScale = fallMultiplier;
        }
        else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb2d.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb2d.gravityScale = 1f;
        }
        */

        movement.y -= gravity * Time.deltaTime;
        print(movement.y);
    }

 

    private void FixedUpdate()
    {

        controller.Move(movement);
    }

}
