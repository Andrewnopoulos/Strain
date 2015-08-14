using UnityEngine;
using System.Collections;

public class LootSpawn : MonoBehaviour {

    public GameObject PistolPickup;
    public GameObject RiflePickup;
    public GameObject ShotgunPickup;
    public GameObject PistolAmmo;
    public GameObject RifleAmmo;
    public GameObject ShotgunAmmo;

    public GameObject currentLoot;

    //out of 100
    public float lootSpawnChance = 50;

    //must add up to 100
    private float pistolPickupChance = 10;
    private float riflePickupChance = 10;
    private float shotgunPickupChance = 10;
    private float pistolAmmoChance = 35;
    private float rifleAmmoChance = 23;
    private float shotgunAmmoChance = 12;

    //how many seconds for another piece of loot to spawn
    private float lootSpawnRate = 20;
    private float lootSpawnCooldown = 0;

	// Use this for initialization
	void Start () {

        currentLoot = null;

	}
	
	// Update is called once per frame
	void Update () {

        lootSpawnCooldown -= Time.deltaTime;

        if (lootSpawnCooldown <= 0)
        { 
            SpawnLoot();
            lootSpawnCooldown = lootSpawnRate;
        }

	}

    void SpawnLoot()
    {

        if (currentLoot)
            Destroy(currentLoot);

        if (Random.Range(0, 100) < lootSpawnChance)
        {
            //spawn some loot
            float lootType = Random.Range(0, 100);

            if (lootType < pistolPickupChance)
            {
                //spawn pistol
                currentLoot = Instantiate(PistolPickup, transform.position, transform.rotation) as GameObject;
            }
            else if (lootType < riflePickupChance + pistolPickupChance)
            {
                //spawn rifle
                currentLoot = Instantiate(RiflePickup, transform.position, transform.rotation) as GameObject;
            }
            else if (lootType < shotgunPickupChance + riflePickupChance + pistolPickupChance)
            {
                //spawn shotgun
                currentLoot = Instantiate(ShotgunPickup, transform.position, transform.rotation) as GameObject;
            }
            else if (lootType < pistolAmmoChance + shotgunPickupChance + riflePickupChance + pistolPickupChance)
            {
                //spawn pistol ammo
                currentLoot = Instantiate(PistolAmmo, transform.position, transform.rotation) as GameObject;
            }
            else if (lootType < rifleAmmoChance + pistolAmmoChance + shotgunPickupChance + riflePickupChance + pistolPickupChance)
            {
                //spawn rifle ammo
                currentLoot = Instantiate(RifleAmmo, transform.position, transform.rotation) as GameObject;
            }
            else
            {
                //spawn shotgun ammo
                currentLoot = Instantiate(ShotgunAmmo, transform.position, transform.rotation) as GameObject;
            }

        }
    }
}
