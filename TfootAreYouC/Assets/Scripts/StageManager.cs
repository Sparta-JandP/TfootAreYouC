using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public float Mineral; //현재 자원
    public float Mine; //일꾼 유닛이 채굴한 자원
    public int KingHealth; //왕의 체력
    public int BossHealth; //적 보스의 체력
    public int currentStage; //현재 스테이지

    private void Start()
    {
        Mineral = Mathf.Clamp(Mineral, 0, 400); //자원 최대 최소값 설정
    }

    private void Update()
    {
        
    }

    public void Mining() //채굴 유닛의 버튼 클릭 시
    {
        Mineral += Mine; //채굴량 설정값 만큼 자원 추가
        Destroy(gameObject); //아이콘 삭제
    }

    public void AllyKingHealth() //내 왕의 체력
    {
        KingHealth = 100;
        if(KingHealth < 0)
        {
            Defeat();
        }
    }

    public void EnemyBossHealth() //적 보스의 체력
    {
        BossHealth = 5 ^ currentStage + 100;
        if(BossHealth < 0)
        {
            Victory();
        }
    }

    public void Defeat()
    {
        //패배 결과 출력
    }

    public void Victory()
    {
        //승리 결과 출력
    }
}