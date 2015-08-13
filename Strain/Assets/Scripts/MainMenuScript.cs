using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

    public string level;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Test()
    {
        Debug.Log("Button Pressed");
        Application.LoadLevel(level);
    }

    public void SetGameModeShooting()
    {
        ZombieSpawner spawner = GetComponent<ZombieSpawner>();
        Time.timeScale = 1;
        spawner.currentMode = GAMEMODE.SHOOTAEN;
        spawner.ShootingStart();
    }

    public void SetGameModeSimulating()
    {
        ZombieSpawner spawner = GetComponent<ZombieSpawner>();
        Time.timeScale = 1;
        spawner.currentMode = GAMEMODE.SIMULATAEN;
        spawner.SimulationStart();
    }
}
