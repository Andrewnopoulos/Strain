using UnityEngine;
using System.Collections;

public class CrossHair : MonoBehaviour {

	public GameObject mouse;
	public Camera camera;

	// Use this for initialization
	void Start () {
	
		Cursor.visible = false;

	}
	
	// Update is called once per frame
	void Update () {

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		transform.position = ray.GetPoint(10);

		transform.LookAt(camera.transform, new Vector3(1, 0, 0));
	}
}
