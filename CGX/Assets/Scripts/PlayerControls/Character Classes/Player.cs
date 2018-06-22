using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player : Agent
{

    // movement config
    [SerializeField]
    protected float gravity = -25f;
    [SerializeField]
    protected float jumpStartMultiplier = -25f;
    [SerializeField]
    protected float jumpEndMultiplier = -25f;
    [SerializeField]
    protected float jumpHeight = 3f;

    [HideInInspector]
    protected float normalizedHorizontalSpeed = 0;


    protected RaycastHit2D _lastControllerColliderHit;

    protected KeyCode actionKey;
    private bool isPlayingRunSound;

    /// <summary>
    /// mask with all layers that trigger events should fire when intersected
    /// </summary>
    /// 
    [SerializeField]
    protected LayerMask enemyMask = 0;


    protected bool abilityAnimating;


    protected enum PlayerState {IDLE, RUNNING, JUMPING, FALLING, ABILITY };
    protected  PlayerState currentState;
    
	[SerializeField]
	protected Transform partyPosition;

	private bool jumpSignal;

	private float speedOffset;
	public  float alteredMoveSpeed;
	private float acceptableDistance;



	[SerializeField]
	protected float knowbackDur;

	[SerializeField]
	protected float power;





	/*///////INSPECTOR SHIT///////////////////////////////////////
	[Serializable]
	private class MyCustomEventClass : UnityEngine.Events.UnityEvent{ // make this shit in the inspector for fun

	};

	[SerializeField]
	private MyCustomEventClass myEvent = null;
	*/
	////////////////////////////////////////////////////////////


    void Update()
    {

		//myEvent.Invoke ("TEST");
		/*

		if(Input.GetKeyDown(KeyCode.UpArrow)){
			SetJumpSignal(true);

		}

		if(Input.GetKeyDown(KeyCode.DownArrow)){
			
			//gameObject.GetComponent<Agent>().KnockBack(knockBackTestx,knockBackTesty,);
			
		}*/
		

		if (partyPosition != null && _controller.isGrounded) {
		

			//print (partyPosition);
			//print(partyPosition.transform.position);

			if (transform.position.x < partyPosition.position.x) {

				alteredMoveSpeed = moveSpeed + speedOffset;

			} else if (transform.position.x > partyPosition.position.x) {

				alteredMoveSpeed = moveSpeed - speedOffset;
			}


			if(Mathf.Abs(partyPosition.transform.position.x - transform.position.x) < acceptableDistance){
				alteredMoveSpeed = moveSpeed;
			}


		} else {
			alteredMoveSpeed = moveSpeed;
		}


		if (_controller.isGrounded)
			_velocity.y = 0;


		if(jumpSignal){
			// we can only jump whilst grounded
			if (_controller.isGrounded)
			{
				_velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
				currentState = PlayerState.JUMPING;
				Animation(2); //jump
				//jump sound
			}
			
			if (!_controller.isGrounded)
			{
				currentState = PlayerState.ABILITY;
				Animation(4);
				abilityAnimating = true;
				Ability();
				//shoot sound
				
			}

			SetJumpSignal(false);
		}
		
        normalizedHorizontalSpeed = 1;
        if (transform.localScale.x < 0f)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        if( _controller.isGrounded && moveSpeed > 0)
        {
            currentState = PlayerState.RUNNING;
            Animation(1); //run anim
            // run sound

        }

		if( _controller.isGrounded && moveSpeed <= 0)
		{
			currentState = PlayerState.IDLE;
			Animation(0); //run anim
			// run sound
			
		}


        //sound toggle
        if (_controller.isGrounded && !isPlayingRunSound)
        {
            PlayRunSound();
            isPlayingRunSound = true;

        }
        else if (!_controller.isGrounded)
        {
            StopRunSound();
            isPlayingRunSound = false;
        }
		
        if (!_controller.isGrounded && _velocity.y < 0 && !abilityAnimating)
        {
            currentState = PlayerState.FALLING;
            Animation(3);
        }
		
      
		_velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * alteredMoveSpeed, Time.deltaTime );

        // apply gravity before moving

        if (_velocity.y < 0) // if falling
        {
            gravity = jumpEndMultiplier;
        }
        else if (_velocity.y > 0 && Input.GetKey(actionKey))
        {
            gravity = jumpStartMultiplier;
        }
        


        _velocity.y += gravity * Time.deltaTime;
        //print(_velocity.y);

      

        _controller.move(_velocity * Time.deltaTime);

        // grab our current _velocity to use as a base for all calculations
        _velocity = _controller.velocity;
    }

    public virtual void Ability()
    {
        //the players jump ability
    }

    public virtual void Animation(int anim)
    {

    }


    public virtual void PlayRunSound()
    {

    }
    public virtual void StopRunSound()
    {

    }

    public void AnimationEnd()
    {
        abilityAnimating = false;
    }

    public void changeGravity(float newGravity)
    {
        gravity = newGravity;
    }


    public void FloatAgent(float newYVelocity, float newGravity)
    {
        changeYVelocity(newYVelocity);
        changeGravity(newGravity);

    }


	public void SetPartyPosition(Transform targetPos){
		partyPosition = targetPos;
	}


	public void SetJumpSignal(bool signalState){
		jumpSignal = signalState;
	}

	public void ChangeMoveSpeed(float newSpeed){
		SetMovementSpeed (newSpeed);
	}

	public void SetSpeedOffsets(float nSpeed){
		speedOffset = nSpeed;
	
	}
	public void SetAcceptableDistance(float newDistance){
		acceptableDistance = newDistance;
	}



	
}

