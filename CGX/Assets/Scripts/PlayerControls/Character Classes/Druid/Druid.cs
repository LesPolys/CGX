using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Druid : Player
{

    public float attackRange;
    public float rootTime;

    public ObjectPooler[] vinePooler;

    Druid()
    {
        actionKey = KeyCode.Q;
    }

    public override void PlayRunSound()
    {
        AkSoundEngine.PostEvent("Druid_Footsteps_Start", gameObject);
    }

    public override void StopRunSound()
    {
        AkSoundEngine.PostEvent("Druid_Footsteps_Stop", gameObject);
    }



    public override void Animation(int anim)
    {
        switch (anim)
        {

            case 0: // idle
                _animator.Play(Animator.StringToHash("DruidIdle"));
                break;
            case 1: //run
                _animator.Play(Animator.StringToHash("DruidRun"));
                break;
            case 2: //jump up
                _animator.Play(Animator.StringToHash("DruidJump"));
                AkSoundEngine.PostEvent("Druid_Jump", gameObject);
                break;
            case 3: //fall down
                _animator.Play(Animator.StringToHash("DruidFall"));
                break;
            case 4: //ability
                _animator.Play(Animator.StringToHash("DruidPower"));
                AkSoundEngine.PostEvent("Druid_Attack", gameObject);
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
     
          
                for (int i = 0; i < colliders.Length; i ++)
                {

				if(colliders[i].gameObject.GetComponent<Agent>().IsGrounded()){
					GameObject newVine = vinePooler[0].GetPooledObject();

					newVine.transform.position =  colliders[i].transform.position;


					newVine.SetActive(true);
					newVine.gameObject.GetComponent<Vine>().Grow();
					colliders[i].gameObject.GetComponent<Enemy>().Root(0, rootTime);
				}
                   

                }
              
                
        }

    }

    public void SpawnVine(GameObject target)
    {
       
    }


}