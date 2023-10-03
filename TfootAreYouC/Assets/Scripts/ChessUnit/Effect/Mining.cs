using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour, IEffect
{
    public GameObject sandObject;
    public event Action OnApplyingEffect;



    public void ApplyEffect(int power, float rate)
    {
        StartCoroutine(SpawnSand(rate));
    }


    IEnumerator SpawnSand(float rate)
    {
        while (true)
        {
            yield return new WaitForSeconds(rate);
            GameObject mySand = Instantiate(sandObject, new Vector3(transform.position.x + UnityEngine.Random.Range(-0.1f, 0.1f), transform.position.y + UnityEngine.Random.Range(-0.3f, -0.3f), 0), Quaternion.identity);
            mySand.GetComponent<SandResource>().dropToYPos = transform.position.y - 1;
            StageManager.instance.StageObjects.Add(mySand);
        }
    }
}
