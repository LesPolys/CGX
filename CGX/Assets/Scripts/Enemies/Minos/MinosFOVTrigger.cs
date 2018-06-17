using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinosFOVTrigger : MonoBehaviour {

	[SerializeField]
	private Minos parentMinos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		//print ("TEST");
		if (other.gameObject.tag == "Player")// || hit.gameObject.tag == "Bullet")
		{
			print("Player");
			parentMinos.EnemyInRange ();
			return;
		}
	}
}
