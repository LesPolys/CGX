using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Knight : PlayerClass
{

 
    void Update()
    {
        if(grounded)
        if (Input.GetKeyDown(KeyCode.R))
        {
            jumpRequest = true;
            //                        AkSoundEngine.PostEvent("knight_jump", gameObject);
        }

        myAnimator.SetFloat("Speed", rb2d.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
        myAnimator.SetFloat("Jump", rb2d.velocity.y);
    }

 
}
