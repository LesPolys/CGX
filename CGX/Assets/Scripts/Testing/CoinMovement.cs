using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour {

	public float rotateSpeed;

	public float minBobSpeed;
	public float maxBobSpeed;

	public float minBobAmount;
	public float maxBobAmount;

	private float bobAmount;
	private float bobSpeed;

	Vector3 tmpPos;

	void Awake(){
		transform.Rotate(Vector3.up, Random.Range(0,180));
		bobAmount = Random.Range(minBobAmount,maxBobAmount);
		bobSpeed = Random.Range(minBobSpeed,maxBobSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		
		tmpPos = transform.position;
    	tmpPos.y += (Mathf.Sin(bobSpeed * Time.time) * bobAmount);
		transform.position = tmpPos;
		//transform.position = Mathf.Lerp(transform.position.y, tmpPos, Time.time);
    	transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
	}


}

