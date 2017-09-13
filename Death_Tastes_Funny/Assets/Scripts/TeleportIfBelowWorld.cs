using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportIfBelowWorld : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    public GameObject topSpawner;
    public GameObject bottomCollider;

	void Update () {
        if (transform.position.y < bottomCollider.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, topSpawner.transform.position.y);
        }
		
	}
}
