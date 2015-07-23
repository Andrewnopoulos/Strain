using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Movement code
		Vector2 velocity = new Vector2 (0, 0);

		if (Input.GetKey (KeyCode.A)) 
		{
			velocity.x -= 1;
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			velocity.y -= 1;
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			velocity.x += 1;
		}
		if (Input.GetKey (KeyCode.W)) 
		{
			velocity.y += 1;
		}

		transform.position = new Vector3 (velocity.x + transform.position.x, transform.position.y, velocity.y + transform.position.z);

		//Rotation code
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		float distance = 0.0f;
		if (Physics.Raycast (ray, out hit)) 
		{
			distance = hit.distance;
		}
		transform.LookAt (ray.GetPoint (distance - 1.28f), Vector3.up);

		//Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, Camera.main.transform.position.z);
	}
}
