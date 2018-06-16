using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour {

	Animator _animator;

	// Use this for initialization

	//vines need to grow up from ground when set to active,

	//when the end of the animation happens it should trigger an event call to play the reverse animation or brake animation, when thats done that animation should call an event which would release the enemies

	//think about making the slow call a part of the vine and not a part of the druid, more flexible that way
	//vines could store a reference to their enemy is probably the way to go

	void Awake()
	{
		_animator = GetComponent<Animator> ();
	}

	public void Grow(){
		_animator.Play (Animator.StringToHash("Grow"));
	}

	public void EndAnimation(){
		gameObject.SetActive(false);
	}


}
