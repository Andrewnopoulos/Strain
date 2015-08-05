using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {

	public GameObject zombieprefab;
	public GameObject player;

	public float spawnRate = 1;
	private float spawnCooldown = 0;

	// Use this for initialization
	void Start () {
	
		spawnCooldown = spawnRate;
	}
	
	// Update is called once per frame
	void Update () {
	
		spawnCooldown -= Time.deltaTime;

		if (spawnCooldown <= 0) {
			spawnCooldown  = spawnRate;

			float randX = Random.Range(-50, 50);
			float randY = Random.Range(-50, 50);

			GameObject zombie = (GameObject)Instantiate(zombieprefab, new Vector3(randX, 1, randY), transform.rotation);
			zombie.layer = 10;
			Zombie script = zombie.GetComponent<Zombie>();
			script.target = player;
		}

	}
}
