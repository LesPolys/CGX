using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyProperties : MonoBehaviour {

    [Range(0.1f, 10f)]
    public float partyMoveSpeed = 2f;

	public GameObject[] partyMembers;
	private Vector3[] partyStartPositions;

	GameManager gameManager; 
	private int partyDeathCount = 0;

	void Awake(){

		gameManager = FindObjectOfType<GameManager>();
		partyStartPositions = new Vector3[partyMembers.Length];
		for(int i = 0; i < partyStartPositions.Length; i++){
			partyStartPositions[i] = partyMembers[i].transform.position;
		}

	}

	void Update(){

		if(partyDeathCount >= 4){
			partyDeathCount = 0;
			gameManager.RestartGame();
		}

	}

	public Vector3 GetPartyMemberStartPos(int index){
		return partyStartPositions[index];
	}

	public void PartyMemberDied(){
		partyDeathCount++;
	}
}
