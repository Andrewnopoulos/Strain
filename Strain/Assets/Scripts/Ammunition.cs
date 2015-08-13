using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour {

    public int ammo;

	// Use this for initialization
	void Start () {

        ammo = (int)Random.Range(1, 20);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
