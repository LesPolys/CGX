using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnd : MonoBehaviour {


	public PartyManager theParty;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D collider){

		//print ("w");
		if (collider.gameObject.tag == "Player") {
			theParty.StopPartyCoroutine (0);
		}
	}


}
