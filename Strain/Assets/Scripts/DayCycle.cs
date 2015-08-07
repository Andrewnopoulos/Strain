using UnityEngine;
using System.Collections;

public class DayCycle : MonoBehaviour {

    public Light sun;

    private float maxDay = Mathf.PI * 100;
    public float currentDay = Mathf.PI * 100 / 2;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        currentDay += Time.deltaTime;

        if (currentDay >= maxDay)
            currentDay = 0;

        sun.intensity = Mathf.Clamp(2 * Mathf.Sin(currentDay / 100), 0.2f, 1.2f);

        sun.transform.Rotate(new Vector3(0, 1, 0), -Mathf.PI / 4 *  Time.deltaTime);

	}
}
