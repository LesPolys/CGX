using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {


	private Animator _animator;

	public float fireBallSpeed;

	bool hasTarget = false;


	void Awake(){
		_animator = GetComponent<Animator>();

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (hasTarget) {
			transform.position += (-1 * transform.up) * fireBallSpeed * Time.deltaTime;
		}
		
	}

	public void SetTarget(Vector3 target){


		Vector3 vectorToTarget = target - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = q;

	}


	public void PlayFireBallGrow(){
		_animator.Play(Animator.StringToHash("FireBallGrow"));
	}

	public void PlayFireBall(){
		hasTarget = true;
		_animator.Play(Animator.StringToHash("FireBall"));

	}

	public void FireBallHit(){
		gameObject.SetActive (false);
		hasTarget = false;
	}



	void OnTriggerEnter2D(Collider2D collider){
		//print ("w");
		
		if(collider.gameObject.tag != "Enemy"){
			FireBallHit();
		}
		
		if (collider.gameObject.tag == "Player") {// || hit.gameObject.tag == "Bullet")
			FireBallHit ();
		} 




	}




}
