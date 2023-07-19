using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NpcStamina : MonoBehaviour
{
    public float currentStamina, maxStamina = 100;
    private Image staminaBar;
    
    void Start()
    {
        currentStamina = maxStamina;
        GameObject canva = GameObject.Find("StaminaCanva");

    }

    // Update is called once per frame
    void Update()
    {
        staminaBar.fillAmount = currentStamina / maxStamina;
    }
    public void SetStamina(float stamina)
    {
        currentStamina += stamina;
    }
    public void SetMaxStamina(float stamina)
    {
        maxStamina += stamina;
    }
    public float GetStamina()
    {
        return currentStamina;
    }

}
