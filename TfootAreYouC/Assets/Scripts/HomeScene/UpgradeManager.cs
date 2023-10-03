using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// TODO: 강화 최대 레벨 설정, GameManager의 코인과 연결해서 구매 하도록
public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    [SerializeField] private UnitStats[] _chessStats;
    [SerializeField] private TMP_Text _effectDesc;
    [SerializeField] private Image[] _selectionUI;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text[] _effects;
    [SerializeField] private TMP_Text[] _levels;
    [SerializeField] private TMP_Text _coin;

    private int selectedChess;
    private string changedEffect;
    private int selectedPrice;

    private int maxLevel = 10;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetUI();
    }

    public void SelectChess(int chessNum)
    {
        if (_chessStats[chessNum].level >= maxLevel)
            return;
        SoundManager.instance.PlayEffect("option");
        selectedChess = chessNum;
        selectedPrice = _chessStats[selectedChess].level * 1200;
        StopCoroutine(InformUpdate());
        StopCoroutine(InformFailure());
        UpdateSelectionUI();
        UpdateEffectDesc();
    }

    public void UpgradeChess()
    {
        if (selectedChess != -1)
        {
            if (GameManager.instance.CoinClamp(selectedPrice))
            {
                Upgrade();
                UpdateWholeUI();    //여기서 inform
            }
            else
            {
                ResetUI();
            }
            _price.text = "0";
            _selectionUI[selectedChess].color = new Color32(255, 255, 255, 100);
            selectedChess = -1;
            changedEffect = "";
            selectedPrice = 0;
        }   
    }

    void Upgrade()
    {
        switch(selectedChess)
        {
            case 0:
                _chessStats[selectedChess].effectRate = Mathf.Round(_chessStats[selectedChess].effectRate * 0.8f * 100f) / 100f;
                break;
            case 1:
                _chessStats[selectedChess].effectPower += 1;
                _chessStats[selectedChess].speed = Mathf.Round(_chessStats[selectedChess].speed * 1.2f * 10000f) / 10000f;
                break;
            case 2:
                _chessStats[selectedChess].maxHealth += 3;
                break;
            case 3:
                _chessStats[selectedChess].effectRate = Mathf.Round(_chessStats[selectedChess].effectRate * 0.8f * 100f) / 100f;
                _chessStats[selectedChess].effectPower += 1;
                break;
            case 4:
                _chessStats[selectedChess].effectPower += 1;
                _chessStats[selectedChess].speed = Mathf.Round(_chessStats[selectedChess].speed * 1.2f * 10000f) / 10000f;
                break; 
            case 5:
                _chessStats[selectedChess].effectRate = Mathf.Round(_chessStats[selectedChess].effectRate * 0.8f * 100f) / 100f;
                break;
        }
        SoundManager.instance.PlayEffect("positive");
        _chessStats[selectedChess].level++;
    }

    void UpdateWholeUI()
    {
        StartCoroutine(InformUpdate());
        _effects[selectedChess].text = changedEffect;
        _levels[selectedChess].text = $"Lv. {_chessStats[selectedChess].level.ToString()}";
        if (_chessStats[selectedChess].level == 10)
            _levels[selectedChess].text = "Lv. Max";
        _coin.text = GameManager.instance.Coin.ToString();
    }

    void ResetUI()
    {
        StartCoroutine(InformFailure());

    }

    void UpdateSelectionUI()
    {
        for (int i = 0; i < _selectionUI.Length; i++)
        {
            if (i != selectedChess)
            {
                _selectionUI[i].color = new Color32(255, 255, 255, 100);
            }
            else
            {
                _selectionUI[i].color = new Color32(33, 127, 255, 100);
            }
        }

        _price.text = selectedPrice.ToString();
    }

    void UpdateEffectDesc()
    {
        _effectDesc.color = new Color32(255, 255, 255, 255);
        string content = "";
        switch (selectedChess)
        {
            case 0:
                content = $"모래 {Mathf.Round(_chessStats[selectedChess].effectRate * 0.8f * 100f) / 100f}초마다 생성";
                break;
            case 1:
                content = $"공격력 {_chessStats[selectedChess].effectPower + 1}\r\n이동 속도 {Mathf.Round(_chessStats[selectedChess].speed * 1.2f * 10000f) / 10000f}";
                break;
            case 2:
                content = $"체력 {_chessStats[selectedChess].maxHealth + 3}";
                break;
            case 3:
                content = $"{Mathf.Round(_chessStats[selectedChess].effectRate * 0.8f * 100f) / 100f}초마다 주변 동료 \r\n체력 회복 + {_chessStats[selectedChess].effectPower + 1}";
                break;
            case 4:
                content = $"공격력 {_chessStats[selectedChess].effectPower + 1}\r\n이동 속도 {Mathf.Round(_chessStats[selectedChess].speed * 1.2f * 10000f) / 10000f}";
                break;
            case 5:
                content = $"{Mathf.Round(_chessStats[selectedChess].effectRate * 0.8f * 100f) / 100f}초마다 장거리 공격";
                break;
        }
        _effectDesc.text = content;
        changedEffect = content;
    }

    void SetUI()
    {
        for (int i = 0; i < _chessStats.Length; i++)
        {
            switch (i)
            {
                case 0:
                    _effects[i].text = $"모래 {_chessStats[i].effectRate}초마다 생성";
                    break;
                case 1:
                    _effects[i].text = $"공격력 {_chessStats[i].effectPower}\r\n이동 속도 {_chessStats[i].speed}";
                    break;
                case 2:
                    _effects[i].text = $"체력 {_chessStats[i].maxHealth}";
                    break;
                case 3:
                    _effects[i].text = $"{_chessStats[i].effectRate}초마다 주변 동료 \r\n체력 회복 + {_chessStats[i].effectPower}";
                    break;
                case 4:
                    _effects[i].text = $"공격력 {_chessStats[i].effectPower}\r\n이동 속도 {_chessStats[i].speed}";
                    break;
                case 5:
                    _effects[i].text = $"{_chessStats[i].effectRate}초마다 장거리 공격";
                    break;
            }
            _levels[i].text = $"Lv. {_chessStats[i].level}";
            _selectionUI[i].color = new Color32(255, 255, 255, 100);
        }
        _coin.text = GameManager.instance.Coin.ToString();
        _effectDesc.color = new Color32(150, 150, 150, 255);
        _effectDesc.text = "대상을 선택하세요";
        _price.text = "0";
    }

    IEnumerator InformUpdate()
    {
        _effectDesc.color = new Color32(195, 255, 175, 255);
        _effectDesc.text = "강화가 완료되었습니다.";
        yield return new WaitForSeconds(2f);
        _effectDesc.color = new Color32(150, 150, 150, 255);
        _effectDesc.text = "대상을 선택하세요";
    }

    IEnumerator InformFailure()
    {
        _effectDesc.color = new Color32(255, 83, 83, 255);
        _effectDesc.text = "코인이 부족합니다.";
        yield return new WaitForSeconds(2f);
        _effectDesc.color = new Color32(150, 150, 150, 255);
        _effectDesc.text = "대상을 선택하세요";
    }
}
