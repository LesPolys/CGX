﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class GameController : MonoBehaviour {

	public OnScreenMovable moveables;
	public GameObject rightLooper;


	enum DemoGameState {KNIGHTJUMP, KNIGHTATTACK, RANGERADD, RANGERATTACK, MAGEADD, MAGEATTACK, DRUIDADD, DRUIDATTACK, ALLTOGETHER};
	DemoGameState currentState;
	bool wasStateChanged = false;


	[SerializeField]
	private PartyManager pManager;

	#region KNIGHTJUMP variables
	private int numKnightJumps = 0;
	private int maxNumJumps = 3;



	#endregion  

	#region KNIGHTATTACK variables
	//hit x number of things with knight

	private int numKnightAttackHits = 0;
	private int maxNumKnightEnemiesHit = 3;
	
	#endregion


	#region RANGERADD variables
	//is the ranger in the party
	private bool hasRanger = false;



	#endregion


	#region RANGERATTACK variables
	//has the ranger hit x number of things

	private int numRangerAttackHits = 0;
	private int maxNumRangerEnemiesHit = 3;
	
	#endregion

	#region MAGEADD variables
	//is the mage in the party
	private bool hasMage = false;
	
	#endregion

	#region MAGEATTACK variables
	//have you jumped over x number of pits
	private int numPitsJumped = 0;

	
	#endregion

	#region DRUIDADD variables
	//is the druid in the party
	private bool hasDruid = false;
	
	#endregion

	#region DRUIDATTACK variables
	//has the druid frozen x number of enemies
	private int numDruidAttackHits = 0;
	private int maxNumDruidEnemiesHit = 3;
	
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
		
		//DruidAttackHitMethodListener;

		PartyManager.druidAddEvent += HasDruidMethodListner;
		
		//MageAttackHitMethodListener;

		PartyManager.mageAddEvent += HasMageMethodListner;
		
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
			print(currentState);
			//NextChallenge();
		}


		switch (currentState) {

			case DemoGameState.KNIGHTJUMP:
				if(numKnightJumps > maxNumJumps){
					//currentState = DemoGameState.KNIGHTATTACK;
					currentState++;
					wasStateChanged = true;
				}
				break;


			case DemoGameState.KNIGHTATTACK:

				break;


			case DemoGameState.RANGERADD:

				if(!hasRanger){
					pManager.CreatePartyMember(1);
				}
				break;


			case DemoGameState.RANGERATTACK:

				break;


			case DemoGameState.MAGEADD:
				if(!hasMage){
					pManager.CreatePartyMember(2);
				}
				break;


			case DemoGameState.MAGEATTACK:

				break;


			case DemoGameState.DRUIDADD:
				if(!hasDruid){
					pManager.CreatePartyMember(3);
				}
				break;


			case DemoGameState.DRUIDATTACK:

				break;


			case DemoGameState.ALLTOGETHER:

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

	}

	private void KnightJumpMethodListener(){//functions called wby event set in awake
		print ("hbkhjb");
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
		numPitsJumped++;
	}

	private void HasMageMethodListner(){
		hasMage = true;
	}



}
