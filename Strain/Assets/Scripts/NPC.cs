using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    /// <summary>
    /// chromosome class that contains virus values and crossover/mutation methods
    /// currently unable to change genes after the chromosome is created (except by mutation)
    /// </summary>
    public class Chromosome
    {
        private static int length = 8;
        public static float mutationRate = 0.2f;
        public static float mutationStrength = 0.1f; // maximum mutation strength
        public static float randomInitValue = 0.4f;

        private float[] genes = new float[length];
        private float topGene = 0;
        private int topIndex = -1;

        /// <summary>
        /// Initializes randomly generated chromosome based on an inbuilt randomInitValue
        /// </summary>
        public Chromosome()
        {
            // initialize chromosome with random values between 0 and 0.4f
            for (int i = 0; i < length; i++)
            {
                genes[i] = Random.Range(0.0f, randomInitValue);
            }
            EvaluateStrength();
        }

        /// <summary>
        /// Initialized a chromosome where every gene is the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public Chromosome(float initValue)
        {
            for (int i = 0; i < length; i++)
            {
                genes[i] = initValue;
            }
            EvaluateStrength();
        }

        /// <summary>
        /// Initialized a chromosome based on an input array.
        /// If input array is wrong length, initializes a zero value chromosome
        /// </summary>
        /// <param name="input"></param>
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

        /// <summary>
        /// [0] - speed<para />
        /// [1] - health<para />
        /// [2] - damage<para />
        /// [3] - infectivity<para />
        /// [4] - incubation<para />
        /// [5] - sight<para />
        /// [6] - sound<para />
        /// [7] - smell
        /// </summary>
        /// <param name="index">Index of the gene to retrieve the value of</param>
        /// <returns>Value of the gene at the requested index.
        /// Returns 0 if index exceeds chromosome length</returns>
        public float Get(int index)
        {
            if (index >= length)
            {
                return 0;
            }

            return genes[index];
        }

        /// <summary>
        /// Returns the length of the chromosomes
        /// </summary>
        /// <returns>Chromosome length</returns>
        public static int GetLength()
        {
            return length;
        }

        /// <summary>
        /// code for blending two virus chromosomes
        /// if player is already infected by one strain and is again bitten,
        /// the strains can crossover
        /// </summary>
        /// <param name="input">Input chromosome to perform crossover with</param>
        /// <returns>Child chromosome</returns>
        public Chromosome Crossover(Chromosome input)
        {
            // if either chromosome is zero, return the other one
            if (input.EvaluateStrength() == 0)
            {
                return this;
            }else if (EvaluateStrength() == 0)
            {
                return input;
            }

            // find crossover point and perform crossover
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

            // mutate and return chromosome with a higher max value
            if (out1.EvaluateStrength() > out2.EvaluateStrength())
            {
                out1.Mutate();
                return out1;
            }
            out2.Mutate();
            return out2;
        }

        /// <summary>
        /// Mutates chromosome, then re-evaluates its strength
        /// </summary>
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

        /// <summary>
        /// Evaluates the highest value in the chromosome, storing and returning that value.
        /// If it's recently calculated the strength, it will simply return the pre-calculated strength value.
        /// </summary>
        /// <returns></returns>
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
    public float infectivity;
    public float incubationTime = 0;

    public bool incubating;

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

    private Chromosome virusStrain;
    private int chromosomeLength;

    public float minSpeed = 3;
    public float varianceSpeed = 10;
    public float minHealth = 100;
    public float varianceHealth = 300;
    public float minDamage = 10;
    public float varianceDamage = 30;
    public float minInfectivity = 40;
    public float varianceInfectivity = 60;
    public float maxIncubation = 150;
    public float varianceIncubation = 149;

	// Use this for initialization
	void Start () {
        chromosomeLength = Chromosome.GetLength();
		zombieSpawnerReference = groundReference.GetComponent<ZombieSpawner>();

        virusStrain = new Chromosome(0.0f);

        chromosomeLength = Chromosome.GetLength();

        UpdateStats();

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
			
			if ((target.position - transform.position).magnitude < 2 && biteCooldown <= 0) {
				BiteHuman();
			}
		} 
		else 
		{

            FindNearestZombie();

            if ((transform.position - target.position).magnitude < 10)
            {
                Flee();
            }
			if((transform.position - targetPosition).magnitude < 2)
			{ 
				Wander(); 
			}

            if (incubating)
            {
                incubationTime -= Time.deltaTime;
                if (incubationTime <= 0)
                {
                    BecomeZombie();
                }
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

    /// <summary>
    /// Updates NPC stats (speed, health, damage, infectivity) based on chromosome values
    /// </summary>
    private void UpdateStats()
    {
        speed = minSpeed + virusStrain.Get(0) * varianceSpeed;
        navComponent = this.transform.GetComponent<NavMeshAgent>();
        navComponent.speed = speed;

        health = minHealth + virusStrain.Get(1) * varianceHealth;

        damage = minDamage + virusStrain.Get(2) * varianceDamage;

        infectivity = minInfectivity + virusStrain.Get(3) * varianceInfectivity;

        // TODO check for values exceeding certain limits in here
        // Place triggers for evolution here
    }

	public void KillYourself()
	{
		if (!isZombie)
			zombieSpawnerReference.currentHuman--;
		else
			zombieSpawnerReference.currentZombie--;

		Destroy (gameObject);
	}

    private void Infect(Chromosome inputVirus)
    {
        virusStrain = virusStrain.Crossover(inputVirus);

        // sets the incubation time if it's not already incubating
        if (!incubating)
        {
            incubating = true;
            incubationTime = maxIncubation - virusStrain.Get(4) * varianceIncubation;
        }
    }

	void Wander()
	{
        targetPosition = Random.insideUnitSphere * 80;
		targetPosition = new Vector3 (targetPosition.x, transform.position.y, targetPosition.z);
	}

	private void BecomeZombie()
	{
		isZombie = true;
		gameObject.tag = "Zombie";
		gameObject.layer = 10;
		myMat.material = red;

        zombieSpawnerReference.currentZombie++;
        zombieSpawnerReference.currentHuman--;

        UpdateStats();

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
                if (human.tag == "Player")
                {
                    smallestDist = (human.transform.position - transform.position).magnitude;
                    target = human.transform;
                }
                else
                {
                    NPC script = human.GetComponent<NPC>();
                    if (!script.incubating)
                    {
                        smallestDist = (human.transform.position - transform.position).magnitude;
                        target = human.transform;
                    }
                }
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
                if (Random.Range(0, 100) < infectivity) // probability of infecting the person they're biting
                {
                    script.Infect(virusStrain);
                }

                script.health -= damage;
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
        targetPosition = transform.position + (Vector3.Normalize(fleeDir) * 10);
	}
}
