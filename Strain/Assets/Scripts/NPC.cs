﻿using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    /// <summary>
    /// chromosome class that contains virus values and crossover/mutation methods
    /// currently unable to change genes after the chromosome is created (except by mutation)
    /// </summary>
    private class Chromosome
    {
        private static int length = 6;
        public static float mutationRate = 0.05f;
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
        /// [4] - sight<para />
        /// [5] - sound<para />
        /// [6] - smell
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

    public float minSpeed;
    public float varianceSpeed;
    public float minHealth;
    public float varianceHealth;
    public float minDamage;
    public float varianceDamage;
    public float minInfectivity;
    public float varianceInfectivity;

	// Use this for initialization
	void Start () {
        chromosomeLength = Chromosome.GetLength();
		zombieSpawnerReference = groundReference.GetComponent<ZombieSpawner>();

        virusStrain = new Chromosome(0.0f);

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

            if ((transform.position - target.position).magnitude < 100)
            {
                Flee();
            }
			if((transform.position - targetPosition).magnitude < 2)
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

	void Wander()
	{
        targetPosition = Random.insideUnitSphere * 80;
		targetPosition = new Vector3 (targetPosition.x, transform.position.y, targetPosition.z);
	}

	private void BecomeZombie(Chromosome inputVirus)
	{
		isZombie = true;
		gameObject.tag = "Zombie";
		gameObject.layer = 10;
		myMat.material = red;

        virusStrain = virusStrain.Crossover(inputVirus);

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
                if (Random.Range(0, 100) < infectivity) // probability of infecting the person they're biting
                {
                    NPC script = target.GetComponent<NPC>();
                    script.BecomeZombie(virusStrain);
                    zombieSpawnerReference.currentZombie++;
                    zombieSpawnerReference.currentHuman--;
                }
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
        targetPosition = transform.position + (Vector3.Normalize(fleeDir) * 100);
	}
}
