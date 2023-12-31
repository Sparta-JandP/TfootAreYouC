using System;
using System.Collections;
using UnityEngine;

public class Defense : MonoBehaviour, IEffect
{
    public event Action OnDefenseStart;
    public event Action OnDefenseEnd;
    public event Action OnApplyingEffect;

    private UnitController _controller;

    [SerializeField] private LayerMask target;

    private int _enemyCount = 0;
    private bool _isDefending;

    private void Start()
    {
        _controller = GetComponent<UnitController>();
    }

    public void ApplyEffect(int power, float rate)
    {
        StartCoroutine(DefenseAction());
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

    IEnumerator DefenseAction()
    {
        while (true)
        {
            if (_enemyCount == 0)
            {
                if (_isDefending == true)
                {
                    OnDefenseEnd?.Invoke();  // Rook - 애니메이션에서 standing up 연결
                    _isDefending = false;
                }
            }
            else if (_enemyCount >= 1)
            {
                if (_isDefending == false)
                {
                    OnDefenseStart?.Invoke(); // Rook - 애니메이션에서 sitting 연결
                    _isDefending = true;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
