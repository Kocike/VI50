using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void Update()
    {
        if (gameObject.transform.position.y < 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collision C)
    {
        Debug.Log(C.collider.gameObject.tag);
        if (C.collider.gameObject.tag == "Floor")
        {
            Destroy(this.gameObject);
        }
    }
}

    void OnTriggerEnter(Collider C)
    {
        Debug.Log(C.gameObject.tag);
        if (C.gameObject.tag == "Floor")
        {
            Destroy(this.gameObject);
        }