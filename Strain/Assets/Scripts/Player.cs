using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float health;
    public GameObject gun;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CycleGun();
        }
	
        if (health <= 0)
        {
            Die();
        }

	}

    void CycleGun()
    {
        if (gun.GetComponent<Pistol>().enabled == true)
        {
            gun.GetComponent<Pistol>().enabled = false;
            gun.GetComponent<AssaultRifle>().enabled = true;

            gun.GetComponent<AssaultRifle>().holsterCooldown = gun.GetComponent<AssaultRifle>().holsterRate;
        }
        else
        {
            gun.GetComponent<Pistol>().enabled = true;
            gun.GetComponent<AssaultRifle>().enabled = false;

            gun.GetComponent<Pistol>().holsterCooldown = gun.GetComponent<AssaultRifle>().holsterRate;
        }
    }

    void Die()
    {

    }
}
