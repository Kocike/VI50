using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject spawn;
    public GameObject NPC;

	// Use this for initialization
	void Start () {
        spawnNPC();
	}
	
	// Update is called once per frame
	void Update () {
        var nbNPC = GameObject.FindGameObjectsWithTag("NPC").Length;
        if (nbNPC == 0)
        {
            spawnNPC();
        }
	}

    void spawnNPC()
    {
        Quaternion spawnRotation = Quaternion.identity;
        var npc = Instantiate(NPC, spawn.transform.position, NPC.transform.rotation);
    }
}
