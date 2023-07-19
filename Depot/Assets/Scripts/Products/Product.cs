using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public ManageProducts mp;

    [HideInInspector] public float _happyProdIndex;
    [HideInInspector] public float _productHeavy;
    [HideInInspector] public float _createTime;
    [HideInInspector] public Sprite _artwork;

    [HideInInspector] public string _productName;
    [HideInInspector] public int _cost;

    void Start()
    {
        _productName = mp.productName;
        this.name = mp.productName;
        _happyProdIndex = mp.happyProdIndex;
        _productHeavy = mp.productHeavy;
        _createTime = mp.createTime;
        _artwork = mp.artwork;
        _cost = mp.cost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
