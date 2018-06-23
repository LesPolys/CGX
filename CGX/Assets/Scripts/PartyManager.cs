using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PartyManager : MonoBehaviour {

	//Manages the control of the actual player characters


	public float partyMoveSpeed;
	public float acceptableDistance;


	public float jumpDelay;

	public float partySpeedOffset;

	public List<Player> tempPartyHolder;
	public LinkedList<Player> theParty = new LinkedList<Player>();
	private List<Player> iteratorHolder = new List<Player>();

	[SerializeField]
	Transform[] partyPositions;

	[SerializeField]
	public List<GameObject> spawnablePlayerPrefabs;


	int currentPlayerIndex;

	Player currentPlayer;

	bool isTalking;

	private bool jumpPressed = false;


	//public static event Action knightAddEvent = null; //events are kind of like a weird list
	public static event Action rangerAddEvent = null; //events are kind of like a weird list
	public static event Action mageAddEvent = null; //events are kind of like a weird list
	public static event Action druidAddEvent = null; //events are kind of like a weird list


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
		/*
		if(Input.GetKeyDown(KeyCode.P)){
			isTalking = !isTalking;
			StopPartyCoroutine(1);
		}*/

	}

	public void StartParty(){
		isTalking = false;
	}

	public void StopPartyCoroutine(int i){
		isTalking = !isTalking;
		StartCoroutine(StopParty(i));
	}

	IEnumerator StopParty(int i){

		float partyMoveSpeedHolder = partyMoveSpeed;
		float partySpeedOffsetHolder = partySpeedOffset;
		//float acceptableDistanceHolder = acceptableDistance;

		while (isTalking) {
			partyMoveSpeed = 0;
			partySpeedOffset = 0;
			//acceptableDistance = 0; 
			yield return null;
		}
		CreatePartyMember (i);
		partyMoveSpeed = partyMoveSpeedHolder;
		partySpeedOffset = partySpeedOffsetHolder;
		//acceptableDistance = acceptableDistanceHolder;

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
		iteratorHolder.Clear ();

		foreach (Player member in theParty) {
			member.SetPartyPosition(partyPositions[x]);
			iteratorHolder.Add(member);
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
		
		foreach (Player member in theParty) { //coroutine causing weird enumeration race condition returning a numm list on enumaration from the linked list, solution is to dissallow reorganizing while jumping
			member.ChangeMoveSpeed(partyMoveSpeed);
			member.SetSpeedOffsets(partySpeedOffset);
			member.SetAcceptableDistance(acceptableDistance);
		}
	}

	IEnumerator GroupJump(){

			if(jumpPressed){
				//foreach (Player member in theParty) {

				for(int i =0;i< iteratorHolder.Count; i++){
				if(!iteratorHolder[i].IsGrounded() && currentPlayer == iteratorHolder[i]){
					iteratorHolder[i].SetJumpSignal(true);
						yield return new WaitForSecondsRealtime(jumpDelay);
				}else if(iteratorHolder[i].IsGrounded()){
					iteratorHolder[i].SetJumpSignal(true);
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

	public void CreatePartyMember(int index){
		if(theParty.Count < 4){

			switch(index){ //	[Tooltip("0=K, 1=R, 2=M, 3=D")]
			case 0: //knight

				break;

			case 1: //Ranger
				FireRangerAddEvent();
				break;

			case 2: //Mage
				FireMageAddEvent();
				break;

			case 3: //Druid
				FireDruidAddEvent();
				break;
			}


			//GameObject newMember = Instantiate (spawnablePlayerPrefabs[index], partyPositions[theParty.Count].position, Quaternion.identity);
			GameObject newMember = Instantiate (spawnablePlayerPrefabs[index], new Vector3(0.5f, partyPositions[theParty.Count].position.y, 0.0f), Quaternion.identity);
			newMember.transform.parent = transform;



			//theParty.AddLast(newMember.GetComponent<Player>());
			AddMember(newMember.GetComponent<Player>());
			OrganizeParty();
		}
	}

	public void FireRangerAddEvent(){
		rangerAddEvent.Invoke ();
	}

	public void FireDruidAddEvent(){
		druidAddEvent.Invoke ();
	}

	public void FireMageAddEvent(){
		mageAddEvent.Invoke ();
	}



}
