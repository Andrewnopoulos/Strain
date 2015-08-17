using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public CharacterController characterController;

	public Camera mainCamera;

    private float currentDodgeRoll = 0.0f;
    private float dogdeRollTime = 0.2f;
    private Vector3 dodgeRollDir;

    public float dodgeRollCost = 50.0f;

	// Use this for initialization
	void Start () {

        Physics.IgnoreLayerCollision(9, 8, true);

        characterController = gameObject.transform.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        //Movement code
        currentDodgeRoll -= Time.deltaTime;
        Vector3 velocity = new Vector3(0, 0, 0);

        if (currentDodgeRoll > 0)
        {
            velocity = dodgeRollDir;
        }
        else
        {
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
                velocity = Vector3.Normalize(velocity) * 8.0f;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (transform.GetComponentInParent<Player>().currentStamina > dodgeRollCost)
                    {
                        currentDodgeRoll = dogdeRollTime;
                        dodgeRollDir = velocity * 3.0f;
                        velocity = dodgeRollDir;
                        transform.GetComponentInParent<Player>().currentStamina -= dodgeRollCost;

						mainCamera.GetComponent<CameraMove>().screenShake = true;
                    }
                }
            }
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
