using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {


	public bool scrolling, paralax;


	public float paralaxSpeed;

	private Transform cameraTransform;
	private GameObject[] layers;
	private float viewZone = 10f;
	private int leftIndex;
	private int rightIndex;
	private float lastCameraX;

	public float randomSpacingRange;


	public ObjectPooler[] bgPools;
	public int numBGSGenerated;

	private void Start()
	{
		cameraTransform = Camera.main.transform;
		lastCameraX = cameraTransform.position.x;
		layers = new GameObject[numBGSGenerated];



		for (int i =0; i < numBGSGenerated; i ++)
		{
			//Random.Range(0,bgPools.Length-1)
			layers [i] =  bgPools[Random.Range(0,bgPools.Length)].GetPooledObject();
			if (i == 0) {
				layers [i].transform.position = new Vector3 (transform.position.x, transform.position.y, 0.0f);
			} else {

				layers [i].transform.position = new Vector3 (   layers [i-1].transform.position.x  + layers [i].GetComponent<SpriteRenderer> ().bounds.size.x/2 + Random.Range(0,randomSpacingRange)  , transform.position.y, 0.0f);
			}

			layers [i].SetActive (true);
		}

		leftIndex = 0;
		rightIndex = layers.Length - 1;



	}


	private void Update()
	{
		
		if (paralax)
		{
			float deltaX = cameraTransform.position.x - lastCameraX;
			//transform.position += Vector3.right * (deltaX * paralaxSpeed);
			foreach (GameObject bg in layers) {
				bg.transform.position += Vector3.right * (deltaX * paralaxSpeed);
			}
		}

		lastCameraX = cameraTransform.position.x;


		if (scrolling)
		{
			if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
			{
				ScrollLeft();
			}

			if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
			{
				ScrollRight();
			}
		}
	}


	private void ScrollLeft()
	{
		layers [rightIndex].SetActive (false);
		layers[rightIndex] = bgPools[Random.Range(0,bgPools.Length)].GetPooledObject();
		layers[rightIndex].transform.position = new Vector3( (layers[leftIndex].transform.position.x - layers[leftIndex].GetComponent<SpriteRenderer>().bounds.size.x), transform.position.y, 0);
		layers [rightIndex].SetActive (true);

		leftIndex = rightIndex;
		rightIndex--;
		if (rightIndex < 0)
		{
			rightIndex = layers.Length - 1;
		}
	}

	private void ScrollRight()
	{
		layers [leftIndex].SetActive (false);
		layers[leftIndex] = bgPools[Random.Range(0,bgPools.Length)].GetPooledObject();
		layers[leftIndex].transform.position = new Vector3((layers[rightIndex].transform.position.x + layers[leftIndex].GetComponent<SpriteRenderer>().bounds.size.x) , transform.position.y, 0);
		layers [rightIndex].SetActive (true);


		rightIndex = leftIndex;
		leftIndex++;
		if (leftIndex == layers.Length)
		{
			leftIndex = 0;
		}
	}
}
