using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRaycast : MonoBehaviour {


    [Range(1, 10)]
    public float jumpVelocity;
    public float groundedSkin = 0.05f;
    public LayerMask mask;

    private Rigidbody2D rb2d;

    bool jumpRequest;
    bool grounded;

    Vector2 playerSize;


    // Use this for initialization

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerSize = GetComponent<BoxCollider2D>().size;
    }

    // Update is called once per frame
    void Update()
    {
        //rb2d.velocity = new Vector2(moveSpeed,rb2d.velocity.y);
        if (Input.GetButtonDown("Jump") && grounded)
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
            grounded = false;
        }
        else
        {
            Vector2 rayStart = (Vector2)transform.position + Vector2.down * playerSize.y * 0.5f;
            grounded = Physics2D.Raycast(rayStart, Vector2.down, groundedSkin, mask);
        }
    }
}
