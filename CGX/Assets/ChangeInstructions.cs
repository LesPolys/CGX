using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeInstructions : MonoBehaviour {


	public Sprite[] instructions;
	Image uiImage;

	int index = 0;

	void Awake(){
		uiImage = gameObject.GetComponent<Image> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown (KeyCode.A)) {
		//	NextSprite();
		//}
	}

	public void SetSprite(){

	}

	public void NextSprite(){
		index++;
		if (index < instructions.Length) {
			uiImage.sprite = instructions [index];
		}
		if (index >= instructions.Length) {
			uiImage.gameObject.SetActive(false);
		}
	}
}
