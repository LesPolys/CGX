using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;

    public float jumpTime;
    private float jumpTimeCounter;

    private Rigidbody2D rb2d;
    private Animator myAnimator;
    private GameManager gameManager;

    //for grounding
    public bool grounded;
    public LayerMask ground;
    public Transform groundCheck;
    public float groundCheckRadius;

    private Collider2D myCollider;

    private void Awake()
    {

        rb2d = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        
        jumpTimeCounter = jumpTime;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //grounded = Physics2D.IsTouchingLayers(myCollider, ground);
        grounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,ground);

        rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (jumpTimeCounter > 0) {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                jumpTimeCounter-=Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpTimeCounter = 0;
        }

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
        }

        myAnimator.SetFloat("Speed", rb2d.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
        myAnimator.SetFloat("Jump",rb2d.velocity.y);

	}

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "KillBox" ){
            gameManager.RestartGame();
        }
    }

}
