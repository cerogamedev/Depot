using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class NpcStateManager : MonoBehaviour
{
    private GameObject canva, prefabIncome, prefabOutcome, ContentIncome, ContentOutcome;
    public  GameObject[] npcsIncome, npcsOutcome, incomeSheets, outcomeSheets;
    public Button[] incomeButton;

    public bool updateState = false, buttonAssing = false;
    private void Awake()
    {
        canva = GameObject.Find("NpcStateCanva");
        npcsIncome = GameObject.FindGameObjectsWithTag("IncomePersonal");
        npcsOutcome = GameObject.FindGameObjectsWithTag("OutcomePersonel");
        prefabIncome = GameObject.Find("NpcSheetIncome");
        prefabOutcome = GameObject.Find("NpcSheetOutcome");

        ContentIncome = GameObject.Find("ContentIncome");
        ContentOutcome = GameObject.Find("ContentOutcome");

        incomeSheets = GameObject.FindGameObjectsWithTag("IncomeSheet");
        outcomeSheets = GameObject.FindGameObjectsWithTag("OutcomeSheet");
    }
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        npcsIncome = GameObject.FindGameObjectsWithTag("IncomePersonal");
        npcsOutcome = GameObject.FindGameObjectsWithTag("OutcomePersonel");
        incomeSheets = GameObject.FindGameObjectsWithTag("IncomeSheet");

        if (updateState)
        {
            if (npcsIncome.Length == incomeSheets.Length)
            {

                for (int i = 0; i < npcsIncome.Length; i++)
                {
                    incomeSheets[i].transform.Find("StaminaText").GetComponent<TextMeshProUGUI>().text = "Stamina: " + npcsIncome[i].GetComponent<NpcStamina>().currentStamina.ToString();
                    incomeSheets[i].transform.Find("HappinessText").GetComponent<TextMeshProUGUI>().text = "Happiness: " + npcsIncome[i].GetComponent<NpcHappines>().currentHappy.ToString();
                }
            }
            else if (npcsIncome.Length > incomeSheets.Length)
            {
                Instantiate(prefabIncome, ContentIncome.transform);
            }
            else if (npcsIncome.Length < incomeSheets.Length)
            {
                Destroy(incomeSheets[incomeSheets.Length - 1]);
            }


            if (npcsOutcome.Length == outcomeSheets.Length)
            {

                for (int i = 0; i < npcsOutcome.Length; i++)
                {
                    outcomeSheets[i].transform.Find("StaminaText").GetComponent<TextMeshProUGUI>().text = "Stamina: " + npcsOutcome[i].GetComponent<NpcStamina>().currentStamina.ToString();
                    outcomeSheets[i].transform.Find("HappinessText").GetComponent<TextMeshProUGUI>().text = "Happiness: " + npcsOutcome[i].GetComponent<NpcHappines>().currentHappy.ToString();
                }
            }
            else if (npcsOutcome.Length > outcomeSheets.Length)
            {
                Instantiate(prefabOutcome, ContentOutcome.transform);
            }
            else if (npcsOutcome.Length < outcomeSheets.Length)
            {
                Destroy(outcomeSheets[outcomeSheets.Length - 1]);
            }
        }
    }

    public void Show()
    {
        canva.SetActive(true);
        updateState = true;

    }
    private void OnMouseDown()
    {
        canva.SetActive(true);
        updateState = true;
    }
}
