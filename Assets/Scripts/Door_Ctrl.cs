using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Ctrl : MonoBehaviour {
	private bool touchL=false;
	private bool touchR = false;
	
	private bool opening = false;
	private bool closing = false;
    public bool STOP = false;

    [HideInInspector]
    public bool closed = true;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!STOP && ((touchL && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5f)|| (touchR && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5f)))
		{
			//Debug.Log ("Hello");
			closing = !closed;
			opening = closed;
		}
		//Debug.Log (gameObject.transform.localEulerAngles.x);
		if (opening) {
			if (250<gameObject.transform.localEulerAngles.x && gameObject.transform.localEulerAngles.x <=357) {
				Opening (1);
			} else {
				closed= false;
				opening = false;
			}
		}
		if (closing) {
			if (gameObject.transform.localEulerAngles.x >270 || gameObject.transform.localEulerAngles.x <50) {
				Opening (-1);
			} else {
                closed = true;
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
		float speed = b > 0 ? 359-gameObject.transform.localEulerAngles.x : gameObject.transform.localEulerAngles.x>270 ? gameObject.transform.localEulerAngles.x-269 : 90 + gameObject.transform.localEulerAngles.x;
        speed = Mathf.Max(speed, 1);
        this.gameObject.transform.Rotate(Vector3.right *b* Time.deltaTime*speed) ;
	}

}