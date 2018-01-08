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
    private Vector3 target_pos;
    [HideInInspector]
    public Product productWanted;
    private GameObject productDropZone;
    public GameController gameController;

    private int current;

    // Use this for initialization
    void Start() {
        RobotAnimator = GetComponent<Animator>();
        RobotAnimator.SetBool("move", false);
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        canvas.enabled = false;
        target = new Transform[] { GameObject.FindGameObjectWithTag("ClientPosition").transform };
        roomEntrance = GameObject.FindGameObjectWithTag("RoomEntrance").transform;
        productDropZone = GameObject.FindGameObjectWithTag("ClientOrderTargetPosition");
        target_pos = target[current].position;
        target_pos.y = transform.position.y;


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
            gameController.addToScore(1);
        }

        //Check there is a delivered product
        if (productDropZone.GetComponent<DropZoneScript>().HasProduct(productWanted))
        {
            hasProductWanted = true;
            gameController.addToScore(1);
            productDropZone.GetComponent<DropZoneScript>().RemoveProductOfType(productWanted);
        }

        // Go back to the room entrance
        if (hasProductWanted)
        {
            target = new Transform[] { roomEntrance };
            target_pos = target[current].position;
            target_pos.y = this.gameObject.transform.position.y;
        }

        // Movement
        if (transform.position != target_pos)
        {
            RobotAnimator.SetBool("move", true);
            Quaternion neededRotation = Quaternion.LookRotation(target_pos - transform.position);
            var rot = Quaternion.RotateTowards(transform.rotation, neededRotation, 250 * Time.deltaTime);
            GetComponent<Rigidbody>().MoveRotation(rot);
            
            var pos = Vector3.MoveTowards(transform.position,target_pos, speed * Time.deltaTime);
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
