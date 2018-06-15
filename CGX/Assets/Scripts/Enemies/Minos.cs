using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minos : Enemy {

	public float losRange;

	bool enraged = false;

	[SerializeField]
	Transform LPP;

	[SerializeField]
	Transform RPP;


	enum MinosStates {PATROL, ENRAGED, CHARGING };
	 

	// Use this for initialization
	void Start () {
		normalizedHorizontalSpeed = -1;//1 for right -1 for left
	}
	
	// Update is called once per frame
	void Update () {
		//DrawRay( transform.position, transform.right * losRange, Color.red );

		if(Input.GetKeyDown(KeyCode.A)){
			ChangeFacing();
		}
		

		if(enraged){
			
			_velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * moveSpeed, Time.deltaTime );
			_velocity.y += gravity * Time.deltaTime;
			_controller.move(_velocity * Time.deltaTime);
			// grab our current _velocity to use as a base for all calculations
			_velocity = _controller.velocity;
		}

	}

	void DrawRay( Vector3 start, Vector3 dir, Color color )
	{
		Debug.DrawRay( start, dir, color );
	}

	public void EnemyInRange(){
		enraged = true;
		moveSpeed = 10;
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

	
}
