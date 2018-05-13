using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerClass : MonoBehaviour {

    public enum CharClass{Knight,Ranger,Mage,Druid};
	private bool isDead = false;


    public CharClass theClass;

    PartyProperties partyStats;
    private float moveSpeed = 0.1f;

    [Range(1, 10)]
    public float jumpVelocity;

    public float groundedSkin = 0.05f; // the thickness below ourself we check if we are standin on it
    public LayerMask mask;

    private Rigidbody2D rb2d;
    private Animator myAnimator;
    public GameManager gameManager;

    bool jumpRequest;
    bool grounded;

    Vector2 playerSize;
    Vector2 boxSize;



    // Use this for initialization

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x, groundedSkin);
        myAnimator = GetComponent<Animator>();
        partyStats = GetComponentInParent<PartyProperties>();
        moveSpeed = partyStats.partyMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
      
  

        if(grounded){
            switch (theClass)
            {
                case CharClass.Knight:
                    if(Input.GetKeyDown(KeyCode.R) ){
                        jumpRequest = true;
                        AkSoundEngine.PostEvent("Knight_Jump", gameObject);
                    }
                    break;

                case CharClass.Ranger:
                 if(Input.GetKeyDown(KeyCode.E) ){
                        jumpRequest = true;
                        AkSoundEngine.PostEvent("Ranger_Jump", gameObject);
                    }
                break;

                case CharClass.Mage:
                 if(Input.GetKeyDown(KeyCode.W) ){
                        jumpRequest = true;
                        AkSoundEngine.PostEvent("Mage_Jump", gameObject);
                    }
                break;

                case CharClass.Druid:
                 if(Input.GetKeyDown(KeyCode.Q) ){
                        jumpRequest = true;
                        AkSoundEngine.PostEvent("Druid_Jump", gameObject);
                    }
                break;
            }
        }

        myAnimator.SetFloat("Speed", rb2d.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
        myAnimator.SetFloat("Jump", rb2d.velocity.y);


    }

    private void FixedUpdate()
    {
      

        if (jumpRequest)
        {
            //rb2d.velocity += Vector2.up * jumpForce;
            rb2d.AddForce(Vector2.up * jumpVelocity , ForceMode2D.Impulse);
            //rb2d.velocity = new Vector2(rb2d.velocity.x, jumpVelocity);
            jumpRequest = false;
            grounded = false;
        }
        else
        {
            Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
            grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0.0f , mask) != null);
        }

        rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);

    }

   void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "KillBox" ){
			if(!GetIsDead()){ ToggleDeath(); }
			
            gameManager.RemoveFromCam(this.gameObject);
			gameObject.SetActive(false);
          
            gameManager.ShakeTheCamera();
			partyStats.PartyMemberDied();
           
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

