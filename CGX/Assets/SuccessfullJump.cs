using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SuccessfullJump : MonoBehaviour {

	public GameObject[] boxes;

	public static event Action boxJumpEvent = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		
		if(other.gameObject.tag == "Player"){

			print ("aswbflsjnv;l");
		
			foreach (GameObject box in boxes) {
				if(box.GetComponent<BoxBreak>().broken == true){
					break;
				}else{
					FireBoxJumpEvent();
				}
			}
		}
		
	}

	public void ResetCrates(){
		foreach (GameObject box in boxes) {
			box.GetComponent<BoxBreak>().ResetBox();
		}
	}


	public void FireBoxJumpEvent(){ // call this to fire the event to all listen
		//if (jumpEvent != null) {// check to see if no one is listening cause that would be embarassing screaming into the void
		//jumpEvent();//fire the event

		boxJumpEvent.Invoke();//also fires the event but dont need a null check for listeners
		//}
	}



}
