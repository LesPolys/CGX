using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : Enemy {


	public float frequency;
	public float magnitude;


	public float attackRange;
	public float sightDistance;


	[Range(-90.0f,90.0f)]
	public float angle;

	[SerializeField]
	private Transform fireBallSpawnPoint;
	public ObjectPooler[] fireBallPooler;


	// Use this for initialization
	void Start () {
		normalizedHorizontalSpeed = -1;//1 for right -1 for left

	}
	
	// Update is called once per frame
	void Update () {

		//basic move and gravity
		//var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?

		DrawTargetLine ();

	


		_velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * moveSpeed, Time.deltaTime );
		
		_velocity.y = Mathf.Sin (Time.time * frequency) * magnitude;
		//print(_velocity.y);
		_controller.move(_velocity * Time.deltaTime);		
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;

	}

	public void SpawnFireBall(Vector3 target){
		GameObject fireBall = fireBallPooler[0].GetPooledObject();
		fireBall.transform.position = fireBallSpawnPoint.position;
		fireBall.SetActive(true);
		fireBall.GetComponent<FireBall>().PlayFireBallGrow();
		fireBall.GetComponent<FireBall>().SetTarget(target);
	}


	public void DrawTargetLine(){

		float x =  0.65f * normalizedHorizontalSpeed * Mathf.Cos(Mathf.Deg2Rad * angle ) ;
		float y =  0.65f * Mathf.Sin (Mathf.Deg2Rad * angle) ;

		Vector3 circlePoint = new Vector3(x, y , 0.0f) ;

		DrawRay( circlePoint + transform.position , circlePoint * attackRange  , Color.red);
		
		DrawRay(circlePoint + transform.position + new Vector3( sightDistance * normalizedHorizontalSpeed , 0.0f ,0.0f) , (circlePoint * attackRange ) , Color.yellow);

		RaycastHit2D hit = Physics2D.Raycast(circlePoint + transform.position + new Vector3( sightDistance * normalizedHorizontalSpeed , 0.0f ,0.0f), (circlePoint ), attackRange);
		if (hit.collider != null && hit.collider.gameObject.tag == "Player"){
				SpawnFireBall(circlePoint);
		}
	}

	void DrawRay( Vector3 start, Vector3 dir, Color color )
	{
		Debug.DrawRay( start, dir, color );
	}


}
