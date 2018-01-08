using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZoneScript : MonoBehaviour {
    public List<GameObject> contains = new List<GameObject>();

    private void OnTriggerEnter(Collider C)
    {
        //Debug.Log(C.gameObject.tag);
        if (C.gameObject.tag == "Product")
        {
            //Debug.Log("I contain something");
            if (!contains.Contains(C.gameObject)) { contains.Add(C.gameObject); }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TakeAway(other.gameObject);
    }

    public bool HasProduct(Product p)
    {
       foreach (GameObject c in contains){
            if(c.GetComponent<ProductType>().type == p )
            {
                return true;
            }

            // if it's a glass check what is inside
            if(c.GetComponent<ProductType>().type == Product.Glass)
            {
                return c.GetComponent<ContentFinder>().getContentType() == p;
            }
        }
        return false;
    }

    public void RemoveProduct(GameObject other)
    {
        Destroy(other.gameObject);
    }

    public void TakeAway(GameObject other)
    {
        if (contains.Contains(other.gameObject))
        {
            contains.Remove(other.gameObject);
        }
    }

    public void RemoveProductOfType(Product type)
    {
        var c = contains.Find(comp => comp.GetComponent<ProductType>().type == type);
        TakeAway(c);
        Destroy(c.gameObject);
    }
}
