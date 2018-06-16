using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minos : Enemy {

	public float losRange;
	public enum MinosStates {IDLE, ENRAGED, WALK, CHARGING };


	MinosStates currentState = MinosStates.IDLE;

	[SerializeField]
	Transform LPP;

	[SerializeField]
	Transform RPP;

	protected float groundDamping = 20f; // how fast do we change direction? higher means faster
	protected float inAirDamping = 5f;



	 

	// Use this for initialization
	void Start () {
		normalizedHorizontalSpeed = -1;//1 for right -1 for left
	}
	
	// Update is called once per frame
	void Update () {
		//DrawRay( transform.position, transform.right * losRange, Color.red );

		

	


		switch (currentState) {

		case MinosStates.IDLE:
			moveSpeed = 0;
			_animator.Play(Animator.StringToHash("MinosIdle"));
			break;

		case MinosStates.WALK:
			moveSpeed = 1;
			_animator.Play(Animator.StringToHash("MinosRun"));
			break;

		case MinosStates.ENRAGED:
			moveSpeed = 5;
//			_animator.Play(Animator.StringToHash("MinosEnraged"));
			_animator.Play(Animator.StringToHash("MinosAttack"));
			break;

		case MinosStates.CHARGING:
			moveSpeed = 5;
				_animator.Play(Animator.StringToHash("MinosAttack"));
			break;

		}


		
		//basic move and gravity
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * moveSpeed, Time.deltaTime * smoothedMovementFactor);
	
		_velocity.y += gravity * Time.deltaTime;
		//print(_velocity.y);
		_controller.move(_velocity * Time.deltaTime);		
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;



	}

	void DrawRay( Vector3 start, Vector3 dir, Color color )
	{
		Debug.DrawRay( start, dir, color );
	}

	public void EnemyInRange(){
		currentState = MinosStates.ENRAGED;
	}


	public void SetState(MinosStates newState){
		currentState = newState;
	}


	void ChangeFacing(){//swap sprite facing and forward movement multipluer
		
		normalizedHorizontalSpeed = normalizedHorizontalSpeed * -1;

		if(normalizedHorizontalSpeed == 1){//face and move right
			transform.rotation = new Quaternion(transform.rotation.x,0,transform.rotation.z, 0);
		}
		if(normalizedHorizontalSpeed == -1){//face and move left
			transform.rotation = new Quaternion(transform.rotation.x,180,transform.rotation.z, 0);
		}
	}


	void OnTriggerEnter2D(Collider2D other)
		//void OnCollisionEnter(Collision hit)
	{
		print ("w");
		if (other.gameObject.tag == "Player")// || hit.gameObject.tag == "Bullet")
		{
			print ("hit");
			other.gameObject.GetComponent<Agent>().KnockBack(5, 10,(other.transform.position - transform.position).normalized);
			return;
		}
		
	
	}

	
}
