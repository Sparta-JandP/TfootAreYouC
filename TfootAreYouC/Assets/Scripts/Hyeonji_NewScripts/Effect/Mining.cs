using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour, IEffect
{
    public GameObject sandObject;

    
    public void ApplyEffect(int power, float rate)
    {
        StartCoroutine(SpawnSand(rate));
    }


    IEnumerator SpawnSand(float rate)
    {
        while (true)
        {
            yield return new WaitForSeconds(rate);
            GameObject mySand = Instantiate(sandObject, new Vector3(transform.position.x + Random.Range(-0.1f, 0.1f), transform.position.y + Random.Range(-0.3f, -0.3f), 0), Quaternion.identity);
            mySand.GetComponent<SandResource>().dropToYPos = transform.position.y - 1;          
        }
    }
}
