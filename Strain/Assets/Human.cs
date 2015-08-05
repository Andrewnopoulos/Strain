using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour {
	
	private Vector3 target;
	private NavMeshAgent navComponent;

	public GameObject groundReference;
	private ZombieSpawner zombieSpawnerReference;

	public bool alive = true;

	// Use this for initialization
	void Start () {
	
		navComponent = this.transform.GetComponent<NavMeshAgent> ();

		zombieSpawnerReference = groundReference.GetComponent<ZombieSpawner>();

		Wander ();
	}
	
	// Update is called once per frame
	void Update () {
	

		navComponent.SetDestination(target);

		if((transform.position - target).magnitude < 0.5)
		{ 
			Wander(); 
		}

		if (!alive) {
			Die ();
		}
	}

	void Wander()
	{
		target = Random.insideUnitSphere * 50;
		target.y = 1;
	}

	void Die()
	{
		zombieSpawnerReference.SpawnZombie (transform);

		Destroy (gameObject);
	}
}
