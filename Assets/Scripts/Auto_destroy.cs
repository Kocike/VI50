using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnCollisionEnter(Collision C)
    {
        Debug.Log(C.collider.gameObject.tag);
        if (C.collider.gameObject.tag == "Floor")
        {
            Destroy(this.gameObject);
        }
    }
}
