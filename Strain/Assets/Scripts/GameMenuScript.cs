﻿using UnityEngine;
using System.Collections;

public class GameMenuScript : MonoBehaviour {

    public ZombieSpawner spawner;
    public GameObject self;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (spawner.currentMode != GAMEMODE.PAUSED)
        {
            self.SetActive(false);
        }
        else
        {
            self.SetActive(true);
        }
	}
}
