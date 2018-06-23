using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;


[RequireComponent(typeof(CharacterController2D))]
public class Agent : MonoBehaviour
{

	 [SerializeField]
    protected float health;

    [SerializeField]
    protected float moveSpeed;




	protected CharacterController2D _controller;
    protected Animator _animator;

    protected Vector3 _velocity;


    #region Event Listeners

    void onControllerCollider(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they arent very interesting
        if (hit.normal.y == 1f)
            return;

        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
        //Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
    }


    void onTriggerEnterEvent(Collider2D col)
    {
        //Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
    }


    void onTriggerExitEvent(Collider2D col)
    {
        //Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
    }

    #endregion

	protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();

        // listen to some events for illustration purposes
        _controller.onControllerCollidedEvent += onControllerCollider;
        _controller.onTriggerEnterEvent += onTriggerEnterEvent;
        _controller.onTriggerExitEvent += onTriggerExitEvent;
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void KnockBack( float knockDur, float power, Vector3 knockBackDirection)
    {
		//knockBackDirection += (new Vector2 (0, yPower) );
		//changeVelocity(knockBackDirection * xPower);

		StartCoroutine (KnockBackRoutine(  knockDur,  power,  knockBackDirection));

    }

	IEnumerator KnockBackRoutine(float knockDur, float knockBackPwr, Vector3 knockbackDir){
		float timer = 0;

		while (knockDur > timer) {
			timer += Time.deltaTime;

			changeVelocity (new Vector3 (knockbackDir.x * -100, knockbackDir.y * knockBackPwr, 0.0f));

		}
		yield return null;
	}


	public void KnockUp(float yPower){

	}


	public void Damage(float damage){ //reduce health stat by X
		health -= damage;
	}


    public void Root(float newSpeed, float abilityTime)
    {
        StartCoroutine(RootRoutine(newSpeed, abilityTime, transform.position));
    }

    IEnumerator RootRoutine(float newSpeed, float abilityTime, Vector3 trans)
    {
		transform.position = trans;
        float startSpeed = moveSpeed;
		if (gameObject.GetComponent<Minos> () != null) {
			float tempC = gameObject.GetComponent<Minos> ().chargeSpeed;
			gameObject.GetComponent<Minos> ().chargeSpeed = 0;
			_velocity.x = newSpeed;//remove for gradual velocity reduction, should probably lerp between the values.
			moveSpeed = newSpeed;
			yield return new WaitForSeconds (abilityTime);
			moveSpeed = startSpeed;
			gameObject.GetComponent<Minos> ().chargeSpeed = tempC;
		} else {
			_velocity.x = newSpeed;//remove for gradual velocity reduction, should probably lerp between the values.
			moveSpeed = newSpeed;
			yield return new WaitForSeconds(abilityTime);
			moveSpeed = startSpeed;
		}

    }

    IEnumerator AlterSpeedTemp(float speedChange, float abilityTime)
    {
        float startSpeed = moveSpeed;
        moveSpeed += speedChange;
        yield return new WaitForSeconds(abilityTime);
        moveSpeed = startSpeed;
    }

    protected void changeYVelocity(float newYVelocity)
    {
        _velocity.y = newYVelocity;
    }

    protected void changeVelocity(Vector3 newVelocity)
    {
        _velocity = newVelocity;
    }

	protected void moveVelocity(Vector3 newVelocity)
	{
		_velocity += newVelocity;
	}

	public bool IsGrounded(){
		return _controller.isGrounded;
	}

	protected void SetMovementSpeed(float newSpeed){
		moveSpeed = newSpeed;
	}

		



}
