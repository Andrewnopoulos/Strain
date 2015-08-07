using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	public Transform target;
	public Vector3 targetPosition;

	public float speed;
    public float health;
    public float damage;

    public float biteSpeed;
    private float biteCooldown = 0;

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

        biteCooldown = biteSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		if (isZombie) 
		{
            biteCooldown -= Time.deltaTime;

            FindNearestHuman();

			if (target) 
			{
				navComponent.SetDestination(target.position);
			}
			
			if ((target.position - transform.position).magnitude < 100 && biteCooldown <= 0) {
				BiteHuman();
			}
		} 
		else 
		{

            FindNearestZombie();

            if ((transform.position - target.position).magnitude < 2000)
            {
                Flee();
            }
			if((transform.position - targetPosition).magnitude < 100)
			{ 
				Wander(); 
			}
		}

		if (isZombie)
			navComponent.SetDestination (target.position);
		else
			navComponent.SetDestination (targetPosition);

        if (health <= 0)
        {
            alive = false;
        }

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
        targetPosition = Random.insideUnitSphere * 10000;
		targetPosition = new Vector3 (targetPosition.x, transform.position.y, targetPosition.z);
	}

	public void BecomeZombie()
	{
		isZombie = true;
		gameObject.tag = "Zombie";
		gameObject.layer = 10;
		myMat.material = red;

        FindNearestHuman();
	}

	void FindNearestHuman()
	{
		float smallestDist = 999999;
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
    void FindNearestZombie()
    {
        float smallestDist = 999999;
        foreach (GameObject zombie in zombieSpawnerReference.npcList)
        {
            if (zombie == null)
                continue;
            if (!(zombie.tag == "Zombie"))
                continue;

            if ((zombie.transform.position - transform.position).magnitude < smallestDist)
            {
                smallestDist = (zombie.transform.position - transform.position).magnitude;
                target = zombie.transform;
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
                Player script = target.GetComponent<Player>();
                script.health -= damage;
			}
		}

        biteCooldown = biteSpeed;
	}

	void Flee()
	{
        Vector3 fleeDir = transform.position - target.position;
        targetPosition = transform.position + (Vector3.Normalize(fleeDir) * 1000);
	}
}
