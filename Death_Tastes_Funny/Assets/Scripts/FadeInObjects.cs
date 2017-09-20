using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInObjects : MonoBehaviour {

    public GameObject objectToFadeIn;
    public float alphaOnCollision = 1f;
    private bool startFade = false;

    // Use this for initialization
    void Start() {
        /*
        foreach (SpriteRenderer s in objectToFadeIn.GetComponentsInChildren<SpriteRenderer>())
        {
            Color tmp = s.color;
            tmp.a = 0;//+= Time.deltaTime / 10000f;//alphaOnCollision;
            s.color = tmp;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (startFade)
        {
            foreach (SpriteRenderer s in objectToFadeIn.GetComponentsInChildren<SpriteRenderer>())
            {
                Color tmp = s.color;
                tmp.a += Time.deltaTime / 3f;//alphaOnCollision;
                s.color = tmp;

                if (tmp.a >= 1)
                {
                    startFade = false;
                }
            }

        }
    }


void OnTriggerEnter2D(Collider2D other)
    {
        startFade = true;
        print("starting fade...");


        foreach (SpriteRenderer s in objectToFadeIn.GetComponentsInChildren<SpriteRenderer>())
        {
            Color tmp = s.color;
            tmp.a = 0;
            s.color = tmp;
        }
    }
}
