using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    // chromosome class that contains virus values and crossover/mutation methods
    // currently unable to change genes after the chromosome is created (except by mutation)
    private class Chromosome
    {
        private static int length = 6;
        public static float mutationRate = 0.05f;
        public static float mutationStrength = 0.1f; // maximum mutation strength
        public static float randomInitValue = 0.4f;

          /*
          * [0] - speed
          * [1] - health
          * [2] - damage
          * [3] - infectivity
          * [4] - sight
          * [5] - sound
          * [6] - smell
          */
        private float[] genes = new float[length];
        private float topGene = 0;
        private int topIndex = -1;

        public Chromosome()
        {
            // initialize chromosome with random values between 0 and 0.4f
            for (int i = 0; i < length; i++)
            {
                genes[i] = Random.Range(0.0f, randomInitValue);
            }
        }
        public Chromosome(float[] input)
        {
            if (input.Length != length)
            {
                for (int i = 0; i < length; i++)
                {
                    genes[i] = 0;
                }
                return;
            }

            for (int i = 0; i < length; i++)
            {
                genes[i] = input[i];
            }

            EvaluateStrength();
        }

        public float Get(int index)
        {
            if (index >= length)
            {
                return 0;
            }

            return genes[index];
        }

        // code for blending two virus chromosomes
        // if player is already infected by one strain and is again bitten,
        // the strains can crossover
        public Chromosome Crossover(Chromosome input)
        {
            int crossoverPoint = Random.Range(0, length-1);
            float[] output1 = new float[length];
            float[] output2 = new float[length];
            for (int i = 0; i < crossoverPoint; i++)
            {
                output1[i] = genes[i];
                output2[i] = input.genes[i];
            }
            for (int i = crossoverPoint; i < length; i++)
            {
                output2[i] = genes[i];
                output1[i] = input.genes[i];
            }

            Chromosome out1 = new Chromosome(output1);
            Chromosome out2 = new Chromosome(output2);

            if (out1.EvaluateStrength() > out2.EvaluateStrength())
            {
                out1.Mutate();
                return out1;
            }
            out2.Mutate();
            return out2;
        }

        // Mutates chromosome, then re-evaluates its strength
        public void Mutate()
        {
            // TODO mutation code that is more dependent on what the max value is
            for (int i = 0; i < length; i++)
            {
                if (Random.Range(0.0f, 1.0f) > mutationRate)
                {
                    genes[i] += Random.Range(-0.5f * mutationStrength, mutationStrength);
                }
            }

            topIndex = -1;
            EvaluateStrength();
        }

        // returns the maximum value
        public float EvaluateStrength()
        {
            if (topIndex != -1)
            {
                return topGene;
            }

            float maxValue = 0;
            for (int i = 0; i < length; i++)
            {
                if (genes[i] > maxValue)
                {
                    maxValue = genes[i];
                    topIndex = i;
                }
            }
            topGene = maxValue;
            return maxValue;
        }
    }

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
