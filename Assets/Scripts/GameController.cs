using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject spawn;
    public GameObject NPC;
    public int Score;
    public Text scoreText;
    public float spawnRate = 20;
    private float lastSpawnTime;

    // Use this for initialization
    void Start() {
        spawnNPC();
        Score = 0;
        updateScoreText();
    }

    // Update is called once per frame
    void Update()
    { 
        spawnNPC();
	}

    void spawnNPC()
    {
        var nbNPC = GameObject.FindGameObjectsWithTag("NPC").Length;
        if ((nbNPC < 3 && Time.time >= lastSpawnTime + spawnRate) || nbNPC == 0)
        {
            lastSpawnTime = Time.time;
            Quaternion spawnRotation = Quaternion.identity;
            //new Vector3 spawn_pos=
            var npc = Instantiate(NPC, spawn.transform.position, NPC.transform.rotation);
            float rnd = Random.Range(0.3f, 0.5f);
            npc.transform.localScale = new Vector3(rnd, rnd, rnd);
        }
    }

    void updateScoreText()
    {
        scoreText.text = Score.ToString();
    }

    public void addToScore(int sc)
    {
        Score += sc;
        updateScoreText();
    }
}
