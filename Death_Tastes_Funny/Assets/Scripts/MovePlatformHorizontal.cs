using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformHorizontal : MonoBehaviour {

    public float speed = 10;
    //the total distance the platform travels
    public float travelDistance = 100;
    //the distance the platform currently traveled this cycle
    private float distanceTraveled = 0;
    //initial direction of movement, 0 = right, 1 = left
    public int direction = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        distanceTraveled += Time.deltaTime * speed;

        if (distanceTraveled >= travelDistance)
        {
            distanceTraveled = 0;
            //swap direction value when travel distance is reached
            direction = (direction == 0) ? 1 : 0;
        }



        if (direction == 0)
        {
            transform.Translate(Time.deltaTime * speed, 0, 0);
        }
        else if (direction == 1)
        {
            transform.Translate(Time.deltaTime * -speed, 0, 0);
        }

    }


}
