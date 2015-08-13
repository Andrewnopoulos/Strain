using UnityEngine;
using System.Collections;

public class LootEffects : MonoBehaviour {

    private float lifeTime;

	// Use this for initialization
	void Start () {

        lifeTime = Random.Range(0, 10);
        gameObject.transform.Rotate(new Vector3(0, 1, 0), Random.Range(1, 10));
	}
	
	// Update is called once per frame
	void Update () {

        lifeTime += Time.deltaTime;

        gameObject.transform.Rotate(new Vector3(0, 1, 0), 20.0f * Time.deltaTime);

        gameObject.transform.position = new Vector3(transform.position.x, 1 + 0.2f * Mathf.Sin(lifeTime), transform.position.z);

	}
}
