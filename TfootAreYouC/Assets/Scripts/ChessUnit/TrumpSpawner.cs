using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpSpawner : MonoBehaviour
{
    public Transform[] spawnpoints;

    public GameObject trump;

    private void Start()
    {
        InvokeRepeating("SpawnTrump", 2, 5);
    }
    void SpawnTrump()
    {
        int r = Random.Range(0, spawnpoints.Length);
        GameObject myTrump = Instantiate(trump, spawnpoints[r].position, Quaternion.identity);
    }

}
