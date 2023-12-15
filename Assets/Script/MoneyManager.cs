using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static int currentMoney;
    public int baseMoney = 1000;
    public TextMeshProUGUI moneyNumber;
    // Start is called before the first frame update
    void Start()
    {
        currentMoney = baseMoney;
        moneyNumber.text = currentMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddMoney(int money)
    {
        currentMoney += money;
    }
    public void SubtractMoney(int money)
    {
        if (money<= currentMoney)
        {
            currentMoney -= money;
        }
        else
        {
            Debug.LogError("Not enough money");
        }
    }
}
