using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Druid : Player
{

    public float attackRange;

    Druid()
    {
        actionKey = KeyCode.Q;
    }


    public override void Animation(int anim)
    {
        switch (anim)
        {

            case 0: // idle
                _animator.Play(Animator.StringToHash("DruidIdle"));
                AnimationEnd();
                break;
            case 1: //run
                _animator.Play(Animator.StringToHash("DruidRun"));
               // AkSoundEngine.PostEvent("Druid_Footsteps", gameObject);
                AnimationEnd();
                break;
            case 2: //jump up
                _animator.Play(Animator.StringToHash("DruidJump"));
               // AkSoundEngine.PostEvent("Druid_Jump", gameObject);
                break;
            case 3: //fall down
                _animator.Play(Animator.StringToHash("DruidFall"));
                break;
            case 4: //ability
                _animator.Play(Animator.StringToHash("DruidPower"));
                //AkSoundEngine.PostEvent("Druid_Attack", gameObject);
                break;
            case 5: //fainting
                break;
            case 6: //just in case
                break;



        }
    }


    public override void Ability()//find all the enemies in range and root them (Spawn a group of vines, )
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyMask);
        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders){
               // SpawnVine(collider.gameObject);
               // collider.gameObject.
            }   
        }

    }

    public void SpawnVine(GameObject target)
    {
        //Instantiate(vinesPrefab, target.position);
    }


}