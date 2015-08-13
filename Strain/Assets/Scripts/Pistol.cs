using UnityEngine;
using System.Collections;

public class Pistol : GunClass 
{

	// Use this for initialization
	void Start () {

        shotCount = 1;
        bulletDeviation = 0.5f;
        holsterRate = 0.5f;
        holsterCooldown = holsterRate;
		fireRate = 0.6f;
		shotCooldown = fireRate;
		damage = 35.0f;
		totalAmmo = 0;
		maxClipAmmo = 8;
		currentClipAmmo = maxClipAmmo;
		reloadTime = 2.0f;
		currentReloadTime = reloadTime;

	}
	
}
