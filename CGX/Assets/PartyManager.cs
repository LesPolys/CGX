using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour {

	//Manages the control of the actual player characters


	public float partyMoveSpeed;

	public float partyGravity;
	public float partyJumpStartMultiplier;
	public float partyJumpEndMultiplier;
	public float partyJumpHeight;


	public float partyForwardSpeedOffset;
	public float partyBackwardSpeedOffset;

	public List<Player> tempPartyHolder;
	public LinkedList<Player> theParty = new LinkedList<Player>();
	[SerializeField]
	Transform[] partyPositions;

	int currentPlayerIndex;

	Player currentPlayer;

	void OnEnable(){
		foreach (Player member in tempPartyHolder) {
			theParty.AddFirst(member);
			member.ChangeMoveSpeed(partyMoveSpeed);
		}
		currentPlayer = theParty.First.Value;

		OrganizeParty ();

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
	
			currentPlayer.SetJumpSignal(true);
		}

			
		transform.Translate(transform.right * partyMoveSpeed * Time.deltaTime);

		
	}


	void NextMember(){
		Player temp = theParty.Last.Value;
		theParty.RemoveLast ();
		theParty.AddFirst (temp);
		OrganizeParty ();
		SetCurrentPlayer ();
		UpdatePartyStats ();
		 //PrintPartyList ();
	}

	void PreviousMember(){
		Player temp = theParty.First.Value;
		theParty.RemoveFirst ();
		theParty.AddLast (temp);
		OrganizeParty ();
		SetCurrentPlayer ();
		UpdatePartyStats ();
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

	void UpdatePartyStats(){
		
		foreach (Player member in theParty) {
			member.ChangeMoveSpeed(partyMoveSpeed);
			member.SetSpeedOffsets(partyForwardSpeedOffset, partyBackwardSpeedOffset);
		}
	}

}
