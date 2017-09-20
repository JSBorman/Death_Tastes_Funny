using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInObjects : MonoBehaviour {

    public GameObject objectToFadeIn;
    public float alphaOnCollision = 1f;
    private bool startFade = false;

    // Use this for initialization
    void Start() {
        Color tmp = objectToFadeIn.GetComponent<SpriteRenderer>().color;
        tmp.a = 0;//alphaOnCollision;
        objectToFadeIn.GetComponent<SpriteRenderer>().color = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        if (startFade)
        {
            foreach (SpriteRenderer s in objectToFadeIn.GetComponentsInChildren<SpriteRenderer>())
            {
                Color tmp = objectToFadeIn.GetComponent<SpriteRenderer>().color;
                tmp.a = 0;//+= Time.deltaTime / 10000f;//alphaOnCollision;
                s.color = tmp;
            }

           
        }
    }


void OnTriggerEnter2D(Collider2D other)
    {
        startFade = true;
    }
}
