using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject enemy; // 적 유닛 프리팹, 여러개로 분리
    public Transform[] spawnPoints; // 1~5번 라인 중에서 적 유닛이 생성될 위치
    public int currentRound = 1;
    public int[] enemyCountByRound = { 20, 50, 100, 400 }; // 각 라운드마다의 적 유닛 수 배열

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartRound(int round) //시작하기, 계속하기 누르면 작동, OnClickButton
    {
        int enemyAmount = enemyCountByRound[round - 1]; //라운드에 따라 소환되는 횟수 반복
        for (int i = 0; i < enemyAmount; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 적 유닛 생성
        GameObject Enemy = Instantiate(enemy, spawnPoint.position, Quaternion.identity);

        //적 유닛 종류에 따라 능력치 다르게 하는 것과 연결
    }


}
