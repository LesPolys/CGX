using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Arrow : MonoBehaviour {

    public float arrowSpeed = 0.0f;
    public Vector3 myDir;
    public float speed = 30.0f; //Probably don't need a slerp for this
 

	private Transform arrowTarget;
	private Animator _animator;

   
	public static event Action arrowHitEvent = null; //events are kind of like a weird list
    //void Start()
    //{
        
    //    Vector3 vectorToTarget = target.position - transform.position;
    //    float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
    //    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    //}

	void Awake(){
		_animator = GetComponent<Animator>();
	}

	public void SetTarget(Transform newTarget){
		arrowTarget = newTarget;
	}

	public void Animate (){
		_animator.Play(Animator.StringToHash("Arrow"));
	}
    
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
		Target (arrowTarget);
		transform.position = Vector3.Lerp (transform.position, arrowTarget.transform.position , arrowSpeed * Time.deltaTime);
       // transform.position += transform.right * arrowSpeed * Time.deltaTime;
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
			//print ("hi");
            //knockback and damage
			other.gameObject.GetComponent<Agent>().Damage(1);
			if(other.GetComponent<Imp>() != false){
				other.GetComponent<Imp>().PlayFlash();
			}
			if(other.GetComponent<Minos>() != false){
				other.GetComponent<Minos>().SetState(Minos.MinosStates.HIT);
			}
			//other.gameObject.GetComponent<Agent>().KnockBack(2, 1,(other.transform.position - transform.position).normalized);
			FireArroweHitEvent();
            //other.gameObject.GetComponent<Agent>().KnockBack(2, 1,(other.transform.position - transform.position));
            gameObject.SetActive(false);
            return;
		}
    
        
        //Destroy(gameObject);
    }


	public void FireArroweHitEvent(){ // call this to fire the event to all listen
		//if (jumpEvent != null) {// check to see if no one is listening cause that would be embarassing screaming into the void
		//jumpEvent();//fire the event
		arrowHitEvent.Invoke();//also fires the event but dont need a null check for listeners
		//}
	}

}
