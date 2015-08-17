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

    public float maxStamina = 100.0f;
    public float currentStamina = 100.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (currentStamina < maxStamina)
        {
            currentStamina += 15 * Time.deltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
        }

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
            SwitchToMelee();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToPistol();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchToRifle();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchToShotgun();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
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
            if (hasPistol)
            {
                gun.GetComponent<Pistol>().totalAmmo += gun.GetComponent<Pistol>().maxClipAmmo;
            }
            else
            {
                hasPistol = true;
                SwitchToPistol();
            }

            Destroy(other.gameObject);
        }
        else if (other.tag == "RiflePickup")
        {
            if (hasRifle)
            {
                gun.GetComponent<AssaultRifle>().totalAmmo += gun.GetComponent<AssaultRifle>().maxClipAmmo;
            }
            else
            {
                hasRifle = true;
                SwitchToRifle();
            }

            Destroy(other.gameObject);
        }
        else if (other.tag == "ShotgunPickup")
        {
            if (hasShotgun)
            {
                gun.GetComponent<Shotgun>().totalAmmo += gun.GetComponent<Shotgun>().maxClipAmmo;
            }
            else
            {
                hasShotgun = true;
                SwitchToShotgun();
            }

            Destroy(other.gameObject);
        }
        else if (other.tag == "FlamethrowerPickup")
        {
            hasFlamethrower = true;
            SwitchToFlamethrower();

            Destroy(other.gameObject);
        }
        else if (other.tag == "PistolBullets")
        {
            Ammunition script = other.GetComponent<Ammunition>();
            gun.GetComponent<Pistol>().totalAmmo += script.ammo;

            Destroy(other.gameObject);
        }
        else if (other.tag == "RifleBullets")
        {
            Ammunition script = other.GetComponent<Ammunition>();
            gun.GetComponent<AssaultRifle>().totalAmmo += script.ammo;

            Destroy(other.gameObject);
        }
         else if (other.tag == "ShotgunBullets")
        {
            Ammunition script = other.GetComponent<Ammunition>();
            gun.GetComponent<Shotgun>().totalAmmo += script.ammo;

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

    void SwitchToMelee()
    {
        if (gameObject.GetComponentInChildren<MeleeAttack>().enabled)
            return;

        //enable pistol
        gameObject.GetComponentInChildren<MeleeAttack>().enabled = true;

        //disable other guns
        gun.GetComponent<Pistol>().enabled = false;
        gun.GetComponent<AssaultRifle>().enabled = false;
        gun.GetComponent<Shotgun>().enabled = false;

        gun.GetComponent<MeshRenderer>().enabled = false;

        gameObject.GetComponentInChildren<MeleeAttack>().holsterCooldown = gameObject.GetComponentInChildren<MeleeAttack>().holsterRate;
    }

    void SwitchToPistol()
    {
        if (!hasPistol || gun.GetComponent<Pistol>().enabled)
            return;

        //enable pistol
        gun.GetComponent<Pistol>().enabled = true;

        //disable other guns
        gameObject.GetComponentInChildren<MeleeAttack>().enabled = false;
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

        gameObject.GetComponentInChildren<MeleeAttack>().enabled = false;
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

        gameObject.GetComponentInChildren<MeleeAttack>().enabled = false;
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

	public void TakeDamage(float damage)
	{
		gameObject.GetComponentInChildren<CameraMove>().screenShake = true;
		health -= damage;
	}

    void Die()
    {

        Application.LoadLevel("MainMenu");

    }
}
