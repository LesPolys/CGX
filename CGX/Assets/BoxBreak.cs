using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreak : MonoBehaviour {


	Animator _animator;

	public bool broken = false;

	void Awake(){
		_animator = GetComponent<Animator> ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Player"){
	//		print ("skbf");
			//broken = true;
			_animator.Play (Animator.StringToHash("CrateSmash"));
		}

	}

	public void CrateIsBrokeAF(){
		_animator.Play(Animator.StringToHash("SmashedCrate"));
	}


	public void ResetBox(){
		broken = false;
		_animator.Play (Animator.StringToHash("IdleCrate"));
	}
}
