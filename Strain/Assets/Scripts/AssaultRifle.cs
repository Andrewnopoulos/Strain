using UnityEngine;
using System.Collections;

public class AssaultRifle : GunClass 
{
	
	// Use this for initialization
	void Start () {

        bulletDeviation = 5.0f;
        holsterRate = 1.5f;
        holsterCooldown = holsterRate;
		fireRate = 0.2f;
		shotCooldown = fireRate;
		damage = 8.0f;
		totalAmmo = 120;
		maxClipAmmo = 30;
		currentClipAmmo = maxClipAmmo;
		reloadTime = 3.0f;
		currentReloadTime = reloadTime;
		
	}

}