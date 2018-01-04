using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteraction : MonoBehaviour {

    private GameObject OvenPivot = null;
    public Animator OvenAnimator = null;

	// Use this for initialization
	void Start () {
        if (OvenPivot)
        {
            OvenAnimator = OvenPivot.GetComponent<Animator>();
            OvenAnimator.SetBool("OpenOven", false);
        }	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (OvenAnimator)
            {
                OvenAnimator.SetBool("OpenOven", !OvenAnimator.GetBool("OpenOven"));
                //Debug.Log(OvenAnimator.GetBool("OpenOven").ToString());
            }
        }
	}
}
