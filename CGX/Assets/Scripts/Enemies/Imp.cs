using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : Enemy {


	public float frequency;
	public float magnitude;

	// Use this for initialization
	void Start () {
		normalizedHorizontalSpeed = -1;//1 for right -1 for left
	}
	
	// Update is called once per frame
	void Update () {

		//basic move and gravity
		//var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?



		_velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * moveSpeed, Time.deltaTime );
		
		_velocity.y = Mathf.Sin (Time.time * frequency) * magnitude;
		//print(_velocity.y);
		_controller.move(_velocity * Time.deltaTime);		
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;

	}
}
