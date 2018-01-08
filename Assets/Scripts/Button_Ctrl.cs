﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Ctrl : MonoBehaviour {
    public Transform StartPos;
    public Transform EndPos;
    private bool isPressed = false;
    public bool Locked = true;
    public Oven_Ctrl oven;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider C)
    {
        if (!Locked)
        {
            isPressed = true;
            gameObject.transform.position = EndPos.position;
            oven.Cook();
        }

    }
    public void Unpressed()
    {
            isPressed = false;
            gameObject.transform.position = StartPos.position;
    }

}
