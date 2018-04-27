using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MultiTargetCamera : MonoBehaviour {

    public List<GameObject> targets;

    public Vector3 offset;
    public float smoothTime = 0.5f;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;


    private Vector3 velocity;
    public Camera cam;


    private void LateUpdate()
    {
        if(targets.Count == 0)
        {
            return;
        }

        Move();
        //Zoom();
      
    }

    void Zoom()
    {
        //float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter );
        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);// only works in perspective mode



    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);

    }

    float GetGreatestDistance()
    {
        return EncapsulateActive().size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].transform.position;
        }

        return EncapsulateActive().center;

    }

    private Bounds EncapsulateActive(){
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero); 

        bool startingPointFound = false;

        for (int i = 0; i < targets.Count; i++)
        {
            if(!targets[i].GetComponent<PlayerClass>().GetIsDead() && startingPointFound == false){
                bounds = new Bounds(targets[i].transform.position, Vector3.zero);
                bounds.Encapsulate(targets[i].transform.position);
                startingPointFound = !startingPointFound;
            }else if(startingPointFound){
                bounds.Encapsulate(targets[i].transform.position);
            }
        }

        return bounds;
    }

    public void RemoveTarget(GameObject deadPlayer){
        targets.Remove(deadPlayer);
    }
}
