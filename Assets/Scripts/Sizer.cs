using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sizer : MonoBehaviour {
    public Vector3 size;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        size = GetComponent<MeshRenderer>().bounds.size;
	}
}
