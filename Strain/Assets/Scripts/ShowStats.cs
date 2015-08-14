using UnityEngine;
using System.Collections;

public class ShowStats : MonoBehaviour {

    public bool showStats;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))
        {
            showStats = !showStats;
        }

	}
}
