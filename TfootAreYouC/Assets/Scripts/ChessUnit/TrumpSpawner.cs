using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrumpSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap Tilemap;

    public Vector3[] spawnPoints;

    public GameObject[] trumps = new GameObject[4];

    private StageManager stageManager;
    private int stageNum;
    private int stageRate;

    private void Start()
    {
        SetSpawnPoint();

        stageManager = StageManager.instance;

        //TODO: StartSpawning과 UpdateStage 이벤트에 연결하기
        UpdateStage();
        StartSpawning();
    }

    void SetSpawnPoint()
    {
        int start = Tilemap.cellBounds.min.y;
        int width = Tilemap.cellBounds.max.x;
        int height = Tilemap.cellBounds.size.y;

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

    void UpdateStage() 
    {
        StopCoroutine("RepeateSpawnTrump");
        stageNum = stageManager.currentStage;
        stageRate = 5 / stageNum; 
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
    }

}
