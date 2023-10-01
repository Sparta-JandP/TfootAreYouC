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

    [Header("GameProgress")]
    [SerializeField] private TMP_Text _stageNumText;
    [SerializeField] private GameObject _stageClearPanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _winPanel;

    [Header("Pause")]
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _pauseBtns;
    [SerializeField] private GameObject _soundSetting;



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
        _stageManager.OnStageClear += OpenStageClear;
        _stageManager.OnStageResume += CloseStageClear;

        _stageManager.OnWin += OpenWinPanel;
        _stageManager.OnGameOver += OpenGameOverPanel;

        _stageManager.OnSandAmountChange += UpdateSandAmountUI;
        _stageManager.OnKingHealthChange += UpdateKingHealthUI;
        _stageManager.OnBossHealthChange += UpdateTrumpBossHealthUI;

        UpdateStageNumUI();
        UpdateSandAmountUI();
        UpdateKingHealthUI();
        UpdateTrumpBossHealthUI();

        _pausePanel.SetActive(false);
        _stageClearPanel.SetActive(false);
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

    public void OpenPausePanel()
    {
        _soundSetting.SetActive(false);
        _pauseBtns.SetActive(true);
        _pausePanel.SetActive(true);
    }

    public void ClosePausePanel()
    {
        _pausePanel.SetActive(false);
    }

    public void OpenSoundSetting()
    {
        _soundSetting.SetActive(true);
        _pauseBtns.SetActive(false);
    }

    public void CloseSoundSetting()
    {
        _soundSetting.SetActive(false);
        _pauseBtns.SetActive(true);
    }

    void OpenStageClear()
    {
        _stageClearPanel.SetActive(true);
    }

    void CloseStageClear()
    {
        _stageClearPanel.SetActive(false);
    }
    
    void OpenWinPanel()
    {
        // 보상 UI에 연결
        _winPanel.SetActive(true);
    }

    void OpenGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }
}
