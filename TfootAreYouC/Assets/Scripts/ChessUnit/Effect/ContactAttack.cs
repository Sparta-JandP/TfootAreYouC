using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ContactAttack : MonoBehaviour, IEffect
{
    [SerializeField] private LayerMask target;

    public event Action OnApplyingEffect;

    private int _enemyCount = 0;

    private List<GameObject> _enemyList = new List<GameObject>();

    // tag를 LayerMask 로 바꾸는데, LayerMask를 public 으로 설정을 하면 Script 가 붙은 프리팹의 인스펙터에서 이 Layer Mask를 지정할 수 있는데,
    // 아래에서 조건문을 판별하지 않고, 상대의 layer mask 를 지정해서

    public void ApplyEffect(int power, float rate)
    {
        StartCoroutine(ApplyHealthChange(power, rate));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((target.value == (target.value | (1 << other.gameObject.layer))))
        {
            _enemyList.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((target.value == (target.value | (1 << other.gameObject.layer))))
        {
            _enemyList.Remove(other.gameObject);
        }
    }


    IEnumerator ApplyHealthChange(int power, float rate)
    {
        while (true)
        {
            if (_enemyList.Count > 0)
            {
                yield return new WaitForSeconds(rate);
                foreach (var enemy in _enemyList)
                {
                    enemy.GetComponent<HealthSystem>().ChangeHealth(-power);
                    OnApplyingEffect?.Invoke();
                }
                SoundManager.instance.PlayEffect("contact");
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

}
