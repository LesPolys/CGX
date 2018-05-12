using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerClass : MonoBehaviour {



    protected bool isDead = false;

    //PartyProperties partyStats;
    [SerializeField]
    protected float moveSpeed = 0.1f;
    //falling feel
    [SerializeField]
    protected  float fallMultiplier = 2.5f;
    [SerializeField]
    protected  float lowJumpMultiplier = 2f;

    [SerializeField]
    [Range(1, 10)]
    protected float jumpVelocity;

    [SerializeField]
    protected float groundedSkin = 0.05f; // the thickness below ourself we check if we are standin on it

    [SerializeField]
    protected LayerMask mask;

    protected Rigidbody2D rb2d;

    protected Animator myAnimator;

    protected bool jumpRequest;
    protected bool grounded;

    
    public  Vector2 playerSize;
    public  Vector2 boxSize;

    public PlayerClass()
    {

    }

    // Use this for initialization

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x, groundedSkin);
        myAnimator = GetComponent<Animator>();
        
    }

    private void FixedUpdate()
    {
      

        if (jumpRequest)
        {
            rb2d.AddForce(Vector2.up * jumpVelocity , ForceMode2D.Impulse);
            jumpRequest = false;
            grounded = false;
        }
        else
        {
            Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
            grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0.0f , mask) != null);
        }



        //falling properties
        if (rb2d.velocity.y < 0) // if falling
        {
            rb2d.gravityScale = fallMultiplier;
        }
        else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb2d.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb2d.gravityScale = 1f;
        }

 


        rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);

    }

   void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "KillBox" ){
			if(!GetIsDead()){ ToggleDeath(); }
			
            //gameManager.RemoveFromCam(this.gameObject);
			//gameObject.SetActive(false);
          
            //gameManager.ShakeTheCamera();
			//partyStats.PartyMemberDied();
           
        }
    }


	public bool GetIsDead(){
		return isDead;
	}

	public void ToggleDeath(){
		isDead = !isDead;
	}

	public void SetIsDead(bool newDeathState){
		isDead = newDeathState;
	}

}

