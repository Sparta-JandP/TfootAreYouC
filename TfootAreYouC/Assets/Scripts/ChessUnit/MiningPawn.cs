using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningPawn : MonoBehaviour
{
    public GameObject sandObject;

    public float cooldown;

    private void Start()
    {
        InvokeRepeating("SpawnSand", cooldown, cooldown);
    }
    void SpawnSand()
    {
        GameObject mySand = Instantiate(sandObject, new Vector3(transform.position.x + Random.Range(-0.1f, 0.1f), transform.position.y + Random.Range(-0.3f, -0.3f), 0), Quaternion.identity);
        mySand.GetComponent<SandResource>().dropToYPos = transform.position.y - 1;
    }
}
