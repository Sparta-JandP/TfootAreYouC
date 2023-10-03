using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSceneUIController : MonoBehaviour
{
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private TMP_Text _coinText;

    [SerializeField] private Button muteButton;
    [SerializeField] private Button muteOffButton;
    [SerializeField] private Slider volumeSlider;


    private void Start()
    {
        GameManager.instance.OnCoinChange += UpdateCoin;

        SoundManager.instance.muteButton = this.muteButton;
        SoundManager.instance.muteOffButton = this.muteOffButton;
        SoundManager.instance.volumeSlider = this.volumeSlider;

        UpdateCoin();
    }

    private void UpdateCoin()
    {
        _coinText.text = GameManager.instance.Coin.ToString();
    }

    public void OpenUpgradePanel()
    {
        SoundManager.instance.PlayEffect("positive");
        _upgradePanel.SetActive(true);
    }

    public void CloseUpgradePanel()
    {
        SoundManager.instance.PlayEffect("negative");
        _upgradePanel.SetActive(false);
    }

    public void OpenSettingPanel()
    {
        SoundManager.instance.PlayEffect("positive");
        _settingPanel.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        SoundManager.instance.PlayEffect("negative");
        _settingPanel.SetActive(false);
    }

    public void GameSceneChange()
    {
        SoundManager.instance.PlayEffect("positive");
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
