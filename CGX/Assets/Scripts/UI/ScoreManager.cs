using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public Text highScoreText;

	public float scoreCount;
	public float highScoreCount;

	public float pointsPerSecond;
	public bool scoreIncreasing;

	void Awake(){
		if(PlayerPrefs.HasKey("HighScore") != null){
			highScoreCount = PlayerPrefs.GetFloat("HighScore");
		}	
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(scoreIncreasing){
			scoreCount += pointsPerSecond * Time.deltaTime;
		
		}

		scoreText.text = "Score: " + Mathf.Round(scoreCount);

		if(scoreCount > highScoreCount){
			
			highScoreCount = scoreCount;
			PlayerPrefs.SetFloat("HighScore", highScoreCount);
		}

		highScoreText.text = "High Score: " + Mathf.Round(highScoreCount);
	}

	public void AddScore(int pointsValue){
		scoreCount += pointsValue;
	}	

}
