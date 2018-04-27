using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoints : MonoBehaviour {


	public int pointsValue;
	private ScoreManager scoreManager;

	void Awake(){
		scoreManager = FindObjectOfType<ScoreManager>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			scoreManager.AddScore(pointsValue);
			gameObject.SetActive(false);
//            AkSoundEngine.PostEvent("coin_pickup", gameObject);
        }
	}

}
