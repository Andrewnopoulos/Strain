using UnityEngine;
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

	public void ShakeScreen(float x, float z)
	{
		screenShake = true;
		xShake = x;
		zShake = z;
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
			                                                              12.0f + shakeOffset.y, 
			                                                              -7.5f + shakeOffset.z);
		}
		else
		{
			transform.position = player.transform.position + new Vector3 (0.0f, 12.0f, -7.5f);
		}
	}
}
