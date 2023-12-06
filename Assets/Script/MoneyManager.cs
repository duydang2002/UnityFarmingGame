using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] public static int currentMoney;
    // Start is called before the first frame update
    void Start()
    {
        currentMoney = 0;
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
