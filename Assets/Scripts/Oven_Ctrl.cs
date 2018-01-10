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
    public float cookingTime;
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
            //Si c'est un produit, on l'ajoute a la liste de ce que contient le four
            if (!contains.Contains(C.gameObject)) { contains.Add(C.gameObject); }
        }
    }

    void OnTriggerExit(Collider C)
    {
        //Debug.Log(C.gameObject.tag);
        if (C.gameObject.tag == "Product")
        {
            //Si c'est un produit, on l'enleve a la liste de ce que contient le four
            if (contains.Contains(C.gameObject)) { contains.Remove(C.gameObject); }
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

    //On verouille le bouton du four si la porte n'est pas fermée ou si le four ne contient pas d'aliment a cuire
    void Unlock_test()
    {
        button.Locked = !(door_ctrl.closed && HasProduct(ToCook));
    }

    // Update is called once per frame
    void Update()
    {
        // Si le four est en marche, on incremente le temps de cuisson
        if (isCooking)
        {
            timer += Time.deltaTime;
        }
        else
        {
            //Sinon on verifie si le bouton doit etre verrouillé ou déverrouillé
            Unlock_test();
        }
        if (timer >= cookingTime)
        {
            //Quand le temps de cuisson est atteint, la cuisson s'arrete
            EndCook();
        }
    }

    private void EndCook()
    {
        //Debug.Log("Finito cookito");
        foreach (GameObject c in contains)
        {
            //On augmente l'echelle de l'aliment cuit et on change son type, on lance également le systeme de particule de l'objet pour simuler de la fumée
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
        //Lorsque le four est mis en marche, la porte et le bouton du four sont verrouillés et le timer demarre
        button.Locked = true;
        isCooking = true;
        door_ctrl.STOP = true;
        timer = 0.0f;
    }

}