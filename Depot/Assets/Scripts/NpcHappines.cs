using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHappines : MonoBehaviour
{
    public float currentHappy, maxHappy;
    void Start()
    {
        currentHappy = maxHappy/2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetHappy()
    {
        return currentHappy;
    }
    public void SetHappy(float happy)
    {
        currentHappy += happy;
    }
    public void SetMaxHappy(float addMaxHappy)
    {
        maxHappy += addMaxHappy;
    }
}
