using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {
	public GameObject dropPrefab;
	private bool spawning =false;
	private float timer;
	private bool stop = true;
	private GameObject Pivot;
	public Animator Pivot_anim;
	public GameObject spawner;
    public bool touch;


	private bool draft=false;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		Pivot_anim.SetBool ("Down", false);
	}


    private void OnTriggerEnter(Collider C)
    {
        //if(C.
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger).ToString());

        if (!draft && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5f)
        {
            draft = true;
            if (Pivot_anim)
            {
                Pivot_anim.SetBool("Down", !Pivot_anim.GetBool("Down"));
            }
        }

        if (draft && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) < 0.5f)
        {
            draft = false;
            if (Pivot_anim)
            {
                Pivot_anim.SetBool("Down", !Pivot_anim.GetBool("Down"));
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Pivot_anim)
            {
                Pivot_anim.SetBool("Down", !Pivot_anim.GetBool("Down"));
            }
        }
        //Debug.Log(this.gameObject.transform.localRotation.x);
        if (this.gameObject.transform.localRotation.x > 0.6f) {
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
}