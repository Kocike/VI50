using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duplicate : MonoBehaviour {
	private float timer;
	// Use this for initialization
	void Start () {
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 3.5f) {
			//Debug.Log("duplicate");
			for (int i = 0; i < 2; i++) {
				GameObject drop = (GameObject)Instantiate (gameObject, this.transform.position, Quaternion.identity);
				drop.GetComponent<duplicate> ().enabled = false;
			}
			this.enabled = false;
		}
		timer += Time.deltaTime;

	}
}
