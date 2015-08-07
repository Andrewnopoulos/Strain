using UnityEngine;
using System.Collections;

public class AssaultRifle : GunClass 
{
	
	// Use this for initialization
	void Start () {

        shotCount = 1;
        bulletDeviation = 2.5f;
        holsterRate = 1.5f;
        holsterCooldown = holsterRate;
		fireRate = 0.2f;
		shotCooldown = fireRate;
		damage = 20.0f;
		totalAmmo = 120;
		maxClipAmmo = 30;
		currentClipAmmo = maxClipAmmo;
		reloadTime = 3.0f;
		currentReloadTime = reloadTime;
		
	}

}