using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Movement code
		Vector3 velocity = new Vector3 (0, 0, 0);

		if (Input.GetKey (KeyCode.A)) 
		{
			velocity.x -= 0.2f;
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			velocity.z -= 0.2f;
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			velocity.x += 0.2f;
		}
		if (Input.GetKey (KeyCode.W)) 
		{
			velocity.z += 0.2f;
		}


		transform.Translate (velocity);

		//Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, Camera.main.transform.position.z);
	}

}
