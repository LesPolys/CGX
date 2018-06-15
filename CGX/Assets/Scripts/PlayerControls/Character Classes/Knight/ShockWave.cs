using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    //void OnCollisionEnter(Collision hit)
    {
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
            other.gameObject.GetComponent<Agent>().Damage(1);
            other.gameObject.GetComponent<Agent>().KnockBack(10, (other.transform.position - transform.position).normalized);
            Destroy(gameObject);
            return;
        }


        //Destroy(gameObject);
    }
}
