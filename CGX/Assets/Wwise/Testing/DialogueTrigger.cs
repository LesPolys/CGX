using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	[Tooltip("0=K, 1=R, 2=M, 3=D")]
	public int whichCharacter;

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
			collider.gameObject.GetComponent<PartyManager>().StopPartyCoroutine(whichCharacter);
		}
	}

}
