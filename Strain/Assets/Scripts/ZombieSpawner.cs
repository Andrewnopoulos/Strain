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

    public int activeHumanCount;
    public int infectedHumanCount;
    public int zombieCount;

    private float humanSpawnRate = 5; // spawns a human every 5 seconds

	public int initialZombieCount = 3;
	private float zombieSpawnCooldown = 0;

	public int initialHumanCount = 100;
	private float humanSpawnCooldown = 0;

    public float simSpeed = 1.0f;

	public List<GameObject> npcList;

    public GAMEMODE currentMode;

	// Use this for initialization
	void Start () {
        currentMode = GAMEMODE.PAUSED;
       // ShootingStart();
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentMode)
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

    public void RemoveNPC(GameObject obj)
    {
        npcList.Remove(obj);
    }

    void CountNPCs()
    {
        activeHumanCount = 0;
        infectedHumanCount = 0;
        zombieCount = 0;
        foreach(GameObject npc in npcList)
        {
            NPC script = npc.GetComponent<NPC>();
            if (script == null)
            {
                continue;
            }
            if (script.isZombie)
            {
                zombieCount++;
            }
            else if (script.incubating)
            {
                infectedHumanCount++;
            }
            else
            {
                activeHumanCount++;
            }
        }
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
	}

    void InitZombies()
    {
        foreach(GameObject obj in npcList)
        {
            if (obj.tag == "Zombie")
            {
                NPC script = obj.GetComponent<NPC>();

                float[] initialZombieValues = new float[NPC.Chromosome.GetLength()];

                for (int i = 0; i < NPC.Chromosome.GetLength(); i++)
                {
                    initialZombieValues[i] = Random.Range(0.0f, 0.3f);
                }

                script.InitializeZombie(initialZombieValues);
            }
        }

        
    }

    public void SimulationStart()
    {
        currentMode = GAMEMODE.SIMULATAEN;
        Time.timeScale = 1;
    }

    void SimulationUpdate()
    {
        Time.timeScale = simSpeed;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            simSpeed += 0.2f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            simSpeed -= 0.2f;
        }

    }

    public void ShootingStart()
    {
        currentMode = GAMEMODE.SHOOTAEN;
        Time.timeScale = 1;

        humanSpawnCooldown = humanSpawnRate;

        for (int i = 0; i < initialHumanCount; i++)
        {
            SpawnHuman(RandomPos(80));
        }

        for (int i = 0; i < initialZombieCount; i++)
        {
            SpawnZombie(RandomPos(80));
        }

        InitZombies();

        npcList.Add(player);
    }

    void ShootingUpdate()
    {
        humanSpawnCooldown -= Time.deltaTime;

        if (humanSpawnCooldown <= 0)
        {
            humanSpawnCooldown = humanSpawnRate;

            CountNPCs();

            SpawnHuman(RandomPos(80));
        }

        //if (zombieSpawnCooldown <= 0 && currentZombie < maxZombie)
        //{
        //    zombieSpawnCooldown = zombieSpawnRate;

        //    SpawnZombie(RandomPos(80));
        //}

        //if (humanSpawnCooldown <= 0 && currentHuman < maxHuman)
        //{
        //    humanSpawnCooldown = humanSpawnRate;

        //    SpawnHuman(RandomPos(80));
        //}
    }

    void PausedUpdate()
    {
        Time.timeScale = 0;
    }

}
