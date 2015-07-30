using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private LineRenderer lineRenderer;

	private Vector3 startPos;

	public float lifeTime;

	public Vector3 pos;
	public Vector3 dir;

	public GameObject collided;
	public bool collidedbool = false;

	private int layermask = 1;

	Ray ray;

	// Use this for initialization
	void Start () {

		layermask = ((1 << 10) | (1 << 11));

		startPos = transform.position;

		lineRenderer = transform.GetComponent<LineRenderer> ();

		pos = transform.position;
		dir = transform.forward;

		ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100000, layermask))
		{
			//collided = hit.collider.gameObject;
			collidedbool = true;
		}

	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawRay (ray);
	}

	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.forward * 5);

		lineRenderer.SetPosition (0, startPos);
		lineRenderer.SetPosition (1, transform.position);

		startPos += Vector3.Normalize ((transform.position - startPos) * 20);

		lifeTime -= Time.deltaTime;

		if (lifeTime <= 0.0f) {
			Destroy(gameObject);
		}

	}
}
