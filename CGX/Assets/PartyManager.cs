using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour {

	//Manages the control of the actual player characters


	struct PlayerStats{
	// movement config
		public float gravity = -25f;
		public float jumpStartMultiplier = -25f;
		public float jumpEndMultiplier = -25f;
		public float groundDamping = 20f; // how fast do we change direction? higher means faster
		public float inAirDamping = 5f;
		public float jumpHeight = 3f;
	}
	
	PlayerStats stats
	//contains a data structure to cycle between the various characters
	//the active character will control the jump height and the active power

	public List<Player> tempPartyHolder;
	public LinkedList<Player> theParty = new LinkedList<Player>();
	[SerializeField]
	Transform[] partyPositions;

	int currentPlayerIndex;

	Player currentPlayer;

	void OnEnable(){
		foreach (Player member in tempPartyHolder) {
			theParty.AddFirst(member);
		}

	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.RightArrow)){
			NextMember();
		}

		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			PreviousMember();
		}

		if(Input.GetKeyDown(KeyCode.UpArrow)){//jump
			currentPlayer.Jump();
		}

		
	}


	void NextMember(){
		Player temp = theParty.Last.Value;
		theParty.RemoveLast ();
		theParty.AddFirst (temp);
		OrganizeParty ();
		SetCurrentPlayer ();

		 //PrintPartyList ();
	}

	void PreviousMember(){
		Player temp = theParty.First.Value;
		theParty.RemoveFirst ();
		theParty.AddLast (temp);
		OrganizeParty ();
		SetCurrentPlayer ();

		//PrintPartyList ();
	}

	void SetCurrentPlayer(){
		currentPlayer = theParty.First.Value;
	}

	void OrganizeParty(){
		int x = 0;
		foreach (Player member in theParty) {
			member.SetPartyPosition(partyPositions[x]);
			x++;
		}
	}


	void PrintPartyList(){
		foreach (Player member in theParty) {
			print (member);

		}
		print (" ");
		print (" ");
		print (" ");

		print ( currentPlayer);
		print (" ");
		print (" ");
	}

}
