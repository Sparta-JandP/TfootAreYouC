using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAttack : MonoBehaviour, IEffect
{
    private string targetTag;
    private bool _isCollidingWithTarget = false;
    private HealthSystem _collidingTargetHealthSystem;

    public void ApplyEffect(int power, float rate)
    {
        if (tag == "Chess")
            targetTag = "Enemy";
        else
            targetTag = "Chess";
        StartCoroutine(ApplyHealthChange(power, rate));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject receiver = collision.gameObject;

        if (!receiver.CompareTag(targetTag))
        {
            return;
        }

        _collidingTargetHealthSystem = receiver.GetComponent<HealthSystem>();
        _isCollidingWithTarget = true;

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(targetTag))
        {
            return;
        }

        _isCollidingWithTarget = false;
    }

    IEnumerator ApplyHealthChange(int power, float rate)
    {
        while (true)
        {
            if (_isCollidingWithTarget)
            {
                yield return new WaitForSeconds(rate);
                _collidingTargetHealthSystem.ChangeHealth(-power);
            }
        }
    }

}
