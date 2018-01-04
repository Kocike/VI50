using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {
	
	public Transform[] target;
    public Animator RobotAnimator;
    public float speed;
	private int current;
	
	// Use this for initialization
	void Start () {
        RobotAnimator = GetComponent<Animator>();
        RobotAnimator.SetBool("move", false);

    }

    // Update is called once per frame
    void Update () {
        if (transform.position != target[current].position)
        {
            RobotAnimator.SetBool("move", true);
            Debug.Log("coucou");
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
            //RobotAnimator.SetBool("move", false);
        }
        else
        {
            RobotAnimator.SetBool("move", false);
            current = (current + 1) % target.Length;
        }
	}
}
