using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ContactAttack : MonoBehaviour, IEffect
{
    [SerializeField] private LayerMask target;

    private bool _isCollidingWithTarget = false;
    private HealthSystem _collidingTargetHealthSystem;
    private UnitController _controller;
    private int _enemyCount = 0;

    // tag를 LayerMask 로 바꾸는데, LayerMask를 public 으로 설정을 하면 Script 가 붙은 프리팹의 인스펙터에서 이 Layer Mask를 지정할 수 있는데,
    // 아래에서 조건문을 판별하지 않고, 상대의 layer mask 를 지정해서

    private void Start()
    {
        _controller = GetComponent<UnitController>();
    }

    public void ApplyEffect(int power, float rate)
    {
     
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((target.value == (target.value | (1 << other.gameObject.layer))))
        {
            _enemyCount++;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((target.value == (target.value | (1 << other.gameObject.layer))))
        {
            _enemyCount--;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
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
    */

    IEnumerator ApplyHealthChange(int power, float rate)
    {
        while (true)
        {
            if (_isCollidingWithTarget)
            {
                yield return new WaitForSeconds(rate);
                _collidingTargetHealthSystem.ChangeHealth(-power);
                Debug.Log(_collidingTargetHealthSystem.CurrentHealth);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

}
