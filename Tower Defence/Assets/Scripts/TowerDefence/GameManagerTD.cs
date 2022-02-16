using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public enum TargetingType
{
    First,
    Last,
    Strong
}

public class GameManagerTD : MonoBehaviour
{
    public int health;
    public int money;

    public TextMeshProUGUI healthTxt;
    public TextMeshProUGUI moneyTxt;

    public bool isTowerSelected;
    public GameObject selectedObj;

    public Transform AvailableTowers;
    public Transform CurrentLoadout;

    public GameObject StartButton;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();

        InitializeSelection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        health = 150;
        money = 250;

        UpdateHealth();
        UpdateMoney();
    }

    public void UpdateHealth()
    {
        healthTxt.text = health.ToString();
    }

    public void UpdateMoney()
    {
        moneyTxt.text = money.ToString();
    }

    public void SelectTowerClick()
    {
        if (!isTowerSelected)
        {
            GameObject currSelected = EventSystem.current.currentSelectedGameObject;

            if (currSelected.GetComponent<Tag>().tagComp == "Available Tower")
            {
                selectedObj = currSelected;

                isTowerSelected = true;
            }
        }
        else
        {
            if (selectedObj != null)
            {
                GameObject currSelected = EventSystem.current.currentSelectedGameObject;
                TowerInfo currTowerInfo = currSelected.GetComponent<TowerInfo>();

                if (currSelected.GetComponent<Tag>().tagComp == "Slot")
                {
                    TowerInfo towerInfo = selectedObj.GetComponent<TowerInfo>();

                    currSelected.transform.GetChild(4).gameObject.SetActive(true);
                    currSelected.transform.GetChild(3).gameObject.SetActive(true);
                    currSelected.transform.GetChild(2).gameObject.SetActive(false);

                    currTowerInfo.cost = towerInfo.cost;
                    currTowerInfo.costText.text = currTowerInfo.cost.ToString();
                    currTowerInfo.iconComp.sprite = towerInfo.prefabIcon;

                    isTowerSelected = false;
                }
            }
        }
    }

    public void InitializeSelection()
    {
        foreach (Transform t in CurrentLoadout)
        {
            t.GetChild(3).gameObject.SetActive(false);
            t.GetChild(4).gameObject.SetActive(false);
        }
    }
}
