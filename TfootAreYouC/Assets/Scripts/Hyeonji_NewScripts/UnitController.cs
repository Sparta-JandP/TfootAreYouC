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
        StartCoroutine(SpeedControl());
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

    IEnumerator SpeedControl()
    {
        while (true)
        {
            if (_enemyCount == 0)
            {
                if (_isFighting == true)
                {
                    speed = unitStats.speed;
                    _isFighting = false;
                }
            }
            else if (_enemyCount >= 1)
            {
                if (_isFighting == false)
                {
                    speed = 0;
                    _isFighting = true;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
