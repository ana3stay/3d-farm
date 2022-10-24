using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public int[] id;

    public string[] productName;

    public int[] price;

    public int numberOfProducts;

    public GameObject shopWindow;

    //public GameObject productPrefab;

    public GameObject[] products;

    public int pageNumber;

    public static bool beInShop;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<4; i++)
        {
            products[i].SetActive(false);
        }

        Refresh();

    }

    // Update is called once per frame
    void Update()
    {
        if(Product.isSowing == true)
        {
            CloseShop();
        }
    }

    public void OpenShop()
    {
        shopWindow.SetActive(true);

        beInShop = true;

        Refresh();

    }

    public void CloseShop()
    {

        beInShop = false;

        shopWindow.SetActive(false);
    }

    public void Refresh()
    {
        for(int i=0; i<numberOfProducts; i++)
        {
            products[i].SetActive(false);
        }

        if(pageNumber == 1)
        {
            for(int i=0; i<numberOfProducts; i++)
            {
                products[i].GetComponent<Product>().id = id[i];
                products[i].SetActive(true);
            }
        }
    }

    public void Left()
    {

    }

    public void Right()
    {

    }
}
