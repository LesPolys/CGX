using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;

    public float distanceBetweenMin;
    public float distanceBetweenMax; // dist between plat

    public ObjectPooler[] theObjectPools;


    //public GameObject[] thePlatforms;
    private int platformSelector;
    private float[] platformWidths;


    //platform y pos stuff
    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightDifference;
    private float heightChange;


    public CoinGenerator coinGenerator;
    public float randomCoinThreshhold;

    // Use this for initialization
    void Start () {
        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;


        coinGenerator = FindObjectOfType<CoinGenerator>();
	}
	
	// Update is called once per frame
	void Update () {

        if(transform.position.x < generationPoint.position.x)
        {



            distanceBetween = Random.Range(distanceBetweenMin,distanceBetweenMax);
            platformSelector = Random.Range(0, theObjectPools.Length);
            heightChange = transform.position.y + Random.Range(-maxHeightDifference, maxHeightDifference);
            if(heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }else if(heightChange < minHeight)
            {
                heightChange = minHeight;
            }
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] /2) + distanceBetween, heightChange, transform.position.z);

          

            //Instantiate(/*thePlatform*/ theObjectPools[platformSelector], transform.position, transform.rotation);


            
            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;

            newPlatform.SetActive(true);


            if(Random.Range(0f,100f) < randomCoinThreshhold){
                coinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y  , transform.position.z));
            }
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);


        }

    }
}
