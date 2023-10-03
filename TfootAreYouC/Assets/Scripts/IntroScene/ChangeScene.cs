using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject UpgradePanel;
    public GameObject GameOverPanel;

    public void SceneChange()
    {
        SoundManager.instance.PlayEffect("positive");
        SceneManager.LoadScene("HomeScene");
    }

    public void GameSceneChange()
    {
        SoundManager.instance.PlayEffect("positive");
        SceneManager.LoadScene("GameScene");
    }

    public void UpgradeSceneChange()
    {
        SoundManager.instance.PlayEffect("positive");
        UpgradePanel.SetActive(true);
    }

    public void UpgradeExitSceneChange()
    {
        SoundManager.instance.PlayEffect("negative");
        UpgradePanel.SetActive(false);
    }

    public void RetrySceneChange()
    {
        SceneManager.LoadScene("GameScene");
    }

}
