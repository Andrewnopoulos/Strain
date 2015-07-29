using UnityEngine;
using System.Collections;

public class GunClass : MonoBehaviour {

	public GameObject bullet;

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
	void Update () {
	
	}
}
