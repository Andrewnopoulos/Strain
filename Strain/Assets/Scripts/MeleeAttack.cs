﻿using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour {

    public float damage = 10;

    //switchgun cooldown
    public float holsterRate = 0.1f;
    public float holsterCooldown = 0.0f;

    //how many bullets are shot per second
    public float attackRate = 0.5f;
    //current time between shots
    public float attackCooldown = 0.0f;

    public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        attackCooldown -= Time.deltaTime;
        holsterCooldown -= Time.deltaTime;

        if (Input.GetMouseButton(0)) //if left mouse button is held down
        {
            if (attackCooldown <= 0.0f && holsterCooldown <= 0.0f)
            {
                if (target)
                {
                    target.GetComponent<NPC>().health -= damage;
                }

                attackCooldown = attackRate;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Human" || other.tag == "Zombie")
            target = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        target = null;
    }

}
