﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager INSTANCE;
    public bool Debug;
    public GameObject[] NPCs;
    public GameObject[] NPCspawns;

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
        if (Input.GetKeyDown(KeyCode.Equals)) {
            RefreshNPCs();
        }
	}

    public void RefreshNPCs() {
        for (int i =0; i<NPCs.Length; i++) {
            NPC n = NPCs[i].GetComponent<NPC>();
            Transform[] spawns =  NPCspawns[i].GetComponentsInChildren<Transform>();
            n.refresh(spawns[Random.Range(0, spawns.Length)].position);
        }
    }
}
