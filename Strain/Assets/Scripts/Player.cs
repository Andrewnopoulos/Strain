using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float health;
    public GameObject gun;
    public Light flashlight;

    public bool hasPistol = false;
    public bool hasRifle = false;
    public bool hasShotgun = false;
    public bool hasFlamethrower = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CycleGun();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashLight();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToPistol();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToRifle();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchToShotgun();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchToFlamethrower();
        }

        if (health <= 0)
        {
            Die();
        }

	}

    void ToggleFlashLight()
    {
        flashlight.enabled = !flashlight.enabled;
    }

    void CycleGun()
    {
        if (gun.GetComponent<Pistol>().enabled && hasPistol)
        {
            gun.GetComponent<AssaultRifle>().enabled = true;

            gun.GetComponent<Pistol>().enabled = false;

            gun.GetComponent<AssaultRifle>().holsterCooldown = gun.GetComponent<AssaultRifle>().holsterRate;
        }
        else if (gun.GetComponent<AssaultRifle>().enabled && hasRifle)
        {
            gun.GetComponent<Pistol>().enabled = true;

            gun.GetComponent<AssaultRifle>().enabled = false;

            gun.GetComponent<Pistol>().holsterCooldown = gun.GetComponent<AssaultRifle>().holsterRate;
        }
        else if (hasShotgun)
        {

        }
        else if (hasFlamethrower)
        {

        }
    }

    void SwitchToPistol()
    {
        if (!hasPistol)
            return;

        //enable pistol
        gun.GetComponent<Pistol>().enabled = true;

        //disable other guns
        gun.GetComponent<AssaultRifle>().enabled = false;

        gun.GetComponent<Pistol>().holsterCooldown = gun.GetComponent<Pistol>().holsterRate;
    }
    void SwitchToRifle()
    {
        if (!hasRifle)
            return;

        gun.GetComponent<AssaultRifle>().enabled = true;

        gun.GetComponent<Pistol>().enabled = false;

        gun.GetComponent<AssaultRifle>().holsterCooldown = gun.GetComponent<AssaultRifle>().holsterRate;
    }

    void SwitchToShotgun()
    {
        if (!hasShotgun)
            return;
    }

    void SwitchToFlamethrower()
    {
        if (!hasFlamethrower)
            return;
    }

    void Die()
    {

    }
}
