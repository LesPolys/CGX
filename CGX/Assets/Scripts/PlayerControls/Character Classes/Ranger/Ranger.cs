using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Player
{

    //public GameObject arrowPrefab;
	//public GameObject crossHair;
    public int numArrows;

    public float attackRange;



    public ObjectPooler[] rangerPooler;


    private GameObject currentTarget;


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
                AnimationEnd();
                break;
            case 1: //run
                _animator.Play(Animator.StringToHash("RangerRun"));
                AnimationEnd();
                break;
            case 2: //jump up
                _animator.Play(Animator.StringToHash("RangerJump"));
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
                break;
            case 6: //just in case
                break;



        }
    }
    
     


    public override void Ability()
    {


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
			arrow.GetComponent<Arrow>().SetTarget(currentTarget.transform);
        }

    }

	IEnumerator CurrentTargetDisplay(){

		while (true) {
			
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyMask);
			if (colliders.Length > 0) {
				//currentTarget = colliders [colliders.Length - 1].gameObject;

				float smallestDistance = 1000.0f;//absurd starting value


				foreach (Collider2D collider in colliders) { // sort by closest
					
					float distance = Vector3.Distance (collider.transform.position, transform.position);

					if(distance < smallestDistance){
						smallestDistance = distance;
						currentTarget = collider.gameObject;
					}

				}


				//MoveCrossHair (currentTarget.transform.position);
			} else {
				currentTarget = null;
			}

            /*
			if (currentTarget != null) {
				crossHair.SetActive (true);
			} else {
				crossHair.SetActive (false);
			}*/

			yield return null;
		}
		yield break;
	}

	/*public void MoveCrossHair(Vector3 targetPos){
		crossHair.transform.position = Vector3.Lerp(crossHair.transform.position, targetPos, 0.5f);
	}*/


}