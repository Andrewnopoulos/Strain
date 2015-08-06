using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {
	
	public Transform target;
	public Vector3 targetPosition;
	public float speed;

	private NavMeshAgent navComponent;

	public GameObject groundReference;
	public ZombieSpawner zombieSpawnerReference;

	public bool isZombie;
	public bool alive = true;

	public Renderer myMat;

	public Material red;
	public Material green;

	// Use this for initialization
	void Start () {
	
		zombieSpawnerReference = groundReference.GetComponent<ZombieSpawner>();
		
		navComponent = this.transform.GetComponent<NavMeshAgent> ();
		speed = navComponent.speed;

		if (!isZombie) {
			Wander ();
			myMat.material = green;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
		if (isZombie) 
		{
			FindNearestHuman ();
			
			if (target) 
			{
				navComponent.SetDestination(target.position);
			}
			
			if ((target.position - transform.position).magnitude < 2) {
				BiteHuman();
			}
		} 
		else 
		{
			if((transform.position - targetPosition).magnitude < 0.5)
			{ 
				Wander(); 
			}
		}

		if (isZombie)
			navComponent.SetDestination (target.position);
		else
			navComponent.SetDestination (targetPosition);

		if (!alive) {
			KillYourself();
		}

	}

	public void KillYourself()
	{
		if (!isZombie)
			zombieSpawnerReference.currentHuman--;
		else
			zombieSpawnerReference.currentZombie--;

		Destroy (gameObject);
	}

	void Wander()
	{
		targetPosition = Random.insideUnitSphere * 50;
		targetPosition = new Vector3 (targetPosition.x, 1, targetPosition.z);
	}

	public void BecomeZombie()
	{
		isZombie = true;
		gameObject.tag = "Zombie";
		gameObject.layer = 10;
		myMat.material = red;

	}

	void FindNearestHuman()
	{
		float smallestDist = 9999;
		foreach (GameObject human in zombieSpawnerReference.npcList)
		{
			if (human == null)
				continue;
			if (!(human.tag == "Human" || human.tag == "Player"))
			    continue;

			if ((human.transform.position - transform.position).magnitude < smallestDist)
			{
				smallestDist = (human.transform.position - transform.position).magnitude;
				target = human.transform;
			}
		}
	}
	
	void BiteHuman()
	{
		if (target != null) 
		{
			if (target.gameObject.tag == "Human")
			{
			NPC script = target.GetComponent<NPC>();
			script.BecomeZombie();
            zombieSpawnerReference.currentZombie++;
            zombieSpawnerReference.currentHuman--;


			}
			else if (target.gameObject.tag == "Player")
			{
				//player take damage
			}
		}
	}

	void FindNearestZombie()
	{

	}

	void Flee()
	{

	}
}
