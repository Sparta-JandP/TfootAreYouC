using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

     public Button muteButton;
     public Button muteOffButton;
     public Slider volumeSlider;

    private float currentVolume = 0.3f;  // 현재 음량

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        // 초기 음량 설정
        _bgmSource.volume = currentVolume;

    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "GameScene")
        {
            if (_bgmSource.clip == home_bgm)
            {
                PlayBGM(game_bgm);
            }           
        }
        else if (scene.name == "IntroScene")
        {
            PlayBGM(home_bgm);
            
        }
        else
        {
            if (_bgmSource.clip == game_bgm)
            {
                PlayBGM(home_bgm);
                
            }
        }

        if (scene.name == "GameScene")
        {
            // 볼륨 슬라이더 초기화
            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 1f;
            volumeSlider.value = currentVolume;
        }
    }

    private void PlayBGM(AudioClip bgm)
    {
        
        _bgmSource.clip = bgm;
        _bgmSource.loop = true;
        _bgmSource.Play();
        
    }

    public void MuteButton() // 음소거 기능
    {
        _bgmSource.mute = true;
        volumeSlider.value = 0f; // 볼륨 슬라이더 값을 0으로 설정
    }

    public void MuteOffButton() // 음소거 해제 기능
    {
        _bgmSource.mute = false;
        volumeSlider.value = currentVolume; // 이전 볼륨 값으로 복원
    }

    public void AdjustVolume(float volume) // 음량 조절 기능
    {
        _bgmSource.volume = volume;
        currentVolume = volume; // 현재 볼륨 값 업데이트
    }

    public void PlayEffect(string effectName)
    {
        _effectSource.volume = 0.5f;

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
        _effectSource.volume = 0.5f;
        _effectSource.PlayOneShot(effect);
    }
}