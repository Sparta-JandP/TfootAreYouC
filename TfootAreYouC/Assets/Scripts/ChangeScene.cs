using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject UpgradePanel;

    public void SceneChange()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void GameSceneChange()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void UpgradeSceneChange()
    {
        UpgradePanel.SetActive(true);
    }
}
