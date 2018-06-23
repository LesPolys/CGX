using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ranger : Player
{

    //public GameObject arrowPrefab;
	//public GameObject crossHair;
    public int numArrows;

    public float attackRange;



    public ObjectPooler[] rangerPooler;


    private GameObject currentTarget;

	public static event Action rangerJumpEvent = null; //events are kind of like a weird list


    Ranger()
    {
        actionKey = KeyCode.E;

    }

	void Start(){
		StartCoroutine (CurrentTargetDisplay ());
	}



    public override void PlayRunSound()
    {
        AkSoundEngine.PostEvent("Ranger_Footsteps_Start", gameObject);
    }

    public override void StopRunSound()
    {
        AkSoundEngine.PostEvent("Ranger_Footsteps_Stop", gameObject);
    }



    public override void Animation(int anim)
    {
        switch (anim)
        {

            case 0: // idle
                _animator.Play(Animator.StringToHash("RangerIdle"));
				break;
            case 1: //run
                _animator.Play(Animator.StringToHash("RangerRun"));
                break;
            case 2: //jump up
                _animator.Play(Animator.StringToHash("RangerJump"));
				FireRangerJumpEvent();
                AkSoundEngine.PostEvent("Ranger_Jump", gameObject);
                break;
            case 3: //fall down
                _animator.Play(Animator.StringToHash("RangerFall"));
                break;
            case 4: //ability
				//check if no enemy?
                _animator.Play(Animator.StringToHash("RangerPower"));
                AkSoundEngine.PostEvent("Ranger_Attack", gameObject);
                break;
            case 5: //fainting
				_animator.Play(Animator.StringToHash("RangerHit"));
                break;
            case 6: //just in case
				_animator.Play(Animator.StringToHash("RangerFaint"));
                break;



        }
    }
    
     



	public void DisplayCrosshair(){
		if (currentTarget != null) {
			GameObject crosshair = rangerPooler [1].GetPooledObject ();
			crosshair.SetActive (true);
			crosshair.GetComponent<Crosshair> ().AssignTarget (currentTarget);
			crosshair.GetComponent<Crosshair> ().PlayZoomIn ();
		}
	}


    void Shoot()
    {
        if (currentTarget != null)
        {
            GameObject arrow = rangerPooler[0].GetPooledObject();
			arrow.transform.position = transform.position;
            arrow.SetActive(true);
			arrow.GetComponent<Arrow>().Animate();
			arrow.GetComponent<Arrow>().SetTarget(currentTarget.transform);
        }

    }

	IEnumerator CurrentTargetDisplay(){

		while (true) {
			
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyMask);
			currentTarget = null;

			if (colliders.Length > 0) {

				float smallestDistance = 1000.0f;//absurd starting value


				foreach (Collider2D collider in colliders) { // sort by closest

					if(collider.transform.position.x > transform.position.x){
						float distance = Vector3.Distance (collider.transform.position, transform.position);
						
						if(distance < smallestDistance){
							smallestDistance = distance;
							currentTarget = collider.gameObject;
						}
					}
				}
			} 
			yield return null;
		}
		yield break;
	}

	public void FireRangerJumpEvent(){ // call this to fire the event to all listen
		//if (jumpEvent != null) {// check to see if no one is listening cause that would be embarassing screaming into the void
		//jumpEvent();//fire the event
		//rangerJumpEvent.Invoke();//also fires the event but dont need a null check for listeners
		//}
	}



}