using UnityEngine;
using System.Collections;

public class DayCycle : MonoBehaviour {

    public Light sun;

    private float maxDay = 180;
    private float currentDay = 0;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        currentDay += Time.deltaTime;

        if (currentDay >= maxDay)
            currentDay = 0;

        sun.intensity = Mathf.Clamp(Mathf.Sin(currentDay / 18), 0.2f, 1.0f);

       // sun.transform.Rotate(new Vector3(0, 1, 0), currentDay);

	}
}
