using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ranger : PlayerClass
{


    void Update()
    {
        if (grounded)
            if (Input.GetKeyDown(KeyCode.E))
            {
                jumpRequest = true;

            }

        myAnimator.SetFloat("Speed", rb2d.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
        myAnimator.SetFloat("Jump", rb2d.velocity.y);
    }


}

