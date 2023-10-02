using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour, IEffect
{
    [SerializeField] private LayerMask healLayerMask;

    private UnitController _controller;

    public event Action OnApplyingEffect;

    private void Awake()
    {
        _controller = GetComponent<UnitController>();
        _controller.ResetSpeed();
    }

    public void ApplyEffect(int power, float rate)
    {
        StartCoroutine(HealAction(power, rate));
    }

    IEnumerator HealAction(int power, float rate)
    {
        while (true)
        {
            yield return new WaitForSeconds(rate);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.7f, healLayerMask); // 모든 Collider2D를 배열로 반환
            Debug.Log(colliders.Length);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject == this.gameObject) // 현재 오브젝트와 같다면
                {
                    continue; // 이번 반복을 건너뛰고 다음 반복으로 넘어간다.
                }
                if (collider != null && (collider.transform.position.y == this.transform.position.y))
                {
                    _controller.speed = 0;
                    OnApplyingEffect?.Invoke();
                    collider.gameObject.GetComponent<HealthSystem>().ChangeHealth(power);
                    yield return new WaitForSeconds(1.2f);
                    _controller.ResetSpeed();
                }
            }
        }
    }
}
