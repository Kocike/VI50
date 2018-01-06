using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    //Detruit l'objet quand il touche ou passe a travers le sol
    private void Update()
    {
        if (gameObject.transform.position.y < 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider C)
    {
        //Debug.Log(C.gameObject.name);
        if (C.gameObject.tag == "Floor")
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision C)
    {
        Debug.Log(C.collider.gameObject.name);
        if (C.collider.gameObject.tag == "Floor")
        {
            Destroy(this.gameObject);
        }
    }
}
