using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public CharacterController characterController;

	// Use this for initialization
	void Start () {

        Physics.IgnoreLayerCollision(9, 8, true);

        characterController = gameObject.transform.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

		//Movement code
		Vector3 velocity = new Vector3 (0, 0, 0);

        if (characterController.isGrounded)
        {
            if (Input.GetKey(KeyCode.A))
            {
                velocity.x -= 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                velocity.z -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                velocity.x += 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                velocity.z += 1;
            }
            velocity = Vector3.Normalize(velocity) * 10.0f;
        }

        // // Pause testing
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Time.timeScale = 0;
        //}

		//transform.Translate (velocity);
        velocity.y -= 500 * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

	}

}
