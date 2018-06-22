using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShockWave : MonoBehaviour {

	public float damage;
	public float kbPower;


	public static event Action shockwaveHitEvent = null; //events are kind of like a weird list

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    //void OnCollisionEnter(Collision hit)
    {
		//print ("here");
        if (other.gameObject.tag == "Player")// || hit.gameObject.tag == "Bullet")
        {
           // print("Player");
            // Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
            Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            return;
        }

        if (other.gameObject.tag == "Enemy")// || hit.gameObject.tag == "Bullet")
        {
            //knockback and damage
			//print ("WTW");
			other.gameObject.GetComponent<Agent>().Damage(damage);
			//other.gameObject.GetComponent<Agent>().KnockBack(kbPower, 1,(other.transform.position.x - transform.position.x).normalized);
			//other.gameObject.GetComponent<Agent>().KnockBack(kbPower, 1,(other.transform.position.x - transform.position.x));
			other.gameObject.GetComponent<Agent>().KnockBack(kbPower, 1,(other.transform.position - transform.position));

			FireShockWaveHitEvent();

            return;
        }


        //Destroy(gameObject);
    }

	public void EndAnimation(){
		gameObject.SetActive (false);
	}

	public void FireShockWaveHitEvent(){ // call this to fire the event to all listen
		//if (jumpEvent != null) {// check to see if no one is listening cause that would be embarassing screaming into the void
		//jumpEvent();//fire the event
		shockwaveHitEvent.Invoke();//also fires the event but dont need a null check for listeners
		//}
	}

}
