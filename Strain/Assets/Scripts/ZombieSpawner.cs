using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GAMEMODE
{
    SHOOTAEN = 0,
    SIMULATAEN = 1,
    PAUSED = 2
}

public class ZombieSpawner : MonoBehaviour {

	public GameObject npcprefab;
	public GameObject player;

	public float maxNPC = 20;
	public float maxZombie = 1;
	public float maxHuman = 20;

	public float currentHuman = 0;
	public float currentZombie = 0;

	public float zombieSpawnRate = 1;
	private float zombieSpawnCooldown = 0;

	public float humanSpawnRate = 100;
	private float humanSpawnCooldown = 0;

	public List<GameObject> npcList;

    public GAMEMODE currentMode;

	// Use this for initialization
	void Start () {
        currentMode = GAMEMODE.PAUSED;
       // ShootingStart();
	}
	
	// Update is called once per frame
	void Update () {
        switch(currentMode)
        {
            case GAMEMODE.SHOOTAEN:
                ShootingUpdate();
                break;
            case GAMEMODE.SIMULATAEN:
                SimulationUpdate();
                break;
            case GAMEMODE.PAUSED:
                PausedUpdate();
                break;
        }
	}

    Vector3 RandomVec3(float range)
    {
        float randX = Random.Range(-range, range);
        float randY = Random.Range(-range, range);

        return new Vector3(randX, 1, randY);
    }

	Vector3 RandomPos(float range)
	{
        Vector3 position;
        while (true)
        {
            position = RandomVec3(range);

            if (Vector3.Magnitude(position - player.transform.position) > 30)
                break;
        }

        return position;
	}

	void SpawnHuman(Vector3 position)
	{
		GameObject newHuman = (GameObject)Instantiate(npcprefab, position, transform.rotation);
		newHuman.layer = 12;
		newHuman.tag = "Human";
		NPC script = newHuman.GetComponent<NPC>();
		script.isZombie = false;
		script.groundReference = gameObject;
		npcList.Add(newHuman);

		currentHuman++;
	}

	void SpawnZombie(Vector3 position)
	{
		GameObject newZombie = (GameObject)Instantiate(npcprefab, position, transform.rotation);
		newZombie.layer = 10;
		newZombie.tag = "Zombie";
		NPC script = newZombie.GetComponent<NPC>();
		script.isZombie = true;
		script.groundReference = gameObject;
		npcList.Add(newZombie);

		currentZombie++;
	}

    public void SimulationStart()
    {

    }

    public void ShootingStart()
    {
        zombieSpawnCooldown = zombieSpawnRate;
        humanSpawnCooldown = humanSpawnRate;

        while (currentZombie < maxZombie)
        {
            SpawnZombie(RandomPos(80));
        }

        while (currentHuman < maxHuman)
        {
            SpawnHuman(RandomPos(80));
        }

        npcList.Add(player);
    }

    void SimulationUpdate()
    {

    }

    void ShootingUpdate()
    {
        zombieSpawnCooldown -= Time.deltaTime;
        humanSpawnCooldown -= Time.deltaTime;

        if (zombieSpawnCooldown <= 0 && currentZombie < maxZombie)
        {
            zombieSpawnCooldown = zombieSpawnRate;

            SpawnZombie(RandomPos(80));
        }

        if (humanSpawnCooldown <= 0 && currentHuman < maxHuman)
        {
            humanSpawnCooldown = humanSpawnRate;

            SpawnHuman(RandomPos(80));
        }
    }

    void PausedUpdate()
    {
        Time.timeScale = 0;
    }

}
