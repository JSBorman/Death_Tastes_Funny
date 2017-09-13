using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager INSTANCE;
    public bool Debug;

	// Use this for initialization
	void Start () {
		if (INSTANCE != null) {
            enabled = false;
            return;
        } else {
            INSTANCE = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
