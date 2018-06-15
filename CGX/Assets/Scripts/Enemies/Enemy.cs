using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent {
	
	// movement config
	[SerializeField]
	protected float gravity = -25f;
   
	[HideInInspector]
	protected float normalizedHorizontalSpeed = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	

	}
}
