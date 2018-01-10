using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientPosition : MonoBehaviour {

    private Robot client;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsFree()
    {
        return client == null;
    }

    public void setClient(Robot newClient)
    {
        if (client == null)
        {
            client = newClient;
        }
    }

    public void clientLeft()
    {
        client = null;
    }
}
