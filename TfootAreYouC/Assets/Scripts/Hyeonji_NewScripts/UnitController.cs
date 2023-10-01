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

    private IEffect _effect;
    private HealthSystem _healthSystem;

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
        yield return new WaitForSeconds(1.3f);
        Destroy(gameObject);
    }
}
