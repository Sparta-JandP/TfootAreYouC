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

    [Header("Sound")]
    [SerializeField] private Button muteButton;
    [SerializeField] private Button muteOffButton;
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private GameObject _disablePanel;

    private StageManager _stageManager;

    int _maxMineral;
    int _maxKingHealth;
    int _maxTrumpBossHealth;

    int _stageNum;
    int _curMineral;
    int _curKingHealth;
    int _curTrumpBossHealth;

    private void Awake()
    {
        SoundManager.instance.muteButton = this.muteButton;
        SoundManager.instance.muteOffButton = this.muteOffButton;
        SoundManager.instance.volumeSlider = this.volumeSlider;
    }
    void Start()
    {
        _stageManager = StageManager.instance;

        _maxMineral = _stageManager.maxMineral;
        _maxKingHealth = _stageManager.maxKingHealth;
        _maxTrumpBossHealth = _stageManager.maxBossHealth;

        Debug.Log(_stageManager);
        _stageManager.OnStageClear += OpenStageClear;
        _stageManager.OnStageResume += CloseStageClear;
        _stageManager.OnStageResume += UpdateStageNumUI;

        _stageManager.OnWin += OpenWinPanel;
        _stageManager.OnGameOver += OpenGameOverPanel;

        _stageManager.OnSandAmountChange += UpdateSandAmountUI;
        _stageManager.OnKingHealthChange += UpdateKingHealthUI;
        _stageManager.OnBossHealthChange += UpdateTrumpBossHealthUI;
        _stageManager.OnStageResume += UpdateTrumpBossHealthUI;
        _stageManager.OnStageResume += UpdateKingHealthUI;

        UpdateStageNumUI();
        UpdateSandAmountUI();
        UpdateKingHealthUI();
        UpdateTrumpBossHealthUI();

        _pausePanel.SetActive(false);
        _stageClearPanel.SetActive(false);
        _gameOverPanel.SetActive(false);

        
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
        SoundManager.instance.PlayEffect("positive");
        _soundSetting.SetActive(false);
        _pauseBtns.SetActive(true);
        _pausePanel.SetActive(true);
    }

    public void ClosePausePanel()
    {
        SoundManager.instance.PlayEffect("negative");
        _pausePanel.SetActive(false);
    }

    public void OpenSoundSetting()
    {
        SoundManager.instance.PlayEffect("positive");
        _soundSetting.SetActive(true);
        _pauseBtns.SetActive(false);
    }

    public void CloseSoundSetting()
    {
        SoundManager.instance.PlayEffect("negative");
        _soundSetting.SetActive(false);
        _pauseBtns.SetActive(true);
    }

    void OpenStageClear()
    {
        StartCoroutine(StageControl(_stageClearPanel));
        _maxKingHealth = _stageManager.maxKingHealth;
        _maxTrumpBossHealth = _stageManager.maxBossHealth;
    }

    void CloseStageClear()
    {
        _stageClearPanel.SetActive(false);
    }
    
    void OpenWinPanel()
    {
        // 보상 UI에 연결
        StartCoroutine(StageControl(_winPanel));
    }

    void OpenGameOverPanel()
    {
        StartCoroutine(StageControl(_gameOverPanel));
    }

    IEnumerator StageControl(GameObject panel)
    {
        _disablePanel.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        panel.SetActive(true);
        _disablePanel.SetActive(false);
    }
}
