using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] Text moneyDisplay;

    public int money;

    void Update()
    {
        moneyDisplay.text = $"{money}$";
    }

    public bool CanBuy(int price)
    {
        if(money >= price)
        {
            return true;
        }
        return false;
    }

    public void Buy(int price)
    {
        money -= price;
    }
}
