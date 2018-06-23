using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Druid : Player
{

    public float attackRange;
    public float rootTime;

    public ObjectPooler[] vinePooler;

	public static event Action druidJumpEvent = null; //events are kind of like a weird list

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
				FireDruidJumpEvent();
                AkSoundEngine.PostEvent("Druid_Jump", gameObject);
                break;
            case 3: //fall down
                _animator.Play(Animator.StringToHash("DruidFall"));
                break;
            case 4: //ability
                _animator.Play(Animator.StringToHash("DruidPower"));
                AkSoundEngine.PostEvent("Druid_Attack", gameObject);
                break;
            case 5: //hit
				_animator.Play(Animator.StringToHash("DruidHit"));    
                break;
			case 6: ////fainting
				_animator.Play(Animator.StringToHash("DruidFaint"));              
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

				print (colliders[i].gameObject.tag);

				if(colliders[i].gameObject.GetComponent<Agent>().IsGrounded()){
					print (colliders[i].gameObject.tag);

					GameObject newVine = vinePooler[0].GetPooledObject();

					newVine.transform.position =  colliders[i].gameObject.transform.GetChild(0).position;


					newVine.SetActive(true);
					newVine.gameObject.GetComponent<Vine>().Grow();
					colliders[i].gameObject.GetComponent<Enemy>().Root(0, rootTime);
				}
                   

                }
              
                
        }

    }

	public void FireDruidJumpEvent(){ // call this to fire the event to all listen
		//if (jumpEvent != null) {// check to see if no one is listening cause that would be embarassing screaming into the void
		//jumpEvent();//fire the event
		//druidJumpEvent.Invoke();//also fires the event but dont need a null check for listeners
		//}
	}




}