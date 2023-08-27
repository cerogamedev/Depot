using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcController : MonoBehaviour
{
    private GameObject canva;
    public NpcProperties npcProp;
    private void Awake()
    {
        canva = GameObject.Find("NPC-control");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        canva.SetActive(true);
        GameObject name = GameObject.Find("npcName");
        name.GetComponent<TextMeshProUGUI>().text = npcProp.npcName;
    }
}
