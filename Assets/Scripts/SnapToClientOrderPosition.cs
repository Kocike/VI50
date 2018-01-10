using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToClientOrderPosition : MonoBehaviour {
    private float posY;

	// Use this for initialization
	void Start () {
        if (gameObject.name.StartsWith("Glass"))
        {
            posY = 1.357f;
        }
        else
        {
            posY = 1.415f;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider C)
    {
        //Debug.Log(C.gameObject.tag);
        if (C.gameObject.tag == "ClientOrderTargetPosition")
        {
            //Debug.Log("On est sur la cible");
            this.gameObject.GetComponent<OVRGrabbable>().ForceRelease();
            this.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            this.transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        }
    }
}
