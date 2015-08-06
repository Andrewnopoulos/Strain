using UnityEngine;
using System.Collections;

public class Pistol : GunClass 
{

	// Use this for initialization
	void Start () {
	
		fireRate = 1.0f;
		shotCooldown = fireRate;
		damage = 10.0f;
		totalAmmo = 100;
		maxClipAmmo = 8;
		currentClipAmmo = maxClipAmmo;
		reloadTime = 3.0f;
		currentReloadTime = reloadTime;

	}
	
	// Update is called once per frame
	void Update () {
	
		shotCooldown -= Time.deltaTime;

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
			if (currentClipAmmo > 0 && shotCooldown <= 0.0f)
			{
				//instantiate bullet prefab in direction player is facing
				Instantiate(bullet, transform.position, transform.rotation);

				shotCooldown = fireRate;
				currentClipAmmo -= 1;
			}
		}

	}
}
