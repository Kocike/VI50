using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFinder : MonoBehaviour {

    private int beerCount;
    private int oilCount;

	// Use this for initialization
	void Start () {
        beerCount = 0;
        oilCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(oilCount);
        //Debug.Log(getContentType());
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);
        if (other.GetComponent<ProductType>().type == Product.Oil)
        {
            oilCount++;
        }
        if (other.GetComponent<ProductType>().type == Product.Beer)
        {
            beerCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ProductType>().type == Product.Oil)
        {
            oilCount--;
        }
        if (other.GetComponent<ProductType>().type == Product.Beer)
        {
            beerCount--;
        }
    }

    public Product getContentType()
    {
        if (oilCount > 5 || beerCount > 5)
        {
            if (beerCount >= oilCount)
            {
                return Product.Beer;
            }
            else
            {
                return Product.Oil;
            }
        }
        else
        {
            return Product.Glass;
        }
    }
}
