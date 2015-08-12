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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PistolPickup")
        {
            hasPistol = true;
            SwitchToPistol();

            Destroy(other.gameObject);
        }
        else if (other.tag == "RiflePickup")
        {
            hasRifle = true;
            SwitchToRifle();

            Destroy(other.gameObject);
        }
        else if (other.tag == "ShotgunPickup")
        {
            hasShotgun = true;
            SwitchToShotgun();

            Destroy(other.gameObject);
        }
        else if (other.tag == "FlamethrowerPickup")
        {
            hasFlamethrower = true;
            SwitchToFlamethrower();

            Destroy(other.gameObject);
        }
    }

    void ToggleFlashLight()
    {
        flashlight.enabled = !flashlight.enabled;
    }

    void CycleGun()
    {
        if (gun.GetComponent<Pistol>().enabled && hasRifle)
        {
            gun.GetComponent<AssaultRifle>().enabled = true;

            gun.GetComponent<Pistol>().enabled = false;

            gun.GetComponent<AssaultRifle>().holsterCooldown = gun.GetComponent<AssaultRifle>().holsterRate;
        }
        else if (gun.GetComponent<AssaultRifle>().enabled && hasShotgun)
        {
            gun.GetComponent<Shotgun>().enabled = true;

            gun.GetComponent<AssaultRifle>().enabled = false;

            gun.GetComponent<Shotgun>().holsterCooldown = gun.GetComponent<Shotgun>().holsterRate;
        }
        else if (gun.GetComponent<Shotgun>().enabled && hasPistol)
        {
            gun.GetComponent<Pistol>().enabled = true;

            gun.GetComponent<Shotgun>().enabled = false;

            gun.GetComponent<Pistol>().holsterCooldown = gun.GetComponent<Pistol>().holsterRate;
        }
        else if (hasFlamethrower)
        {

        }
    }

    void SwitchToPistol()
    {
        if (!hasPistol || gun.GetComponent<Pistol>().enabled)
            return;

        //enable pistol
        gun.GetComponent<Pistol>().enabled = true;

        //disable other guns
        gun.GetComponent<AssaultRifle>().enabled = false;
        gun.GetComponent<Shotgun>().enabled = false;

        gun.GetComponent<MeshRenderer>().enabled = true;

        gun.GetComponent<Pistol>().holsterCooldown = gun.GetComponent<Pistol>().holsterRate;
    }
    void SwitchToRifle()
    {
        if (!hasRifle || gun.GetComponent<AssaultRifle>().enabled)
            return;

        gun.GetComponent<AssaultRifle>().enabled = true;

        gun.GetComponent<Pistol>().enabled = false;
        gun.GetComponent<Shotgun>().enabled = false;

        gun.GetComponent<MeshRenderer>().enabled = true;

        gun.GetComponent<AssaultRifle>().holsterCooldown = gun.GetComponent<AssaultRifle>().holsterRate;
    }

    void SwitchToShotgun()
    {
        if (!hasShotgun || gun.GetComponent<Shotgun>().enabled)
            return;

        gun.GetComponent<Shotgun>().enabled = true;

        gun.GetComponent<Pistol>().enabled = false;
        gun.GetComponent<AssaultRifle>().enabled = false;

        gun.GetComponent<MeshRenderer>().enabled = true;

        gun.GetComponent<Shotgun>().holsterCooldown = gun.GetComponent<Shotgun>().holsterRate;
    }

    void SwitchToFlamethrower()
    {
        if (!hasFlamethrower)
            return;
    }

    void Die()
    {

        Application.LoadLevel("MainMenu");

    }
}
