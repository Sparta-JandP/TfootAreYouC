using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpSpawner : MonoBehaviour
{
    public Transform[] spawnpoints;

    public GameObject[] trumps = new GameObject[4];

    private void Start()
    {
        InvokeRepeating("SpawnTrump", 2, 5);
    }
    void SpawnTrump()
    {
        int idx = Random.Range(0, 4);
        int r = Random.Range(0, spawnpoints.Length);
        GameObject myTrump = Instantiate(trumps[idx], spawnpoints[r].position, Quaternion.identity);
    }

}
