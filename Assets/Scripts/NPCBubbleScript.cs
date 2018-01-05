using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBubbleScript : MonoBehaviour
{

    public Transform player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = player.rotation;
    }
}