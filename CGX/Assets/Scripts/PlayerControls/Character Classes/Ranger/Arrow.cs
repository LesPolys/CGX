using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float arrowSpeed = 0.0f;
    public Vector3 myDir;
    public float speed = 30.0f; //Probably don't need a slerp for this
 


   

    //void Start()
    //{
        
    //    Vector3 vectorToTarget = target.position - transform.position;
    //    float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
    //    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    //}
    
    public void Target(Transform target)
    {
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }

    void Update()
    {
        //Move Projectile
        transform.position += transform.right * arrowSpeed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    //void OnCollisionEnter(Collision hit)
    {
        if (other.gameObject.tag == "Player")// || hit.gameObject.tag == "Bullet")
        {
            //print("Player");
            // Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
            Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            return;
        }

		if (other.gameObject.tag == "Enemy")// || hit.gameObject.tag == "Bullet")
		{
            //knockback and damage
			other.gameObject.GetComponent<Agent>().Damage(1);
            other.gameObject.GetComponent<Agent>().KnockBack(2, (other.transform.position - transform.position).normalized);
            gameObject.SetActive(false);
            return;
		}
    
        
        //Destroy(gameObject);
    }
}
