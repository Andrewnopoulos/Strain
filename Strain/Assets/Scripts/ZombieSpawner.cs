using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	// Use this for initialization
	void Start () {

		zombieSpawnCooldown = zombieSpawnRate;
		humanSpawnCooldown = humanSpawnRate;

		while (currentZombie < maxZombie) 
		{
            SpawnZombie(RandomPos(100));
		}

		while (currentHuman < maxHuman) 
		{
            SpawnHuman(RandomPos(100));
		}

		npcList.Add (player);
	}
	
	// Update is called once per frame
	void Update () {
	
		zombieSpawnCooldown -= Time.deltaTime;
		humanSpawnCooldown -= Time.deltaTime;

		if (zombieSpawnCooldown <= 0 && currentZombie < maxZombie) 
		{
			zombieSpawnCooldown  = zombieSpawnRate;

            SpawnZombie(RandomPos(100));
		}

		if (humanSpawnCooldown <= 0 && currentHuman < maxHuman) 
		{
			humanSpawnCooldown  = humanSpawnRate;

            SpawnHuman(RandomPos(100));
		}
	}

	Vector3 RandomPos(float range)
	{
        float randX = Random.Range(player.transform.position.x - range,player.transform.position.x + range);
        float randY = Random.Range(player.transform.position.y - range,player.transform.position.y + range);

        if (randX < player.transform.position.x + 30 && randX > player.transform.position.x)
            randX = player.transform.position.y + 30;
        else if (randX < player.transform.position.x && randX > player.transform.position.x - 30)
            randX = player.transform.position.y - 30;

        if (randY < player.transform.position.y + 30 && randY > player.transform.position.y)
            randY = player.transform.position.y + 30;
        else if (randY < player.transform.position.y && randY > player.transform.position.y - 30)
            randY = player.transform.position.y - 30;

        return new Vector3(randX, 1, randY);
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
}
