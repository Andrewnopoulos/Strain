using UnityEngine;
using System.Collections;

public class BillboardScript : MonoBehaviour {

    private bool showStats;

    public Camera camera;

    public NPC npc;

	// Use this for initialization
	void Start () {

        camera = Camera.allCameras[0];
        npc = transform.parent.GetComponent<NPC>();
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = transform.parent.position + new Vector3(1.4f, 1.0f, 0);

        transform.LookAt(camera.transform, new Vector3(1, 0, 0));
        transform.Rotate(new Vector3(0, 1, 0), 180);
        transform.Rotate(new Vector3(0, 0, 1), 180);

        if (camera.transform.GetComponent<ShowStats>().showStats != showStats)
        {
            showStats = !showStats;
            ShowStats();
        }

        if (transform.GetComponent<Renderer>().enabled)
        {
            //adjust the size of the bars

            //speed
            transform.GetChild(0).transform.localScale = new Vector3(transform.GetChild(0).transform.localScale.x, 
                0.9f * (npc.GetVirus().Get(0)), 
                transform.GetChild(0).transform.localScale.z);

            transform.GetChild(0).transform.localPosition = new Vector3(transform.GetChild(0).transform.localPosition.x,
                0.45f - (transform.GetChild(0).transform.localScale.y / 2),
                transform.GetChild(0).transform.localPosition.z);

            //health
            transform.GetChild(1).transform.localScale = new Vector3(transform.GetChild(1).transform.localScale.x,
                0.9f * (npc.GetVirus().Get(1)),
                transform.GetChild(1).transform.localScale.z);

            transform.GetChild(1).transform.localPosition = new Vector3(transform.GetChild(1).transform.localPosition.x,
                0.45f - (transform.GetChild(1).transform.localScale.y / 2),
                transform.GetChild(1).transform.localPosition.z);

            //damage
            transform.GetChild(2).transform.localScale = new Vector3(transform.GetChild(2).transform.localScale.x,
                0.9f * (npc.GetVirus().Get(2)),
                transform.GetChild(2).transform.localScale.z);

            transform.GetChild(2).transform.localPosition = new Vector3(transform.GetChild(2).transform.localPosition.x,
                0.45f - (transform.GetChild(2).transform.localScale.y / 2),
                transform.GetChild(2).transform.localPosition.z);

            //sight
            transform.GetChild(3).transform.localScale = new Vector3(transform.GetChild(3).transform.localScale.x,
                0.9f * (npc.GetVirus().Get(5)),
                transform.GetChild(3).transform.localScale.z);

            transform.GetChild(3).transform.localPosition = new Vector3(transform.GetChild(3).transform.localPosition.x,
                0.45f - (transform.GetChild(3).transform.localScale.y / 2),
                transform.GetChild(3).transform.localPosition.z);

            //sound
            transform.GetChild(4).transform.localScale = new Vector3(transform.GetChild(4).transform.localScale.x,
                0.9f * (npc.GetVirus().Get(6)),
                transform.GetChild(4).transform.localScale.z);

            transform.GetChild(4).transform.localPosition = new Vector3(transform.GetChild(4).transform.localPosition.x,
                0.45f - (transform.GetChild(4).transform.localScale.y / 2),
                transform.GetChild(4).transform.localPosition.z);

            //smell
            transform.GetChild(5).transform.localScale = new Vector3(transform.GetChild(5).transform.localScale.x,
                           0.9f * (npc.GetVirus().Get(7)),
                           transform.GetChild(5).transform.localScale.z);

            transform.GetChild(5).transform.localPosition = new Vector3(transform.GetChild(5).transform.localPosition.x,
                0.45f - (transform.GetChild(5).transform.localScale.y / 2),
                transform.GetChild(5).transform.localPosition.z);

        }

	}

    public void ShowStats()
    {
        transform.GetComponent<Renderer>().enabled = !transform.GetComponent<Renderer>().enabled;

        //disable the children
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).transform.GetComponent<Renderer>().enabled = !transform.GetChild(i).transform.GetComponent<Renderer>().enabled;
        }
    }
}
