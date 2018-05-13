using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingProperties : MonoBehaviour {

    private Rigidbody2D rb2d;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // Use this for initialization

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        if (rb2d.velocity.y < 0 ) // if falling
        {
            rb2d.gravityScale = fallMultiplier;
        }
        else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb2d.gravityScale = lowJumpMultiplier;
        }
        else {
            rb2d.gravityScale = 1f;
        }

    }

}
