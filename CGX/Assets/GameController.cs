using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class GameController : MonoBehaviour {


	public ScrollingBackground lastPanel;

	public OnScreenMovable moveables;
	public GameObject rightLooper;


	enum DemoGameState {KNIGHTJUMP, KNIGHTATTACK, RANGERADD, RANGERATTACK, MAGEADD, MAGEATTACK, DRUIDADD, DRUIDATTACK, ALLTOGETHER};
	DemoGameState currentState;
	bool wasStateChanged = false;

	public ChangeInstructions instruc;


	[SerializeField]
	private PartyManager pManager;

	#region KNIGHTJUMP variables
	private int numKnightJumps = 0;
	private int maxNumJumps = 2;



	#endregion  

	#region KNIGHTATTACK variables
	//hit x number of things with knight

	private int numKnightAttackHits = 0;
	private int maxNumKnightEnemiesHit = 1;
	
	#endregion


	#region RANGERADD variables
	//is the ranger in the party
	private bool hasRanger = false;
	private bool rangerLeader = false;


	#endregion


	#region RANGERATTACK variables
	//has the ranger hit x number of things

	private int numRangerAttackHits = 0;
	private int maxNumRangerEnemiesHit = 4;
	
	#endregion

	#region MAGEADD variables
	//is the mage in the party
	private bool hasMage = false;
	private bool mageLeader = false;
	
	#endregion

	#region MAGEATTACK variables
	//have you jumped over x number of pits
	private int numPitsJumped = 0;
	private bool jumpComplete = false;
	
	#endregion

	#region DRUIDADD variables
	//is the druid in the party
	private bool hasDruid = false;
	private bool druidLeader = false;
	
	#endregion

	#region DRUIDATTACK variables
	//has the druid frozen x number of enemies
	private int numDruidAttackHits = 0;
	private int maxNumDruidEnemiesHit = 2;
	
	#endregion

	#region ALLTOGETHER variables
	//idfk but use the fuckers powers
	
	#endregion


	void Awake(){
		currentState = DemoGameState.KNIGHTJUMP;

		//Player.jumpEvent += JumpMethodListener;//sub a listner to event, event holds a reference of all its listners

		Knight.knightJumpEvent += KnightJumpMethodListener;

		ShockWave.shockwaveHitEvent += KnightAttackHitMethodListener;

		PartyManager.rangerAddEvent += HasRangerMethodListner;
		
		Arrow.arrowHitEvent += RangerAttackHitMethodListener;
		
		Vine.vineRootHitEvent += DruidAttackHitMethodListener;

		PartyManager.druidAddEvent += HasDruidMethodListner;
		
		SuccessfullJump.boxJumpEvent += MageAttackHitMethodListener;

		PartyManager.mageAddEvent += HasMageMethodListner;

		PartyManager.rangerLeaderEvent += RangerLeaderMethodListner;

		PartyManager.mageLeaderEvent += MageLeaderMethodListner;

		PartyManager.druidLeaderEvent += DruidLeaderMethodListner;

		
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.X)) {
			//moveables.MoveY(10.0f);
			currentState++;
			wasStateChanged = true;
//			print(currentState);
			//NextChallenge();
		}


		switch (currentState) {

			case DemoGameState.KNIGHTJUMP:
			//print ("IN KNIGHT JUMP");
				if(numKnightJumps > maxNumJumps){
					//currentState = DemoGameState.KNIGHTATTACK;
					currentState++;
					wasStateChanged = true;
				}
				break;


			case DemoGameState.KNIGHTATTACK:
			//print ("IN KNIGHT ATTACK");
				if(numKnightAttackHits > maxNumKnightEnemiesHit){
						//currentState = DemoGameState.KNIGHTATTACK;
						currentState++;
						wasStateChanged = true;
					}
				break;


			case DemoGameState.RANGERADD:
			//print ("IN RANGERADD");
				if(!hasRanger){
					pManager.CreatePartyMember(1);
				}

				if(rangerLeader){
					currentState++;
					wasStateChanged = true;
				}
			break;


			case DemoGameState.RANGERATTACK:
			//print ("RANGERATTACK");
				if(numRangerAttackHits > maxNumRangerEnemiesHit){
					//currentState = DemoGameState.KNIGHTATTACK;
					currentState++;
					wasStateChanged = true;
				}
				break;


			case DemoGameState.MAGEADD:
			//print ("MAGEADD");
				if(!hasMage){
					pManager.CreatePartyMember(2);
				}

				if(mageLeader){
					currentState++;
					wasStateChanged = true;
				}
				break;


			case DemoGameState.MAGEATTACK:
	//print ("MAGEATTACK");
				if(jumpComplete){
					currentState++;
					wasStateChanged = true;
				}
				break;


			case DemoGameState.DRUIDADD:
	//	 ("DRUIDADD");
				if(!hasDruid){
					pManager.CreatePartyMember(3);
				}

				if(druidLeader){
					currentState++;
					wasStateChanged = true;
				}
			break;


			case DemoGameState.DRUIDATTACK:
	//		print ("DRUIDATTACK");
				if(numDruidAttackHits > maxNumDruidEnemiesHit){
					//currentState = DemoGameState.KNIGHTATTACK;
					currentState++;
					wasStateChanged = true;
				}
			break;


			case DemoGameState.ALLTOGETHER:
	//	print ("ALLTOGETHER");
			lastPanel.scrolling = false;
			lastPanel.paralax = false;
			//lastPanel.gameObject.SetActive(false);
			break;


		}


		if(wasStateChanged){
			wasStateChanged = false;
			NextChallenge();
		}

		
	}


	private void NextChallenge(){
		moveables.MoveY(10.0f);
		moveables.MoveX(rightLooper.transform.position.x - transform.position.x);
		instruc.NextSprite ();

	}

	private void KnightJumpMethodListener(){//functions called wby event set in awake
		//print ("hbkhjb");
		numKnightJumps ++;
	}

	private void KnightAttackHitMethodListener(){//functions called wby event set in awake
		numKnightAttackHits++;
	}


	private void HasRangerMethodListner(){
		hasRanger = true;
	}

	private void RangerAttackHitMethodListener(){//functions called wby event set in awake
		numRangerAttackHits++;
	}

	private void DruidAttackHitMethodListener(){//functions called wby event set in awake
		numDruidAttackHits++;
	}

	private void HasDruidMethodListner(){
		hasDruid = true;
	}

	private void MageAttackHitMethodListener(){//functions called wby event set in awake
		jumpComplete = true;
	}

	private void HasMageMethodListner(){
		hasMage = true;
	}

	private void RangerLeaderMethodListner(){
		rangerLeader = true;
	}

	private void MageLeaderMethodListner(){
		mageLeader = true;
	}

	private void DruidLeaderMethodListner(){
		druidLeader = true;
	}


}
