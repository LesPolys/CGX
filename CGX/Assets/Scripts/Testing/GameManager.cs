using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class GameManager : MonoBehaviour {

    public Transform platformGenerator;
    private Vector3 platformsStartPoint;
//TESTING FOR SHAKE//ADD THE VALUES LATER DOWN BELOW IN THE HIT SHAKE FUNCTION
    public float Magnitude = 2f;
    public float Roughness = 10f;
    public float FadeOutTime = 5f;

    public GameObject cameraHolder;
    private Vector3 initCamHolderPos;

    public GameObject partyHolder;
    private PartyProperties theParty;
    private Vector3 partyStartPoint;

    private PlatformDestroyer[] platformList;

    private ScoreManager scoreManager;


	// Use this for initialization
	void Start () {
        initCamHolderPos = cameraHolder.transform.position;
        platformsStartPoint = platformGenerator.position;
		scoreManager = FindObjectOfType<ScoreManager>();
        theParty = partyHolder.GetComponent<PartyProperties>();
        partyStartPoint = partyHolder.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		
	}

    public void RestartGame()
    {
        StartCoroutine("RestartGameCo");
    }

    public IEnumerator RestartGameCo()
    {
        scoreManager.scoreIncreasing = false;
        //thePlayer.gameObject.SetActive(false);
        for(int i = 0; i < theParty.partyMembers.Length; i++){
            theParty.partyMembers[i].SetActive(false);
        }
        

        yield return new WaitForSeconds(0.5f);
        platformList = FindObjectsOfType<PlatformDestroyer>();

        for(int i = 0; i < platformList.Length; i++){
            platformList[i].gameObject.SetActive(false);
        }

     
        
        theParty.transform.position = partyStartPoint;
        for(int i = 0; i < theParty.partyMembers.Length; i++){
            theParty.partyMembers[i].transform.position = theParty.GetPartyMemberStartPos(i);
        }

        cameraHolder.transform.position = initCamHolderPos;
        platformGenerator.position = platformsStartPoint;
        
        for(int i = 0; i < theParty.partyMembers.Length; i++){
            theParty.partyMembers[i].SetActive(true);
            theParty.partyMembers[i].GetComponent<PlayerClass>().SetIsDead(false);
        }

        //thePlayer.gameObject.SetActive(true);
        scoreManager.scoreCount = 0;
        scoreManager.scoreIncreasing = true;
       
    }

    public void ShakeTheCamera(){
        //float Magnitude = 2f;
        //float Roughness = 10f;
        //float FadeOutTime = 5f;
    
        CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, 0, FadeOutTime);
    }

    public void RemoveFromCam(GameObject gameObject){
        cameraHolder.GetComponent<FocusCamera>().RemoveTarget(gameObject);
    }

}
