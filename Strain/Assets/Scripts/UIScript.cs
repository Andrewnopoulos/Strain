using UnityEngine;
using System.Collections;

public class UIScript : MonoBehaviour {

    public GameObject healthBar;

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

        //-500 + (healthBar.transform.localScale.x / 2)
	}
}
