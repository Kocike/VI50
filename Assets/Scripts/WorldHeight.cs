using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldHeight : MonoBehaviour {

    public float heightInWorld;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        heightInWorld = transform.position.y;
	}
}
