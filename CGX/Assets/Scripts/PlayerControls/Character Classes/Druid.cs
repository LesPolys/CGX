using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Druid : PlayerClass
{


    void Update()
    {
        if (grounded)
            if (Input.GetKeyDown(KeyCode.Q))
            {
                jumpRequest = true;

            }

        myAnimator.SetFloat("Speed", rb2d.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
        myAnimator.SetFloat("Jump", rb2d.velocity.y);
    }


}

