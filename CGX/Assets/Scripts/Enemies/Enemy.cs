using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent {
	
	// movement config
	[SerializeField]
	protected float gravity = -25f;
   
	[HideInInspector]
	protected float normalizedHorizontalSpeed = 1;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		_velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * moveSpeed, Time.deltaTime );
												//basic move and gravity
		_velocity.y += gravity * Time.deltaTime;
		//print(_velocity.y);
		_controller.move(_velocity * Time.deltaTime);		
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;

	}

	protected void ChangeFacing(){//swap sprite facing and forward movement multipluer
		
		normalizedHorizontalSpeed = normalizedHorizontalSpeed * -1;
		
		if(normalizedHorizontalSpeed == 1){//face and move right
			transform.rotation = new Quaternion(transform.rotation.x,0,transform.rotation.z, 0);
		}
		if(normalizedHorizontalSpeed == -1){//face and move left
			transform.rotation = new Quaternion(transform.rotation.x,180,transform.rotation.z, 0);
		}
	}

}
