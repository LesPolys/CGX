using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour {

	public float damage;
	public float kbPower;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    //void OnCollisionEnter(Collision hit)
    {
		print ("here");
        if (other.gameObject.tag == "Player")// || hit.gameObject.tag == "Bullet")
        {
            print("Player");
            // Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
            Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            return;
        }

        if (other.gameObject.tag == "Enemy")// || hit.gameObject.tag == "Bullet")
        {
            //knockback and damage
			print ("WTW");
			other.gameObject.GetComponent<Agent>().Damage(damage);
			other.gameObject.GetComponent<Agent>().KnockBack(kbPower, 1,(other.transform.position - transform.position).normalized);
            return;
        }


        //Destroy(gameObject);
    }

	public void EndAnimation(){
		gameObject.SetActive (false);
	}

}
