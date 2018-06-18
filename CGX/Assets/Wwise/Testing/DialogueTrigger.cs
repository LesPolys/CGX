using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	public void TriggerDialogue(){
		//change this to a singleton pattern later
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.P))	TriggerDialogue();

	}

	void OnTriggerEnter2D(Collider2D collider){

		if(collider.gameObject.tag == "TheParty"){

			TriggerDialogue();
			collider.gameObject.GetComponent<PartyManager>().StopPartyCoroutine();
		}
	}

}
