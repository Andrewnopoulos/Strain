using UnityEngine;
using System.Collections;

public class GunClass : MonoBehaviour {

	public GameObject bullet;

    //bullet deviation
    public float bulletDeviation;

    //switchgun cooldown
    public float holsterRate;
    public float holsterCooldown;

	//how many bullets are shot per second
	public float fireRate;
	//current time between shots
	public float shotCooldown;

	//how much damage the gun deals
	public float damage;

	//the total ammount of ammo the player has for this gun type
	public int totalAmmo;
	//how much ammo is left in the current magazine
	public int currentClipAmmo;
	//the max ammount of ammo that can fit into a magazine
	public int maxClipAmmo;

	//how long in seconds to reload
	public float reloadTime;
	//how long the gun has been reloading for currently
	public float currentReloadTime;
	//if gun is currently reloading
	public bool reload = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    shotCooldown -= Time.deltaTime;
        holsterCooldown -= Time.deltaTime;

		if (currentClipAmmo == 0) {
			reload = true;
		}

		if (reload) {

			currentReloadTime -= Time.deltaTime;

			if (currentReloadTime <= 0.0f)
			{
				reload = false;
				currentReloadTime = reloadTime;
				if (maxClipAmmo < totalAmmo)
				{
					totalAmmo -= maxClipAmmo;
					currentClipAmmo = maxClipAmmo;
				}
				else
				{
					currentClipAmmo = totalAmmo;
					totalAmmo = 0;
				}
			}
		}

		//if left mouse button is held down
		if (Input.GetMouseButton (0)) 
		{
			if (currentClipAmmo > 0 && shotCooldown <= 0.0f && holsterCooldown <= 0.0f)
			{

				//instantiate bullet prefab in direction player is facing
				GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;

                //make a slight offset to rotation
                float randX = Random.Range(-bulletDeviation, bulletDeviation);
                newBullet.transform.Rotate(new Vector3(0, 1, 0), randX);

                Bullet script = newBullet.GetComponent<Bullet>();

                script.damage = damage;

				shotCooldown = fireRate;
				currentClipAmmo -= 1;
			}
		}

	}
}
