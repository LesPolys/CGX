﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	private Queue<string> sentences;
	public Text nameText;
	public Text dialogueText;

	public Animator animator;

	void Start(){
		sentences = new Queue<string>();
		animator.SetBool("IsOpen", false);
	}

	public void StartDialogue(Dialogue dialogue){

		animator.SetBool("IsOpen", true);


		nameText.text = dialogue.name;

		sentences.Clear();

		foreach(string sentence in dialogue.sentences){
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();

	}

	public void DisplayNextSentence(){
		if(sentences.Count == 0){
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();

		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));

	}

	IEnumerator TypeSentence(string sentence){
		dialogueText.text = "";

		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}

	}

	private void EndDialogue(){
		//Debug.Log("EOF");
		animator.SetBool("IsOpen", false);
		FindObjectOfType<PartyManager>().StartParty();
		
	}

}
