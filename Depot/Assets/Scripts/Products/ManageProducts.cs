using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProduct", menuName = "Products")]
public class ManageProducts : ScriptableObject
{
    public string productName;
    public float happyProdIndex;
    public float productHeavy;
    public float createTime;
    public Sprite artwork;
    public int cost;

}
