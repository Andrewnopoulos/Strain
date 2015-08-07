using UnityEngine;
using System.Collections;

public class DayCycle : MonoBehaviour {

    public Light sun;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        sun.transform.Rotate(new Vector3(0, -1, 0), Time.deltaTime);
        sun.intensity = Vector3.Dot(sun.transform.forward, new Vector3(0, -1, 0));

        //sun.intensity = Mathf.Clamp(2 * Mathf.Sin(currentDay / 100), 0.2f, 1.2f);

        //sun.transform.Rotate(new Vector3(0, 1, 0), -Mathf.PI / 4 *  Time.deltaTime);

	}
}
