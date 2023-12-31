using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private AudioClip ranged;
    [SerializeField] private AudioClip contact;
    [SerializeField] private AudioClip heal;
    [SerializeField] private AudioClip mine;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip die;



    public Button muteButton;
    public Button muteOffButton;
    public Slider volumeSlider;
    

    private float currentVolume = 0.2f;  // 현재 음량
    private float previousVolume;   // Mute 전 음량
    private bool IsOnMute = false;  //Mute일 때, 아닐 때에만 각각의 버튼이 작동하도록



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

        StartCoroutine(ResetSoundSettingComponents());
    }

    private void PlayBGM(AudioClip bgm)
    {
        
        _bgmSource.clip = bgm;
        _bgmSource.loop = true;
        _bgmSource.Play();
        
    }

    public void ReduceBGMVolume()
    {
        _bgmSource.volume = currentVolume * 0.5f;
    }

    public void ResetBGMVolume()
    {
        _bgmSource.volume = currentVolume;
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
                _effectSource.volume = 0.7f;
                _effectSource.PlayOneShot(option);
                break;
            case "ranged":
                _effectSource.volume = 0.5f;
                _effectSource.PlayOneShot(ranged);
                break;

            case "contact":
                _effectSource.volume = 1f;
                _effectSource.PlayOneShot(contact);
                break;

            case "heal":
                _effectSource.volume = 1f;
                _effectSource.PlayOneShot(heal);
                break;
            case "mine":
                _effectSource.volume = 0.7f;
                _effectSource.PlayOneShot(mine);
                break;
            case "hit":
                _effectSource.volume = 1f;
                _effectSource.PlayOneShot(hit);
                break;

            case "die":
                _effectSource.volume = 1f;
                _effectSource.PlayOneShot(die);
                break;
        }
    }

    public void PlayEffect(AudioClip effect)
    {

        _effectSource.volume = 0.5f;
        _effectSource.PlayOneShot(effect);
    }

    IEnumerator ResetSoundSettingComponents()
    {
        yield return new WaitForSeconds(0.2f);
        if (muteOffButton == null || muteButton == null || volumeSlider == null)
            yield break;
        muteButton.onClick.AddListener(MuteButton);
        muteOffButton.onClick.AddListener(MuteOffButton);
        volumeSlider.onValueChanged.AddListener(AdjustVolume);

        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 1f;
        volumeSlider.value = currentVolume;
    }

    void MuteButton() // 음소거 기능
    {
        if (!IsOnMute)
        {
            _bgmSource.mute = true;
            previousVolume = currentVolume;
            volumeSlider.value = 0f; // 볼륨 슬라이더 값을 0으로 설정
            IsOnMute = true;
        }
    }

    void MuteOffButton() // 음소거 해제 기능
    {
        if (IsOnMute)
        {
            currentVolume = previousVolume;
            _bgmSource.mute = false;
            volumeSlider.value = currentVolume; // 이전 볼륨 값으로 복원
            IsOnMute = false;
        }
    }

    public void AdjustVolume(float volume) // 음량 조절 기능
    {
        _bgmSource.volume = volume;
        currentVolume = volume; // 현재 볼륨 값 업데이트
    }

}