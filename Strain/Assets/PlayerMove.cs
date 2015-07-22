using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 velocity = new Vector2 (0, 0);

		if (Input.GetKey (KeyCode.A)) {
			velocity.x -= 1;
		}

		if (Input.GetKey (KeyCode.S)) {
			velocity.y -= 1;
		}
		if (Input.GetKey (KeyCode.D)) {
			velocity.x += 1;
		}

		if (Input.GetKey (KeyCode.W)) {
			velocity.y += 1;
		}

		transform.position = new Vector3 (velocity.x + transform.position.x, transform.position.y, velocity.y + transform.position.z);

	}
}
