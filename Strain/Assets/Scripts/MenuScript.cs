using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public string level;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Test()
    {
        Debug.Log("Button Pressed");
        Application.LoadLevel(level);
    }
}
