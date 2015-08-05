using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour {

	public GameObject zombieprefab;
	public GameObject humanprefab;
	public GameObject player;

	public float zombieSpawnRate = 1;
	private float zombieSpawnCooldown = 0;

	public float humanSpawnRate = 1;
	private float humanSpawnCooldown = 0;

	public List<GameObject> humanList;

	// Use this for initialization
	void Start () {
	
		zombieSpawnCooldown = zombieSpawnRate;
		humanSpawnCooldown = humanSpawnRate;
	}
	
	// Update is called once per frame
	void Update () {
	
		zombieSpawnCooldown -= Time.deltaTime;
		humanSpawnCooldown -= Time.deltaTime;

		if (zombieSpawnCooldown <= 0) {
			zombieSpawnCooldown  = zombieSpawnRate;

			float randX = Random.Range(-50, 50);
			float randY = Random.Range(-50, 50);

			GameObject zombie = (GameObject)Instantiate(zombieprefab, new Vector3(randX, 1, randY), transform.rotation);
			zombie.layer = 10;
			Zombie script = zombie.GetComponent<Zombie>();
			script.target = player;
			script.groundReference = gameObject;
		}

		if (humanSpawnCooldown <= 0) {
			humanSpawnCooldown  = humanSpawnRate;

			
			float randX = Random.Range(-50, 50);
			float randY = Random.Range(-50, 50);

			GameObject newHuman = (GameObject)Instantiate( humanprefab, new Vector3(randX, 1, randY), transform.rotation);
			Human script = newHuman.GetComponent<Human>();
			script.groundReference = gameObject;
			humanList.Add(newHuman);
		}

	}

	public void SpawnZombie(Transform location)
	{
		GameObject zombie = (GameObject)Instantiate(zombieprefab, location.position, location.rotation);
		zombie.layer = 10;
		Zombie script = zombie.GetComponent<Zombie>();
		script.target = player;
		script.groundReference = gameObject;
	}
}
