﻿using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 thing = player.transform.position;
		transform.position = player.transform.position + new Vector3 (0.0f, 1200.0f, -800.0f);
	}
}