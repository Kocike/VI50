using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product_spawner : MonoBehaviour {
    public Transform SpawnAt;
    public GameObject Product;
    private int count=0;
    public int nb_desired;

	// Use this for initialization
	void Start () {
        if (count < nb_desired)
        {
            GameObject spawning = (GameObject)Instantiate(Product, SpawnAt.transform.position, Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerExit(Collider C)
    {
        if (C.gameObject.name.StartsWith(Product.name))
        {
            count -= 1;
        }
        if (count < nb_desired)
        {
            GameObject spawning = (GameObject)Instantiate(Product, SpawnAt.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider C)
    {
        if (C.gameObject.name.StartsWith(Product.name))
        {
            count += 1;
        }
    }
}
