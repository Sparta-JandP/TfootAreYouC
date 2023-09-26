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
       
    }

    private void Update()
    {
        
    }

    public void Mining() //버튼 클릭 시
    {
        Mineral += Mine;
        Destroy(gameObject);
       
    }

    public void AllyKingHealth() //내 왕의 체력
    {
        KingHealth = currentStage * 2 + 5;
    }

    public void EnemyBossHealth() //적 보스의 체력
    {
        BossHealth = currentStage ^ 2 + 10;
    }
}