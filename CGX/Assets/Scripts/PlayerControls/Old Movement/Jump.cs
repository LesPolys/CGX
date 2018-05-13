using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{


    [Range(1, 10)]
    public float jumpVelocity;

    private Rigidbody2D rb2d;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    bool jumpRequest;

    // Update is called once per frame
    void Update()
    {
        //rb2d.velocity = new Vector2(moveSpeed,rb2d.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequest = true;
        }
        
    }

    private void FixedUpdate()
    {

        if (jumpRequest)
        {
            //rb2d.velocity += Vector2.up * jumpForce;
            rb2d.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpRequest = false;
        }
    }
}
