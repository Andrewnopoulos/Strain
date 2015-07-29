using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

	public GameObject target;
	
	private Transform targetTransform;
	private NavMeshAgent navComponent;

	public float speed;

	// Use this for initialization
	void Start () {

		targetTransform = target.transform;
		
		navComponent = this.transform.GetComponent<NavMeshAgent> ();
		speed = navComponent.speed;
	}
	
	// Update is called once per frame
	void Update () {

		if (targetTransform) 
		{
			navComponent.SetDestination(targetTransform.position);
		}
	}
}
