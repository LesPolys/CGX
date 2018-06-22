using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopController : MonoBehaviour {


	public GameObject rightLooper;

	public OnScreenMovable movable;


	void Awake(){

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Player"){
			movable.MoveX(rightLooper.transform.position.x - other.transform.position.x);
		}

	}

}
