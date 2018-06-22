using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenMovable : MonoBehaviour {


	public float maxHeight = 80.0f;

	public ScrollingBackground[] bgs;


	public GameObject startTrigger, endTrigger;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {




	}


	public void MoveY(float additive){
		if(transform.position.y < maxHeight){
			transform.position = new Vector3 (transform.position.x, transform.position.y + additive, 0.0f);
			startTrigger.transform.position = new Vector3 (startTrigger.transform.position.x, startTrigger.transform.position.y + additive, 0.0f);
			endTrigger.transform.position = new Vector3 (endTrigger.transform.position.x, endTrigger.transform.position.y + additive, 0.0f);

		}
	}

	public void MoveX(float shift){
		transform.position = new Vector3 (transform.position.x + shift, transform.position.y, 0.0f);
		foreach (ScrollingBackground bg in bgs) {
			bg.SetCameraLastXPos(Camera.main.transform.position.x);
		}
	}



}
