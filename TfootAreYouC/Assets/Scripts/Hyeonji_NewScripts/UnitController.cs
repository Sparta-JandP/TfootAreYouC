using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private UnitStats unitStats;

    [HideInInspector]
    public int maxHealth;
    [HideInInspector]
    public int effectPower;
    [HideInInspector]
    public float effectRate;
    [HideInInspector]
    public float speed;

    [SerializeField] private LayerMask target;

    private IEffect _effect;
    private HealthSystem _healthSystem;

    private int _enemyCount = 0;
    private bool _isFighting;

    private void Awake()
    {

        maxHealth = unitStats.maxHealth;
        effectPower = unitStats.effectPower;
        effectRate = unitStats.effectRate;
        speed = unitStats.speed;

        _effect = GetComponent<IEffect>();
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        _healthSystem.OnDeath += Die;
        if (_effect != null)
            _effect.ApplyEffect(effectPower, effectRate);
    }

    private void FixedUpdate()
    {
        if (tag != "Enemy")
            transform.position += new Vector3(speed, 0, 0);
        else
            transform.position -= new Vector3(speed, 0, 0);
    }

    void Die()
    {
        StartCoroutine(DestoryObjectOnDeath());
    }

    IEnumerator DestoryObjectOnDeath()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((target.value & (1 << other.gameObject.layer)) != 0)
        {
            _enemyCount++;
            if (_enemyCount == 1) // 첫번째 적과 접촉할 경우
            {
                speed = 0;
                _isFighting = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((target.value & (1 << other.gameObject.layer)) != 0)
        {
            _enemyCount--;
            if (_enemyCount == 0) // 모든 적과의 접촉이 끝날 경우
            {
                ResetSpeed();
                _isFighting = false;
            }
        }
    }


    public void ResetSpeed()
    {
        speed = unitStats.speed;
    }
}
