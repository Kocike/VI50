using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToClientOrderPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider C)
    {
        //Debug.Log(C.gameObject.tag);
        if (C.gameObject.tag == "ClientOrderTargetPosition")
        {
            Debug.Log("On est sur la cible");
            this.gameObject.GetComponent<OVRGrabbable>().enabled = false;
            this.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            this.transform.position = new Vector3(transform.position.x, 1.357f, transform.position.z);
        }
    }
}
