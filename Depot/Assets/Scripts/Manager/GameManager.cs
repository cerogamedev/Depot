using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject npcStateCanva;
    private void Awake()
    {
        npcStateCanva = GameObject.Find("NpcStateCanva");
    }
    void Start()
    {
        npcStateCanva.SetActive(false);
        GameObject controlCanva = GameObject.Find("NPC-control");
        controlCanva.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
