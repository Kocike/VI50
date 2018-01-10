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
            if (C.gameObject.GetComponent<ProductType>().type == Product.Glass || C.gameObject.GetComponent<ProductType>().type == Product.Beer || C.gameObject.GetComponent<ProductType>().type == Product.Oil)
            {
                C.gameObject.GetComponent<ProductType>().type = C.gameObject.GetComponentInChildren<ContentFinder>().getContentType();

            }
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
                Debug.Log(this.gameObject.name + " contains " + c.GetComponent<ProductType>().type.ToString());
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
        GameObject c;
        c = contains.Find(comp => comp.GetComponent<ProductType>().type == type);
        /**if (type == Product.Beer ||type == Product.Oil)
        {
            c = contains.Find(comp => comp.GetComponent<ProductType>().type == Product.Glass);
        }
        else
        {
            c = contains.Find(comp => comp.GetComponent<ProductType>().type == type);
        }*/
        TakeAway(c);
        Destroy(c.gameObject);
    }
}
