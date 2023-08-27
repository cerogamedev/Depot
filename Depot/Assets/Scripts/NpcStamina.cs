using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NpcStamina : MonoBehaviour
{
    public float currentStamina, maxStamina = 100;
    public Image staminaBar;
    private Rigidbody2D rb;
    
    void Start()
    {
        currentStamina = maxStamina;
        GameObject canva = this.transform.GetChild(0).gameObject;
        staminaBar = canva.transform.GetChild(0).GetComponent<Image>();
        rb = GetComponent<Rigidbody2D>();
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
