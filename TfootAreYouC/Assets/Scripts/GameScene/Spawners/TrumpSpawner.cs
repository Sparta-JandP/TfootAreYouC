using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrumpSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap Tilemap;

    [HideInInspector]
    public Vector3[] spawnPoints;

    public GameObject[] trumps = new GameObject[4]; 

    private StageManager stageManager;
    private int stageNum;
    private float stageRate;

    private void Start()
    {
        SetSpawnPoint();

        stageManager = StageManager.instance;

        //TODO: StartSpawning과 UpdateStage --> 이벤트에 연결하기
        UpdateStage();
        StartSpawning();
    }

    void SetSpawnPoint()    //타일맵의 좌표를 활용해서 마지막 타일맵의 위치값 받아오기
    {
        int start = Tilemap.cellBounds.min.y;   // TileMap.cellBounds.min : 최소 좌표
        int width = Tilemap.cellBounds.max.x;   // TileMap.cellBounds.max : 최대 좌표
        int height = Tilemap.cellBounds.size.y; // TileMap.cellBounds.size : 길이 (개수)

        spawnPoints = new Vector3[height];

        for (int i = 0; i < height; i++)
        {
            Vector3Int cellPosition = new Vector3Int(width - 1, start + i, 0);
            Vector3 worldPosition = Tilemap.GetCellCenterWorld(cellPosition);
            spawnPoints[i] = worldPosition;
        }
       
    }
    
    void StartSpawning()
    {
        StartCoroutine("RepeateSpawnTrump");
    }

    void UpdateStage()  //스테이지 난이도가 반영될 수 있도록 & 스테이지가 종료될 때 Spawning이 멈출 수 있도록 (이벤트 연결 필요)
    {
        StopCoroutine("RepeateSpawnTrump");
        stageNum = stageManager.currentStage;
        stageRate = 5f / stageNum; 
    }

    IEnumerator RepeateSpawnTrump()
    {
        while (true)
        {
            yield return new WaitForSeconds(stageRate);
            SpawnTrump();
        }
    }
    void SpawnTrump()
    {
        int idx = Random.Range(0, 4);
        int r = Random.Range(0, spawnPoints.Length);
        GameObject myTrump = Instantiate(trumps[idx], spawnPoints[r], Quaternion.identity);
        stageManager.StageObjects.Add(myTrump);
    }

}
