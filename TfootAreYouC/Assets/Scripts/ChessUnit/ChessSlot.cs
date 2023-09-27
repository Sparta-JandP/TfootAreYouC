using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChessSlot : MonoBehaviour
{
    public Sprite chessSprite;

    public GameObject chessObject;

    public int price;

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
        if(gms.sands >= price && !gms.currentChess)
        {
            gms.sands -= price;
            gms.BuyChess(chessObject, chessSprite);
        }
    }
    private void OnValidate()
    {
        if (chessSprite)
        {
            icon.enabled = true;
            icon.sprite = chessSprite;
            priceText.text = price.ToString();
        }
        else
        {
            icon.enabled = false;
        }
    }
}
