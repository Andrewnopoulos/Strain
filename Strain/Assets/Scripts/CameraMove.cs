﻿using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public GameObject player;

	public bool screenShake = false;
	private float zShake = 0.2f;
	private float xShake = 0.2f;

	private float shakeholdTime = 0.02f;
	private float shakeCooldown = 0;
	private Vector3 shakeOffset;

	private float shakeLength = 0.1f;
	private float currentShakeLength = 0;

	void Start () {
	
	}

	void Update () {

		if (screenShake)
		{
			currentShakeLength -= Time.deltaTime;

			if (currentShakeLength < 0)
			{
				currentShakeLength = shakeLength;
				screenShake = false;
			}

			shakeCooldown -= Time.deltaTime;

			if (shakeCooldown < 0)
			{
				shakeOffset = new Vector3(Random.Range(-xShake, xShake), 0, Random.Range(-zShake, zShake));
				shakeCooldown = shakeholdTime;
			}

			transform.position = player.transform.position + new Vector3 (0.0f + shakeOffset.x, 
			                                                              16.0f + shakeOffset.y, 
			                                                              -10.0f + shakeOffset.z);
		}
		else
		{
			transform.position = player.transform.position + new Vector3 (0.0f, 16.0f, -10.0f);
		}
	}
}
