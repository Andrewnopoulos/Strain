using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour {
	
	private Vector3 target;
	private NavMeshAgent navComponent;

	// Use this for initialization
	void Start () {
	
		navComponent = this.transform.GetComponent<NavMeshAgent> ();
		Wander ();
	}
	
	// Update is called once per frame
	void Update () {
	

		navComponent.SetDestination(target);

		if((transform.position - target).magnitude < 0.5)
		{ 
			Wander(); 
		}
	}

	void Wander()
	{
		target = Random.insideUnitSphere * 50;
		target.y = 1;
	}
}
