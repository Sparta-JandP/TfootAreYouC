using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int Coin {get; set;}

    private void Awake()
    {
        instance = this;
        Coin = 500000;
    }

    public bool CoinClamp(int price)
    {
        if (Coin - price < 0) return false;
        Coin -= price;
        return true;
    }
}
