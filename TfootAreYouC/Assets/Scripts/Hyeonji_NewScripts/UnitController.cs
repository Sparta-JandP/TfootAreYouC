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

    private IEffect effect;

    private void Awake()
    {

        maxHealth = unitStats.maxHealth;
        effectPower = unitStats.effectPower;
        effectRate = unitStats.effectRate;

        effect = GetComponent<IEffect>();
    }

    private void Start()
    {
        if (effect != null)
            effect.ApplyEffect(effectPower, effectRate);
    }

    private void FixedUpdate()
    {
        if (tag != "Enemy")
            transform.position += new Vector3(unitStats.speed, 0, 0);
        else
            transform.position -= new Vector3(unitStats.speed, 0, 0);
    }
}
