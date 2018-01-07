using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven_Ctrl : MonoBehaviour {
	private bool touchL=false;
	private bool touchR = false;
	//private float timer;
	//private bool baking=false;
	private bool open = false;
	private bool opening = false;
	private bool closing = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if ((touchL && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5f)|| (touchR && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5f))
		{
			//Debug.Log ("Hello");
			closing = open;
			opening = !open;
		}
		//Debug.Log (gameObject.transform.localEulerAngles.x);
		if (opening) {
			if (250<gameObject.transform.localEulerAngles.x && gameObject.transform.localEulerAngles.x <=357) {
				Opening (1);
			} else {
				open = true;
				opening = false;
			}
		}
		if (closing) {
			if (gameObject.transform.localEulerAngles.x >270 || gameObject.transform.localEulerAngles.x <50) {
				Opening (-1);
			} else {
				open = false;
				closing = false;
			}
		}
	}

	private void OnTriggerEnter(Collider C)
	{
		if (C.gameObject.name == "RightHandAnchor") {
			touchR = true;
		}
		if(   C.gameObject.name == "LeftHandAnchor")
		{
			touchL = true;
		}
	}
	private void OnTriggerExit(Collider C)
	{
		if (C.gameObject.name == "RightHandAnchor")
		{
			touchR = false;
		}
		if(C.gameObject.name == "LeftHandAnchor")
		{
			touchL = false;
		}
	}
	void Opening(float b){
		float speed = b > 0 ? 359-gameObject.transform.localEulerAngles.x : gameObject.transform.localEulerAngles.x-269;
		this.gameObject.transform.Rotate(Vector3.right *b* Time.deltaTime*speed) ;
	}

}