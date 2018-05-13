using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollision : MonoBehaviour
{


    [Range(1, 10)]
    public float jumpVelocity;

    private Rigidbody2D rb2d;

    bool jumpRequest;
    List<Collider2D> groundTouched = new List<Collider2D>();

    // Use this for initialization

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb2d.velocity = new Vector2(moveSpeed,rb2d.velocity.y);
        if (Input.GetButtonDown("Jump") && groundTouched.Count != 0)
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

    private void OnCollisionEnter2D(Collision2D c)
    {
        ContactPoint2D[] points = new ContactPoint2D[2];
        c.GetContacts(points);
        for (int i = 0; i < points.Length; i++)
        {
            if(points[i].normal == Vector2.up && !groundTouched.Contains(c.collider))
            {
                groundTouched.Add(c.collider);
                return;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D c)
    {
        if (groundTouched.Contains(c.collider))
        {
            groundTouched.Remove(c.collider);
        }
    }


}
