using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour, IEffect
{
    public void ApplyEffect(int power, float rate)
    {
        StartCoroutine(HealAction(power, rate));
    }

    IEnumerator HealAction(int power, float rate)
    {
        
        yield return new WaitForSeconds(rate);
    }
}
