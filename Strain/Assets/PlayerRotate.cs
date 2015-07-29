using UnityEngine;
using System.Collections;

public class PlayerRotate : MonoBehaviour {

	// Use this for initialization
	//LayerMask layer;

	int layermask = 1;

	void Start () {
		layermask = 1 << 8;
	}
	
	// Update is called once per frame
	void Update () {
		
		//Rotation code
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		float distance = 0.0f;
		if (Physics.Raycast (ray, out hit, 1000, layermask)) 
		{
			distance = hit.distance;
		}
		transform.LookAt (ray.GetPoint (distance), Vector3.up);

	}
}
