using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChessSlot : MonoBehaviour
{
    public Sprite chessSprite;

    public GameObject chessObject;

    public Image icon;

    public TextMeshProUGUI priceText;

    private GameManager gms;

    private void Start()
    {
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetComponent<Button>().onClick.AddListener(BuyChess);
    }

    private void BuyChess()
    {
        //gms
    }
    private void OnValidate()
    {
        if (chessSprite)
        {
            icon.enabled = true;
            icon.sprite = chessSprite;
        }
        else
        {
            icon.enabled = false;
        }
    }
}
