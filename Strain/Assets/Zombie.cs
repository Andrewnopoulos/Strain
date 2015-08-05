using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Zombie : MonoBehaviour {

	public GameObject target;

	private Transform targetTransform;
	private NavMeshAgent navComponent;

	public float speed;

	public GameObject groundReference;
	private ZombieSpawner zombieSpawnerReference;

	// Use this for initialization
	void Start () {

		zombieSpawnerReference = groundReference.GetComponent<ZombieSpawner>();

		targetTransform = target.transform;
		
		navComponent = this.transform.GetComponent<NavMeshAgent> ();
		speed = navComponent.speed;
	}
	
	// Update is called once per frame
	void Update () {

		FindNearestHuman ();

		if (targetTransform) 
		{
			navComponent.SetDestination(targetTransform.position);
		}
	}

	void FindNearestHuman()
	{
		float smallestDist = 9999;
		foreach (GameObject fuckder in zombieSpawnerReference.humanList)
		{
			if ((fuckder.transform.position - transform.position).magnitude < smallestDist)
			{
				smallestDist = (fuckder.transform.position - transform.position).magnitude;
				target = fuckder;
				targetTransform = target.transform;
			}
		}
	}
}
