using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Ctrl : MonoBehaviour {
	private bool touchL=false;
	private bool touchR = false;
    private float speed = 0.0f;
	private bool opening = false;
	private bool closing = false;
    [HideInInspector]
    public bool STOP = false;
    public bool closed = true;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!STOP &&  ((touchL && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5f)|| (touchR && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5f)))
		{
			//Debug.Log ("Hello");
			closing = !closed;
			opening = closed;
            STOP = true;
		}
		//Debug.Log (gameObject.transform.localEulerAngles.x);
		if (opening) {
			if (250<gameObject.transform.localEulerAngles.x && gameObject.transform.localEulerAngles.x <=357) {
				Opening ();
			} else {
				closed= false;
				opening = false;
                STOP = false;
			}
		}
		if (closing) {
			if (gameObject.transform.localEulerAngles.x >271 || gameObject.transform.localEulerAngles.x <50) {
				Opening ();
			} else {
                closed = true;
				closing = false;
                STOP = false;
			}
		}
        // Debug.Log("Opening : " + opening + " Closing : " + closing + " Closed : " + closed + " Angle : " + gameObject.transform.localEulerAngles.x);
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
	void Opening(){
        if (opening)
        {
            speed = 359 - gameObject.transform.localEulerAngles.x;
        }
        else
        {
            speed=270- gameObject.transform.localEulerAngles.x;
        }
        speed = Mathf.Max(speed, 70) * speed / Mathf.Abs(speed);
        this.gameObject.transform.Rotate(Vector3.right * Time.deltaTime*speed) ;
	}

}