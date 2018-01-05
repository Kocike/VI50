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
	private bool draft=false;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		Pivot_anim.SetBool ("Down", false);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger).ToString());
		if (Input.GetKeyDown (KeyCode.A)) {
			if (Pivot_anim) {
				Pivot_anim.SetBool ("Down", !Pivot_anim.GetBool ("Down"));
			}
			stop = !stop;
		}
		if (!draft && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger)>0.5f) {
			if (Pivot_anim.GetBool("Down")==true) {
				Pivot_anim.SetBool ("Down", !Pivot_anim.GetBool ("Down"));
			}
			stop = false;
			draft = true;
		}
		if (draft && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger)>0.5f) {
			if (Pivot_anim.GetBool("Down")==falsee) {
				Pivot_anim.SetBool ("Down", !Pivot_anim.GetBool ("Down"));
			}
			stop = true;
			draft = false;
		}

		if (!spawning && !stop) {
			if (timer > 0.04f) {
				Water ();
				timer = 0.0f;
			} else if (!spawning) {
				timer += Time.deltaTime;
			}
		}
	}


	void Water()
	{
		
		//set spawning to true, to stop timer counting in the Update function
		spawning = true;


		GameObject drop = (GameObject)Instantiate(dropPrefab, spawner.transform.position, Quaternion.identity);
		//drop.AddComponent<DeleteDrop>();
		float rnd=Random.Range(0.002f,0.03f);
		drop.transform.localScale=new Vector3(rnd,rnd,rnd);

		//set spawning back to false so timer may start again
		spawning = false;

	}
}