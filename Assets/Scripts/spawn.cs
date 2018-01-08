using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {
	public GameObject dropPrefab;
	private bool spawning =false;
	private float timer;
    public GameObject halo_object;
	//private GameObject Pivot;
	//public Animator Pivot_anim;
	public GameObject spawner;
    private bool touchL=false;
    private bool touchR = false;

    private bool desc = false;
    private bool asc = false;


    private bool draft=false;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		//Pivot_anim.SetBool ("Down", false);
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

    // Update is called once per frame
    void Update()
    {
         Component halo = halo_object.GetComponent("Halo");
         halo.GetType().GetProperty("enabled").SetValue(halo, (touchR || touchL), null);

        if (Input.GetKeyDown(KeyCode.C))
        {
            asc = draft;
            desc = !draft;
            touchR = !touchR;
        }
        if ((touchL && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5f) || (touchR && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5f))
        {
            asc= draft;
            desc = !draft;
        }
        //Debug.Log (gameObject.transform.localEulerAngles.x);
        if (desc)
        {
            if (gameObject.transform.localEulerAngles.x>250 || gameObject.transform.localEulerAngles.x <79)
            {
                Down(1);
            }
            else
            {
                draft = true;
                desc = false;
            }
        }
        if (asc)
        {
            if (gameObject.transform.localEulerAngles.x > 1 && gameObject.transform.localEulerAngles.x <120)
            {
                Down(-1);
            }
            else
            {
                draft = false;
                asc = false;
            }
        }
        //Debug.Log(this.gameObject.transform.localRotation.x);
        if (this.gameObject.transform.localEulerAngles.x > 60) {
            if (!spawning)
            {
                if (timer > 0.04f)
                {
                    Drop();
                    timer = 0.0f;
                }
                else if (!spawning)
                {
                    timer += Time.deltaTime;
                }
            }
        }
	}



	void Drop()
	{
		
		//set spawning to true, to stop timer counting in the Update function
		spawning = true;
		GameObject drop = (GameObject)Instantiate(dropPrefab, spawner.transform.position, Quaternion.identity);
		drop.transform.localScale=new Vector3(0.025f, 0.025f, 0.025f);
		//set spawning back to false so timer may start again
		spawning = false;

	}
    void Down(float b)
    {
        float speed = b > 0 ? 80 - gameObject.transform.localEulerAngles.x : gameObject.transform.localEulerAngles.x;
        speed = Mathf.Max(speed, 1);
        this.gameObject.transform.Rotate(Vector3.right * b * Time.deltaTime * speed);
    }
}