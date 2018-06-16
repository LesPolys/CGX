using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent {
	
	// movement config
	[SerializeField]
	protected float gravity = -25f;
   
	[HideInInspector]
	protected float normalizedHorizontalSpeed = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


												//basic move and gravity
		_velocity.y += gravity * Time.deltaTime;
		//print(_velocity.y);
		_controller.move(_velocity * Time.deltaTime);		
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
	

	}
}
