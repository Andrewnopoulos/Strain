using UnityEngine;
using System.Collections;

public class TransparencyScript : MonoBehaviour {

    public GameObject player;

	void Start () {

	}
	
	void Update () {

        int layermask = (1 << 11);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, 
            Vector3.Normalize(player.transform.position - transform.position),
            Vector3.Magnitude(player.transform.position - transform.position),
            layermask);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            hit.collider.gameObject.GetComponent<GenericObject>().isTransparent = true;
        }

	}
}