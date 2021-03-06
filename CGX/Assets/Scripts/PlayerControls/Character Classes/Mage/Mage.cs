﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class Mage : Player
{
    public float abilityRange;
    public float magneticPower;
    public float floatStrength = 10f;
    public float amplitude = 0.5f;

    [SerializeField]
    protected LayerMask partyMask = 0;

	public static event Action mageJumpEvent = null; //events are kind of like a weird list

    Mage()
    {
        actionKey = KeyCode.W;
    }

    public override void PlayRunSound()
    {
        //AkSoundEngine.PostEvent("Mage_Footsteps_Start", gameObject);
    }

    public override void StopRunSound()
    {
       // AkSoundEngine.PostEvent("Mage_Footsteps_Stop", gameObject);
    }

    public override void Animation(int anim)
    {
        switch (anim)
        {

            case 0: // idle
                _animator.Play(Animator.StringToHash("MageIdle"));
                break;
            case 1: //run
                _animator.Play(Animator.StringToHash("MageRun"));
                break;
            case 2: //jump up
                _animator.Play(Animator.StringToHash("MageJump"));
				FireMageJumpvent();
                //AkSoundEngine.PostEvent("Mage_Jump", gameObject);
                break;
            case 3: //fall down
                _animator.Play(Animator.StringToHash("MageFall"));
                break;
            case 4: //ability
                _animator.Play(Animator.StringToHash("MagePower"));
                //AkSoundEngine.PostEvent("Mage_Attack", gameObject);
                break;
            case 5: //fainting
				_animator.Play(Animator.StringToHash("MageHit"));
                break;
            case 6: //just in case
				_animator.Play(Animator.StringToHash("MageFaint"));
                break;



        }
    }

    

    public override void Ability()
    {
       // StopAllCoroutines();

        List<GameObject> partyMembers = new List<GameObject>();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, abilityRange, partyMask);
        if (colliders.Length > 0)
        {
            foreach(Collider2D collider in colliders)
            {
                partyMembers.Add(collider.gameObject);
            }
        }

        partyMembers.Add(this.gameObject);//add the mage
         
        StartCoroutine(FloatParty(partyMembers,this.transform.position.y));
    }

    IEnumerator FloatParty(List<GameObject> partyMembers, float floatingHeight)
    {

        
        

        while (abilityAnimating)
        {
            foreach (GameObject member in partyMembers)
            {
                //floatingHeight += Mathf.Sin(Time.time * floatStrength)* Time.deltaTime;
                Vector3 newPos = new Vector3(member.transform.position.x, floatingHeight + Mathf.Sin(Time.time * floatStrength) * amplitude , member.transform.position.z);
                member.transform.position = Vector3.Lerp(member.transform.position, newPos, magneticPower);

                member.GetComponent<Player>().FloatAgent(-0.1f, -0.1f);
               
                //member.transform.position = newPos;
            }

            yield return null;
        }
      

        yield break;
       


      
    }


	public void FireMageJumpvent(){ // call this to fire the event to all listen
		//if (jumpEvent != null) {// check to see if no one is listening cause that would be embarassing screaming into the void
		//jumpEvent();//fire the event
		//mageJumpEvent.Invoke();//also fires the event but dont need a null check for listeners
		//}
	}

}