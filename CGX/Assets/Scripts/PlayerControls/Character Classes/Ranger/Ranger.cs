using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Player
{

    public GameObject arrowPrefab;
    public int numArrows;

    public float attackRange;


    Ranger()
    {
        actionKey = KeyCode.E;
    }


    public override void Animation(int anim)
    {
        switch (anim)
        {

            case 0: // idle
                _animator.Play(Animator.StringToHash("RangerIdle"));
                AnimationEnd();
                break;
            case 1: //run
                _animator.Play(Animator.StringToHash("RangerRun"));
               // AkSoundEngine.PostEvent("Ranger_Footsteps", gameObject);
                AnimationEnd();
                break;
            case 2: //jump up
                _animator.Play(Animator.StringToHash("RangerJump"));
                //AkSoundEngine.PostEvent("Ranger_Jump", gameObject);
                break;
            case 3: //fall down
                _animator.Play(Animator.StringToHash("RangerFall"));
                break;
            case 4: //ability
                _animator.Play(Animator.StringToHash("RangerPower"));
                //AkSoundEngine.PostEvent("Ranger_Attack", gameObject);
                break;
            case 5: //fainting
                break;
            case 6: //just in case
                break;



        }
    }
    
     


    public override void Ability()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyMask);
        if (colliders.Length > 0)
        {
            Shoot(colliders[colliders.Length-1].transform);
        }

    }


    void Shoot(Transform target)
    {
       
        var arrow = (GameObject)Instantiate(
            arrowPrefab,
            transform.position,
            Quaternion.identity);

       
        //arrow.GetComponent<Rigidbody>().velocity = arrow.transform.forward * 6;
        arrow.GetComponent<Arrow>().Target(target);

        // Destroy the bullet after 2 seconds
        Destroy(arrow, 2.0f);
    }


  

}