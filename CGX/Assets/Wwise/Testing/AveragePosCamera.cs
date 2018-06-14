using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class AveragePosCamera : MonoBehaviour {
     public Transform[] players;

    public Vector3 offset;
     public float smoothTime = 0.5f;
     private Vector3 velocity;


     private void Update() {
        SetCameraPos();

     }
 
     void SetCameraPos() {
         Vector3 middle = Vector3.zero;
         int numPlayers = 0;
 
         for(int i=0; i<players.Length; ++i) {
             if(players[i] == null){
                continue; //skip, since player is deleted
             }
             middle += players[i].position;
             numPlayers++;
         }//end for every player
 
         //take average:
         middle /= numPlayers;
         Vector3 newPosition = middle + offset;
      //  transform.position = Vector3.Lerp(transform.position, new Vector3(middle.x, middle.y, -10.0f), Time.deltaTime);
                transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
     }
 
 }
 
