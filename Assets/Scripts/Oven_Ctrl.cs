using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven_Ctrl : MonoBehaviour
{
    public Door_Ctrl door_ctrl;
    public Product ToCook;
    public Product Cooked;
    private bool empty = true;
    private float timer=0.0f;
    private bool isCooking=false;
    public float CookingTime;
    public Button_Ctrl button;
    //public AudioSource ding;
    [HideInInspector]
    public List<GameObject> contains = new List<GameObject>();

    void OnTriggerEnter(Collider C)
    {
        //Debug.Log(C.gameObject.tag);
        if (C.gameObject.tag == "Product")
        {
            //Debug.Log("I contain something");
            if (!contains.Contains(C.gameObject)) { contains.Add(C.gameObject); }
        }
    }

    void OnTriggerExit(Collider C)
    {
        //Debug.Log(C.gameObject.tag);
        if (C.gameObject.tag == "Product")
        {
            //Debug.Log("I contain something");
            if (contains.Contains(C.gameObject)) { contains.Remove(C.gameObject); }
        }
    }

    private void TakeAway(GameObject other)
    {
        if (contains.Contains(other.gameObject))
        {
            contains.Remove(other.gameObject);
        }
    }

    private bool HasProduct(Product p)
    {
        foreach (GameObject c in contains)
        {
            if (c.GetComponent<ProductType>().type == p)
            {
                return true;
            }
        }
        return false;
    }

    void Unlock_test()
    {
        button.Locked = !(door_ctrl.closed && HasProduct(ToCook));
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooking)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Unlock_test();
        }
        if (timer >= CookingTime)
        {
            EndCook();
        }
    }

    private void EndCook()
    {
        //Debug.Log("Finito cookito");
        foreach (GameObject c in contains)
        {
            if (c.GetComponent<ProductType>().type == ToCook)
            {
                c.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                c.GetComponent<ProductType>().type = Cooked;
                c.GetComponent<ParticleSystem>().Play();
            }
        }
        isCooking = false;
        door_ctrl.STOP = false;
        button.Unpressed();
       // ding.Play();

    }

    public void Cook()
    {
        button.Locked = true;
        isCooking = true;
        door_ctrl.STOP = true;
        timer = 0.0f;
        
    }

}