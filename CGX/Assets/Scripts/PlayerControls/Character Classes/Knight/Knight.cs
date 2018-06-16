using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Player
{


    public ObjectPooler[] shockWavePooler;

	[SerializeField]
	private Transform shockWaveSpawnPoint;

	private bool isSlamming = false;

    Knight()
    {
        actionKey = KeyCode.R;
    }

    public override void PlayRunSound()
    {
        AkSoundEngine.PostEvent("Knight_Footsteps_Start", gameObject);
    }

    public override void StopRunSound()
    {
        AkSoundEngine.PostEvent("Knight_Footsteps_Stop", gameObject);
    }

    public override void Animation(int anim)
    {
        switch (anim)
        {

            case 0: // idle
                _animator.Play(Animator.StringToHash("KnightIdle"));
                AnimationEnd();
                break;
            case 1: //run

				if(isSlamming){
					abilityAnimating = false;
					isSlamming = false;
					SpawnShockWave();
				}

                _animator.Play(Animator.StringToHash("KnightRun"));
                AnimationEnd();

                break;
            case 2: //jump up
                _animator.Play(Animator.StringToHash("KnightJump"));
                AkSoundEngine.PostEvent("Knight_Jump", gameObject);
                break;
            case 3: //fall down
                _animator.Play(Animator.StringToHash("KnightFall"));
                break;
            case 4: //ability
                _animator.Play(Animator.StringToHash("KnightPower"));
                AkSoundEngine.PostEvent("Knight_Attack", gameObject);
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

	public void KnightSlam(){
		isSlamming = true;
		_animator.Play(Animator.StringToHash("KnightSlam"));
	}


    public void SpawnShockWave()
    {
       
        GameObject shockwave = shockWavePooler[0].GetPooledObject();

		shockwave.transform.position = shockWaveSpawnPoint.position;// transform.position;
        shockwave.SetActive(true);
    }

	void OnTriggerEnter2D(Collider2D collider){

		print ("TEST");

		if(abilityAnimating && isSlamming){

		}

	}

}