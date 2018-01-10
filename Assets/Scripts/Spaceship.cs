using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour {
    private Transform start;
    private Transform end;
    public GameObject prefab;
    private int NB_POS = 4;
	// Use this for initialization
	void Start () {
        int s = Mathf.RoundToInt(Random.Range(1, NB_POS));
        start = GameObject.Find("pos" + s.ToString()).transform;
        Debug.Log("pos" + s.ToString());
        int e = Mathf.RoundToInt(Random.Range(1, NB_POS));
        while (e == s)
        {
            e = Mathf.RoundToInt(Random.Range(1, NB_POS));
        }
        end = GameObject.Find("pos" + e.ToString()).transform;
        this.gameObject.transform.position = start.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position != end.position)
        {
            Quaternion neededRotation = Quaternion.LookRotation(end.position - transform.position);
            var rot = Quaternion.RotateTowards(transform.rotation, neededRotation, 250 * Time.deltaTime);
            GetComponent<Rigidbody>().MoveRotation(rot);

            var pos = Vector3.MoveTowards(transform.position, end.position, 100*Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else
        {
            Instantiate(prefab, null);
            Destroy(gameObject);
        }
    }
}
