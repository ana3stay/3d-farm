using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameGrid : MonoBehaviour
{

    public int columnLength, rowLength;

    public float x_Space, z_Space;

    public GameObject grass;

    public GameObject[] currentGrid;

    public bool gotGrid;

    public GameObject hitted;

    public GameObject field;

    private RaycastHit _Hit;

    public bool creatingFields;

    public Texture2D basicCursor, fieldCursor, seedCursor;

    public CursorMode cursorMode = CursorMode.Auto;

    public Vector2 hotSpot = Vector2.zero;

    public GameObject goldSystem;

    public int fieldsPrice;

    public GameObject seed;

    void Awake()
    {
        Cursor.SetCursor(basicCursor, hotSpot, cursorMode);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < columnLength * rowLength; i++)
        {
            Instantiate(grass, new Vector3(x_Space + (x_Space * (i % columnLength)), 0, z_Space + (z_Space * (i / columnLength))), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gotGrid == false)
        {
            currentGrid = GameObject.FindGameObjectsWithTag("grid");

            gotGrid = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _Hit))
            {
                if (creatingFields == true)
                {
                    if (_Hit.transform.tag == "grid" && goldSystem.GetComponent<GoldSystem>().gold >= fieldsPrice)
                    {
                        hitted = _Hit.transform.gameObject;
                        Instantiate(field, hitted.transform.position, Quaternion.identity);
                        Destroy(hitted);

                        goldSystem.GetComponent<GoldSystem>().gold -= fieldsPrice;

                        //Cursor.SetCursor(fieldCursor, hotSpot, cursorMode);
                    }
                }

                if (Product.isSowing == true)
                {
                    if (_Hit.transform.tag == "field" && goldSystem.GetComponent<GoldSystem>().gold >= Product.currentProductPrice)
                    {
                        hitted = _Hit.transform.gameObject;
                        Instantiate(seed, hitted.transform.position, Quaternion.identity);
                        Destroy(hitted);

                        goldSystem.GetComponent<GoldSystem>().gold -= Product.currentProductPrice;
                    }
                }

                if(creatingFields == false && Product.isSowing == false)
                {
                    if(_Hit.transform.tag == "crops")
                    {
                        hitted = _Hit.transform.gameObject;
                        Instantiate(field, hitted.transform.position, Quaternion.identity);
                        Destroy(hitted);
                        print("get crops +1");
                    }
                }


            }
        }


            if (creatingFields == true)
            {
                 Cursor.SetCursor(fieldCursor, hotSpot, cursorMode);

                 Product.isSowing = false;
            }

            if (Shop.beInShop == true)
            {
                creatingFields = false;

                Cursor.SetCursor(basicCursor, hotSpot, cursorMode);
            }

            if (Product.isSowing == true)
            {
                creatingFields = false;

                Cursor.SetCursor(seedCursor, hotSpot, cursorMode);
            }

            if (Input.GetMouseButtonDown(1))
            {
                ClearCursor();
            }

        
    }

    public void CreateFields()
    {
        creatingFields = true;
    }

    public void returnToNormality()
    {
        creatingFields = false;
    }

    public void ClearCursor()
    {
        creatingFields = false;
        Product.isSowing = false;

        Cursor.SetCursor(basicCursor, hotSpot, cursorMode);
    }
}
