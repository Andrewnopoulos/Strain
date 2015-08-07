using UnityEngine;
using System.Collections;

public class Pistol : GunClass 
{

	// Use this for initialization
	void Start () {

        bulletDeviation = 1.0f;
        holsterRate = 1.5f;
        holsterCooldown = holsterRate;
		fireRate = 0.6f;
		shotCooldown = fireRate;
		damage = 10.0f;
		totalAmmo = 64;
		maxClipAmmo = 8;
		currentClipAmmo = maxClipAmmo;
		reloadTime = 2.0f;
		currentReloadTime = reloadTime;

	}
	
}
