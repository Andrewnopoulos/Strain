using UnityEngine;
using System.Collections;

public class ParticleHandler : MonoBehaviour {

	public float lifeTime;
	public bool kill;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (kill)
		{
			lifeTime -= Time.deltaTime;

			if (gameObject.GetComponent<ParticleSystem>().loop)
			{
				if (lifeTime - gameObject.GetComponent<ParticleSystem>().startLifetime < 0.01f)
				{
					gameObject.GetComponent<ParticleSystem>().loop = false;
				}
			}

			if (lifeTime < 0)
				Destroy(gameObject);
		}
	}
}
