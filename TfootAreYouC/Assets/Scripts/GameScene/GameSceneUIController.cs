using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIController : MonoBehaviour
{
    [Header("SandClock")]
    [SerializeField] private Image _mineral;
    [SerializeField] private TMP_Text _mineralAmount;

    [Header("Health")]
    [SerializeField] private Image _kingHealthBar;
    [SerializeField] private Image _trumpBossHealthBar;

    [Header("Stage")]
    [SerializeField] private TMP_Text _stageNumText;


    private StageManager _stageManager;

    int _maxMineral;
    int _maxKingHealth;
    int _maxTrumpBossHealth;

    int _stageNum;
    int _curMineral;
    int _curKingHealth;
    int _curTrumpBossHealth;


    void Start()
    {
        _stageManager = StageManager.instance;

        _maxMineral = _stageManager.maxMineral;
        _maxKingHealth = _stageManager.maxKingHealth;
        _maxTrumpBossHealth = _stageManager.maxBossHealth;

        _stageManager.OnStageClear += UpdateStageNumUI;
        _stageManager.OnMining += UpdateSandAmountUI;
        _stageManager.OnKingHealthChange += UpdateKingHealthUI;
        _stageManager.OnBossHealthChange += UpdateTrumpBossHealthUI;
    }

    void UpdateStageNumUI()
    {
        _stageNum = _stageManager.currentStage; 
        _stageNumText.text = _stageNum.ToString();
    }

    void UpdateSandAmountUI()
    {
        _curMineral = _stageManager.mineral;
        _mineral.fillAmount = (float)_curMineral / _maxMineral;
        _mineralAmount.text = _curMineral.ToString();
    }

    void UpdateKingHealthUI()
    {
        _curKingHealth = _stageManager.kingHealth;
        _kingHealthBar.fillAmount = (float)_curKingHealth / _maxKingHealth;
    }

    void UpdateTrumpBossHealthUI()
    {
        _curTrumpBossHealth = _stageManager.bossHealth;
        _trumpBossHealthBar.fillAmount = (float)_curTrumpBossHealth / _maxTrumpBossHealth;
    }
}
