using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Lang;
using UnityEngine.UI;


public class Robot : MonoBehaviour {
	
	private Transform[] target;
    public Animator RobotAnimator;
    public Canvas canvas;
    public float speed;
    private bool hasProductWanted = false;

    private GameObject targetObject;
    private Transform roomEntrance;
    private Vector3 target_pos;

    [HideInInspector]
    public Product productWanted;
    private GameObject productDropZone;
    public GameController gameController;
    Random rnd = new Random();

    public int maxWaitTime = 50; //Time before the client leaves
    private int time;

    private bool exiting = false;
    private int current;

    // Use this for initialization
    void Start() {
        RobotAnimator = GetComponent<Animator>();
        RobotAnimator.SetBool("move", false);
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        canvas.enabled = false;

        // Pick a random (free) client position
        var positions = new List<GameObject>(GameObject.FindGameObjectsWithTag("ClientPosition"));
        foreach (GameObject pos in Shuffle(positions))
        {
            //Debug.Log(pos.ToString());
            if (pos.GetComponent<ClientPosition>().IsFree())
            {
                pos.GetComponent<ClientPosition>().setClient(this);
                target = new Transform[] { pos.transform };
                //Debug.Log(target);
                targetObject = pos;
                productDropZone = pos.transform.GetChild(0).gameObject;
                break;
            }
        }

        //target = new Transform[] { GameObject.FindGameObjectWithTag("ClientPosition").transform };
        roomEntrance = GameObject.FindGameObjectWithTag("RoomEntrance").transform;        
        target_pos = target[current].position;
        target_pos.y = transform.position.y;
        exiting = false;

        StartTimer();

        //Pick a random product > 2 car on ne veut pas de verre vide ni de burger pas cuit
        productWanted = (Product)(Random.Range(2, System.Enum.GetNames(typeof(Product)).Length));
        //Debug.Log(productWanted);
        gameObject.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprites/"+productWanted);

    }

    // Update is called once per frame
    void Update () {
        if (!exiting)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                hasProductWanted = true;
                StopCoroutine("timer");

                // Give a tip if service was fast 
                gameController.addToScore(5 + System.Math.Min(time, 10));
                StartCoroutine(TalkAndLeave("Thanks !",3f));
            }

            //Check there is a delivered product
            if (productDropZone.GetComponent<DropZoneScript>().HasProduct(productWanted))
            {
                hasProductWanted = true;
                StopCoroutine("timer");

                // Give a tip if service was fast 
                gameController.addToScore(5+ System.Math.Min(time,10));
                productDropZone.GetComponent<DropZoneScript>().RemoveProductOfType(productWanted);
                StartCoroutine(TalkAndLeave("Thanks !",3f));
            }
        }
        else // Go back to the room entrance
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

    // Say thanks, wait 3s then leave
    IEnumerator TalkAndLeave(string speach, float waitTime)
    {
        gameObject.GetComponentInChildren<Image>().enabled = false;
        gameObject.GetComponentInChildren<Text>().enabled = true;
        gameObject.GetComponentInChildren<Text>().text = speach;

        //canvas.GetComponent<Text>().text = "Thank you !";
        //canvas.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(waitTime);
        exiting = true;
    }

    private void StartTimer()
    {
        time = maxWaitTime;
        StartCoroutine("timer");
    }

    private int StopTimer()
    {
        StopCoroutine("timer");
        return time;
    }

    private IEnumerator timer()
    {
        while(time > 0)
        {
            time--;
            yield return new WaitForSeconds(1f);
        }
        if(exiting == false)
        {
            StartCoroutine(TalkAndLeave("Too late...", 2f));
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

    public IList<T> Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0,n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }
}
