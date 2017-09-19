using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentOnCollision : MonoBehaviour {

    public float alphaOnCollision = 100;


    void OnTriggerEnter2D(Collider2D other)
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = alphaOnCollision;
        GetComponent<SpriteRenderer>().color = tmp;

    }


    void OnTriggerExit2D(Collider2D other)
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 1;
        GetComponent<SpriteRenderer>().color = tmp;

    }
}
