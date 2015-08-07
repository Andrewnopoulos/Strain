using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float damage;

	private LineRenderer lineRenderer;

	private Vector3 startPos;
	private Vector3 endPos;

	public float lifeTime;

	private GameObject collidedObject;

	private int layermask = 1;

	Ray ray;

	// Use this for initialization
	void Start () {

		float distance = 5000;

		layermask = ((1 << 10) | (1 << 11) | (1 << 12));

		startPos = transform.position;

		lineRenderer = transform.GetComponent<LineRenderer> ();

		ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 10000, layermask))
		{
			collidedObject = hit.collider.gameObject;

			distance = hit.distance;

			if (collidedObject.layer == 10 || collidedObject.layer == 12)
			{
				NPC script = collidedObject.GetComponent<NPC>();
				script.health -= damage;
			}
		}

		endPos = startPos + (transform.forward * distance);

		lineRenderer.SetPosition (0, startPos);
		lineRenderer.SetPosition (1, endPos);

	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawRay (ray);
	}

	// Update is called once per frame
	void Update () {

		lifeTime -= Time.deltaTime;

		if (lifeTime <= 0.0f) {
			Destroy(gameObject);
		}

	}
}
