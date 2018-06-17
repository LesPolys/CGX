using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour {

	//Manages the control of the actual player characters


	public float partyMoveSpeed;
	public float acceptableDistance;


	public float jumpDelay;

	public float partySpeedOffset;

	public List<Player> tempPartyHolder;
	public LinkedList<Player> theParty = new LinkedList<Player>();
	[SerializeField]
	Transform[] partyPositions;

	int currentPlayerIndex;

	Player currentPlayer;



	private bool jumpPressed = false;


	void OnValidate(){
		UpdatePartyStats ();
	}


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

			jumpPressed = true;
			StartCoroutine (GroupJump ());
		}

			
		transform.Translate(transform.right * partyMoveSpeed * Time.deltaTime);


	}


	void NextMember(){
		Player temp = theParty.Last.Value;
		theParty.RemoveLast ();
		theParty.AddFirst (temp);
		OrganizeParty ();

		 //PrintPartyList ();
	}

	void PreviousMember(){
		Player temp = theParty.First.Value;
		theParty.RemoveFirst ();
		theParty.AddLast (temp);
		OrganizeParty ();

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
		SetCurrentPlayer ();
		UpdatePartyStats ();
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
			member.SetSpeedOffsets(partySpeedOffset);
			member.SetAcceptableDistance(acceptableDistance);
		}
	}

	IEnumerator GroupJump(){
			if(jumpPressed){
				foreach (Player member in theParty) {
					if(!member.IsGrounded() && currentPlayer == member){
						member.SetJumpSignal(true);
						yield return new WaitForSecondsRealtime(jumpDelay);
					}else if(member.IsGrounded()){
						member.SetJumpSignal(true);
						yield return new WaitForSecondsRealtime(jumpDelay);
					}
					
				}
				jumpPressed = false;
			}
	}

	public void AddMember(Player newMember){
		if(theParty.Count < 4){
			theParty.AddLast (newMember);
			OrganizeParty ();
		}
	}

	public void RemoveMember(){
		theParty.First.Value.gameObject.SetActive (false);
		theParty.RemoveFirst ();
		OrganizeParty();
	}

}
