using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {

    public GameObject shockwave;

    public enum CharClass{Knight,Ranger,Mage,Druid};
	private bool isDead = false;

    BoxCollider2D coll;

    public CharClass theClass;

    public float moveSpeed = 0.1f;

    [Range(1, 15)]
    public float jumpVelocity;

    public float groundedSkin = 0.05f; // the thickness below ourself we check if we are standin on it
    public LayerMask mask;

    private Rigidbody2D rb2d;
    private Animator myAnimator;
    //public GameManager gameManager;

    bool jumpRequest;
	bool attackRequest;
	bool waveTrigger;
    bool grounded;


    Vector2 playerSize;
    Vector2 boxSize;

	public float attackSpeed = 2;


    //falling feel
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;


    // Use this for initialization

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x, groundedSkin);
        myAnimator = GetComponentInChildren<Animator>();
        coll = GetComponent<BoxCollider2D>();
//        partyStats = GetComponentInParent<PartyProperties>();
      //  moveSpeed = partyStats.partyMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
      
        if(Input.GetKeyDown(KeyCode.R) ){
            PlayerRequest();
        }
         
   

        myAnimator.SetFloat("Speed", rb2d.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
        myAnimator.SetFloat("Jump", rb2d.velocity.y);
        myAnimator.SetBool("Attack", attackRequest);


    }

	

    private void FixedUpdate()
    {
      
        

        if (jumpRequest)
        {
            rb2d.AddForce(Vector2.up * jumpVelocity , ForceMode2D.Impulse);
            jumpRequest = false;
            grounded = false;
        }else if(attackRequest){
 			rb2d.AddForce(Vector2.up * attackSpeed , ForceMode2D.Impulse);
			attackRequest = false;
		}
        

        //falling properties
        if (rb2d.velocity.y < 0 ) // if falling
        {
            rb2d.gravityScale = fallMultiplier;
        }
        else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb2d.gravityScale = lowJumpMultiplier;
        }
        else {
            rb2d.gravityScale = 1f;
        }

        grounded = IsGrounded();
        rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);

    }

    bool IsGrounded() {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;
        
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, mask);
        if (hit.collider != null) {
            return true;
        }
        
        return false;
    }

  


	void PlayerRequest(){
		if(IsGrounded()){
			jumpRequest = true;
		}else if(!attackRequest){
			attackRequest = true;
		}
	}

   void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "KillBox" ){
//			if(!GetIsDead()){ ToggleDeath(); }
			
			gameObject.SetActive(false);
//            gameManager.ShakeTheCamera();
//			partyStats.PartyMemberDied();
           
        }
    }

}
