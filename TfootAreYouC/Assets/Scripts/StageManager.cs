using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public int mineral; //현재 자원
    public TMP_Text mineralText; // TMP Text 변수
    public int mine; //일꾼 유닛이 채굴한 자원
    public int maxMineral; //최대 자원
    public int kingHealth; //왕의 체력
    public int bossHealth; //적 보스의 체력
    public int maxKingHealth; //최대 왕의 체력
    public int maxBossHealth; //최대 보스 체력
    public int currentStage; //현재 스테이지

    public event Action OnStageClear;
    public event Action OnKingHealthChange;
    public event Action OnBossHealthChange;
    public event Action OnMining;

    private void Awake()
    {
        // 이미 인스턴스가 있는지 확인하고, 없으면 현재 스크립트를 인스턴스로 설정
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject); // 중복된 인스턴스 제거
            return;
        }

        // 인스턴스를 파괴하지 않음
        DontDestroyOnLoad(gameObject);

        maxKingHealth = 100; //왕의 최대 체력
        maxBossHealth = (int)Math.Pow(5, currentStage) + 100; //보스의 최대 체력, 스테이지에 따라 다름.
    }

    private void Start()
    {
        kingHealth = maxKingHealth;
        bossHealth = maxBossHealth;
    }

    public void Mining() //채굴 유닛의 버튼 클릭 시
    {
        mineral += mine; //채굴량 설정값 만큼 자원 추가
        Destroy(gameObject); //아이콘 삭제
        maxMineral = 400; //최대 자원값
        mineral = Mathf.Clamp(mineral, 0, maxMineral); //자원 최대 최소값 설정

        // TMP Text 초기화
        if (mineralText != null)
        {
            mineralText.text = mineral.ToString();
        }
        OnMining?.Invoke();
    }
    

    public void AllyKingHealth() //내 왕의 체력
    {
        kingHealth = maxKingHealth;
        if(kingHealth <= 0)
        {
            OnDefeat(); //패배 출력
        }
        OnKingHealthChange?.Invoke();
    }

    public void EnemyBossHealth() //적 보스의 체력
    {
        bossHealth = maxBossHealth;
        if(bossHealth <= 0)
        {
            OnVictory(); //승리 출력
        }
        OnBossHealthChange?.Invoke();
    }

    public void OnDefeat()
    {
        //패배 결과 출력
    }

    public void OnVictory()
    {
        //승리 결과 출력
        if(currentStage < 4)
        {

        }
    }
}