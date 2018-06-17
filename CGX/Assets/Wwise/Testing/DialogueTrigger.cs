using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	public void TriggerDialogue(){
		//change this to a singleton pattern later
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

	}


}
