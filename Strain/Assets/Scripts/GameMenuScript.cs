using UnityEngine;
using System.Collections;

public class GameMenuScript : MonoBehaviour {

    public GameObject spawner;
    public GameObject self;

	// Use this for initialization
	void Start () {
        spawner.GetComponent<ZombieSpawner>().currentMode = GAMEMODE.PAUSED;
	}
	
	// Update is called once per frame
	void Update () {
	    if (spawner.GetComponent<ZombieSpawner>().currentMode != GAMEMODE.PAUSED)
        {
            self.SetActive(false);
        }
        else
        {
            self.SetActive(true);
        }
	}
}
