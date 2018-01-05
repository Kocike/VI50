using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Lang;

public class Robot : MonoBehaviour {
	
	public Transform[] target;
    public Animator RobotAnimator;
    public Canvas canvas;
    public float speed;

    [HideInInspector]
    public Product productWanted;

    private int current;

    // Use this for initialization
    void Start () {
        RobotAnimator = GetComponent<Animator>();
        RobotAnimator.SetBool("move", false);
        canvas.enabled = false;

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
        if (transform.position != target[current].position)
        {
            RobotAnimator.SetBool("move", true);
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else
        {
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
