using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    //public Vector3 spawnPosition;
    public GameObject NPC;
	// Use this for initialization
	void Start () {
        spawnNPC();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void spawnNPC()
    {
        Quaternion spawnRotation = Quaternion.identity;
        var npc = Instantiate(NPC, NPC.transform.position, NPC.transform.rotation);
    }
}
