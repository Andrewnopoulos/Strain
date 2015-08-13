using UnityEngine;
using System.Collections;

public class Shotgun : GunClass {

    void Start()
    {

        shotCount = 8;
        bulletDeviation = 8.0f;
        holsterRate = 3.0f;
        holsterCooldown = holsterRate;
        fireRate = 1.2f;
        shotCooldown = fireRate;
        damage = 18.0f;
        maxClipAmmo = 8;
        currentClipAmmo = maxClipAmmo;
        reloadTime = 3.0f;
        currentReloadTime = reloadTime;

    }

}
