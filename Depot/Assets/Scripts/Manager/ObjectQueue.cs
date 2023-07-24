using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;


public class ObjectQueue : MonoBehaviour
{
    public GameObject prefabObject;
    public int orderObject = 0;
    public float slideSpeed = 0.2f;

    private GameObject canva;
    private float spacing = 2.0f;
    private int currentObjectSize = 6;
    private Button OrderButton;
    private TMP_Dropdown _dropDown;

    //order prod
    public TMP_InputField prodNumber;
    public GameObject[] products;
    List<string> prodList = new List<string> { };

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetLeftBound();
        canva = GameObject.FindGameObjectWithTag("GetProdCanva");
    }
    private void Start()
    {
        float leftBound = GetLeftBound();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).transform.position = new Vector3(leftBound + i * spacing, this.transform.position.y, 0);
        }
        canva.SetActive(false);
    }
    private float GetLeftBound()
    {
        Vector3 objectPosition = transform.position;
        float objectWidth = spriteRenderer.bounds.size.x;

        float leftBound = objectPosition.x - objectWidth / 2f;
        return leftBound;
    }
    private void Update()
    {
        float leftBound = GetLeftBound();
        if (transform.childCount != currentObjectSize && orderObject>0)
        {
            if (this.transform.childCount == 0)
            {
                Instantiate(prefabObject, new Vector3(leftBound, this.transform.position.y, 0), Quaternion.identity, this.transform);
            }
            else
                Instantiate(prefabObject, new Vector3(this.transform.GetChild(this.transform.childCount - 1).transform.position.x + spacing, this.transform.position.y, 0), Quaternion.identity, this.transform);

            orderObject -= 1;
        }
        if (this.transform.childCount>0)
        {
            if (this.transform.GetChild(0).transform.position.x >= leftBound)
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    Vector2 newpos = Vector2.MoveTowards(new Vector2(this.transform.GetChild(i).transform.position.x, this.transform.GetChild(i).transform.position.y), new Vector2(this.transform.GetChild(i).transform.position.x - 0.01f, this.transform.GetChild(i).transform.position.y), slideSpeed);
                    this.transform.GetChild(i).transform.position = newpos;
                }
            }
        }


        //order codes
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            worldPosition.z = 0f;

            Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);

            if (hitCollider != null && hitCollider.gameObject == this.gameObject)
            {
                canva.SetActive(true);
                FindButtons();
            }

        }
    }
    public void FindButtons()
    {
        OrderButton = GameObject.Find("OrderButton").GetComponent<Button>();
        OrderButton.onClick.AddListener(OrderProd);

        prodNumber = GameObject.Find("InputField").GetComponent<TMP_InputField>();
        _dropDown = GameObject.Find("Dropdown").GetComponent<TMP_Dropdown>();
        for (int i = 0; i<products.Length; i++)
        {
            prodList.Add(products[i].name);
        }
        _dropDown.AddOptions(prodList);

    }

    public void DropdownController(int index)
    {
        for (int j = 0; j<products.Length; j++)
        {
            if (j == index)
            {
                prefabObject = products[j];
            }
        }
    }

    //canva voids
    public void OrderProd()
    {
        if (orderObject == 0)
        {
            orderObject = int.Parse(prodNumber.text);

        }
    }
    public void IncreaseProdNumb()
    {
        prodNumber.text = (int.Parse(prodNumber.text) + 1).ToString();
    }
    public void DownProdNumb()
    {
        prodNumber.text = (int.Parse(prodNumber.text) - 1).ToString();
    }
}
