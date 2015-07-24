using UnityEngine;
using System.Collections;

public class PlayerRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		//Rotation code
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		float distance = 0.0f;
		if (Physics.Raycast (ray, out hit)) 
		{
			distance = hit.distance;
		}
		transform.LookAt (ray.GetPoint (distance - 1.28f), Vector3.up);

	}
}
