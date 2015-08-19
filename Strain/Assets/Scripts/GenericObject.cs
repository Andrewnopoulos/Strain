using UnityEngine;
using System.Collections;

public class GenericObject : MonoBehaviour {

    public bool isTransparent = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (isTransparent)
        {
            if (transform.GetComponent<Renderer>().material.shader != Shader.Find("Transparent/Diffuse"))
            {
                for (int i = 0; i < transform.GetComponents<Renderer>().Length; ++i)
                {
                    Renderer rend = transform.GetComponents<Renderer>()[i];

                    rend.material.shader = Shader.Find("Transparent/Diffuse");
                    Color tempColor = rend.material.color;
                    tempColor.a = 0.4F;
                    rend.material.color = tempColor;
                }

            }
        }
        else
        {
            if (transform.GetComponent<Renderer>().material.shader == Shader.Find("Transparent/Diffuse"))
            {
                for (int i = 0; i < transform.GetComponents<Renderer>().Length; ++i)
                {
                    Renderer rend = transform.GetComponents<Renderer>()[i];

                    rend.material.shader = Shader.Find("Standard");
                }
            }
        }

        isTransparent = false;
	}
}
