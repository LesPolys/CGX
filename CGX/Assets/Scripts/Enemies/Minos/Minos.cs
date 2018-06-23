using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minos : Enemy {

	public float losFrontRange;
	public float losBackRange;

	public float chargeSpeed;

	public enum MinosStates {IDLE, ENRAGED, WALK, CHARGING, HIT, DEAD };


	MinosStates currentState = MinosStates.IDLE;

	[SerializeField]
	Transform LPP;

	[SerializeField]
	Transform RPP;

	protected float groundDamping = 20f; // how fast do we change direction? higher means faster
	protected float inAirDamping = 5f;


	public float xknockback;
	public float yknockback;
	 

	Vector3 startPos;
	public float resetDistance;


	protected override void Awake(){
		base.Awake ();
		startPos = transform.position;
	}

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

			ScanForEnemies();
			_animator.Play(Animator.StringToHash("MinosIdle"));
			break;

		case MinosStates.WALK:
			moveSpeed = 1;

			ScanForEnemies();
	

			//1 for right -1 for left
			if(normalizedHorizontalSpeed > 0.0f){
				if(transform.position.x >= RPP.position.x){
					SetState(MinosStates.IDLE);
				}
			}else if(normalizedHorizontalSpeed < 0.0f){
			
				if(transform.position.x <= LPP.position.x){
					SetState(MinosStates.IDLE);
				}
			}

			_animator.Play(Animator.StringToHash("MinosRun"));
			break;

		case MinosStates.ENRAGED:
			moveSpeed = 0;
//			_animator.Play(Animator.StringToHash("MinosEnraged"));
			_animator.Play(Animator.StringToHash("MinosAlert"));
			break;

		case MinosStates.CHARGING:
			moveSpeed = chargeSpeed;
			_animator.Play(Animator.StringToHash("MinosAttack"));

			break;

		case MinosStates.HIT:
			//moveSpeed = chargeSpeed;
			_animator.Play(Animator.StringToHash("MinosHit"));
			
			break;
		case MinosStates.DEAD:
			//moveSpeed = chargeSpeed;
			_animator.Play(Animator.StringToHash("MinosDead"));
			
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

		//print ("Trans" + transform.position);
		//print ("Start" + startPos);

		if(Vector3.Distance(transform.position, startPos) > resetDistance){//if minos has moved to far away from his starting point then reset him to the start point, rresety his velocity to nothing and set his state back to the default
			//print ("toobig");
			ResetMinos();
		}


	}

	public void Scream(){
		AkSoundEngine.PostEvent("Minotaur_Attack", gameObject);
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

	public void ScanForEnemies(){

		DrawRay(transform.position + new Vector3((0.15f * transform.right).x,0.1428f/2.0f,0), transform.right * losFrontRange, Color.red);
		if (Physics2D.Raycast(transform.position + new Vector3((0.15f * transform.right).x,0.1428f/2.0f,0), transform.right, losFrontRange)){
			SetState(MinosStates.ENRAGED);
		}
		
		
		DrawRay(transform.position + new Vector3((-0.15f * transform.right).x ,0.1428f/2.0f,0), -1 * transform.right * losBackRange, Color.blue);
		if (Physics2D.Raycast(transform.position + new Vector3((-0.15f * transform.right).x ,0.1428f/2.0f,0), -1 * transform.right, losBackRange)){
			ChangeFacing();
			SetState(MinosStates.ENRAGED);
		}
	}




	void OnTriggerEnter2D(Collider2D other)
		//void OnCollisionEnter(Collision hit)
	{
		//print ("w");
		if (other.gameObject.tag == "Player")// || hit.gameObject.tag == "Bullet")
		{
		//	print ("hit");
			//other.gameObject.GetComponent<Agent>().KnockBack(xknockback, yknockback,(other.transform.position.x - transform.position.x));
			//other.gameObject.GetComponent<Agent>().KnockBack(xknockback, yknockback,(other.transform.position - transform.position));
			other.gameObject.GetComponent<Agent>().Damage(1);
			other.gameObject.GetComponent<Player>().HitAnim();
			return;
		}
		
	
	}

	void ResetMinos(){

		transform.position = startPos;
		currentState = MinosStates.WALK;
		//_velocity = 0.0f;

	}

	public void Disable(){
		gameObject.SetActive (false);
	}

	
}
