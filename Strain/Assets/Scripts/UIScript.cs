using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public GameObject healthBar;
    public Text weapon;
    public Text ammo;

    public GameObject player;

    private Player playerscript;

	// Use this for initialization
	void Start () {

        playerscript = player.GetComponent<Player>();

	}
	
	// Update is called once per frame
	void Update () {

        healthBar.transform.localScale = new Vector3(400 * playerscript.health / 100, 1, 1);
        healthBar.transform.localPosition = new Vector3(-500 + (healthBar.transform.localScale.x / 2), -200, 0);

        if (player.GetComponentInChildren<Pistol>().enabled)
        {
            weapon.text = "Pistol";
            ammo.text = player.GetComponentInChildren<Pistol>().totalAmmo.ToString();
        }
        else if (player.GetComponentInChildren<AssaultRifle>().enabled)
        {
            weapon.text = "Assualt Rifle";
            ammo.text = player.GetComponentInChildren<AssaultRifle>().totalAmmo.ToString();
        }
        else if (player.GetComponentInChildren<Shotgun>().enabled)
        {
            weapon.text = "Shotgun";
            ammo.text = player.GetComponentInChildren<Shotgun>().totalAmmo.ToString();
        }
        else
        {
            weapon.text = "No Weapon";
            ammo.text = "";
        }
	}
}
