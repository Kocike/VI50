using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Lang;

public class Robot : MonoBehaviour {
	
	private Transform[] target;
    public Animator RobotAnimator;
    public Canvas canvas;
    public float speed;
    private bool hasProductWanted = false;
    private Transform roomEntrance;

    [HideInInspector]
    public Product productWanted;

    private int current;

    // Use this for initialization
    void Start() {
        RobotAnimator = GetComponent<Animator>();
        RobotAnimator.SetBool("move", false);

        canvas.enabled = false;
        target = new Transform[] { GameObject.FindGameObjectWithTag("ClientPosition").transform };
        roomEntrance = GameObject.FindGameObjectWithTag("RoomEntrance").transform;

        //Pick a random product
        productWanted = (Product)(Random.Range(0, System.Enum.GetNames(typeof(Product)).Length));

        switch (productWanted)
        {
            case Product.Beer:
                gameObject.GetComponentInChildren<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("beer");
                break;
            case Product.Burger:
                gameObject.GetComponentInChildren<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("burger");
                break;
            case Product.Oil:
                gameObject.GetComponentInChildren<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("oil");
                break;
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            hasProductWanted = true;
            // Todo : GIVE MONEYZ
        }
        if (hasProductWanted)
        {
            target = new Transform[] { roomEntrance };
        }
        if (transform.position != target[current].position)
        {
            RobotAnimator.SetBool("move", true);
            Quaternion neededRotation = Quaternion.LookRotation(target[current].position - transform.position);
            var rot = Quaternion.RotateTowards(transform.rotation, neededRotation, 250 * Time.deltaTime);
            GetComponent<Rigidbody>().MoveRotation(rot);
            var pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else
        {
            // Destroy the NPC if its target is the room entrance and it has reached it
            if(target[current] == roomEntrance)
            {
                Destroy(gameObject);
            }
            RobotAnimator.SetBool("move", false);
            current = (current + 1) % target.Length;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canvas.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canvas.enabled = false;
        }
    }
}
