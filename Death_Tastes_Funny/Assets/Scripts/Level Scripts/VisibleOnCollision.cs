using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleOnCollision : MonoBehaviour {

    public float alphaOnCollision = 1;

	// Use this for initialization
	void Start () {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 0;
        GetComponent<SpriteRenderer>().color = tmp;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = alphaOnCollision;
        GetComponent<SpriteRenderer>().color = tmp;

    }


    void OnCollisionExit2D(Collision2D other)
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 0;
        GetComponent<SpriteRenderer>().color = tmp;

    }
}
