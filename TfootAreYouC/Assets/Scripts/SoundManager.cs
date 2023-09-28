using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _effectSource;
    [SerializeField] private AudioClip game_bgm;
    [SerializeField] private AudioClip home_bgm;
    [SerializeField] private AudioClip positive;
    [SerializeField] private AudioClip negative;
    [SerializeField] private AudioClip option;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void OnScSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name == "GameScene")
        {
            PlayBGM(game_bgm);
        }
        else if (scene.name == "IntroScene")
        {
            PlayBGM(home_bgm);
        }
        else
        {
            if(_bgmSource.clip == game_bgm)
            {
                PlayBGM(home_bgm);
            }
        }
    }

    private void PlayBGM(AudioClip bgm)
    {
        _bgmSource.clip = bgm;
        _bgmSource.loop = true;
        _bgmSource.volume = 0.5f;
        _bgmSource.Play();
    }

    public void PlayEffect(string effectName)
    {
        _effectSource.volume = 0.7f;

        switch (effectName)
        {
            case "positive":
                _effectSource.PlayOneShot(positive);
                break;

            case "negative":
                _effectSource.PlayOneShot(negative);
                break;

            case "option":
                _effectSource.volume = 0.5f;
                _effectSource.PlayOneShot(option);
                break;
        }
    }

    public void PlayEffect(AudioClip effect)
    {
        _effectSource.volume = 0.7f;
        _effectSource.PlayOneShot(effect);
    }
}
