using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public event Action OnCoinChange;
    public int Coin {get; set;}

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        Coin = PlayerPrefs.GetInt("Coin", 1000); ;
    }

    public bool CoinClamp(int price)
    {
        if (Coin - price < 0) return false;
        Coin += price;
        OnCoinChange?.Invoke();
        PlayerPrefs.SetInt("Coin", Coin);
        return true;
    }
}
