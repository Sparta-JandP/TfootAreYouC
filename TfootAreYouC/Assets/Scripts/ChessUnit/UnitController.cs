using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitController : MonoBehaviour
{
    [SerializeField] private UnitStats unitStats;
    [SerializeField] private LayerMask target;

    int chessLayer;
    int trumpLayer;

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

    private int _enemyCount = 0;

    private Vector3 startXPos;
    private Vector3 endXPos;

    public static event Action<GameObject> OnDestroyed = delegate { };


    private void Awake()
    {
        chessLayer = LayerMask.NameToLayer("ChessTarget");
        trumpLayer = LayerMask.NameToLayer("Target");

        maxHealth = unitStats.maxHealth;
        effectPower = unitStats.effectPower;
        effectRate = unitStats.effectRate;
        speed = unitStats.speed;

        _effect = GetComponent<IEffect>();
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {

        startXPos = StageManager.instance.startXPos;
        endXPos = StageManager.instance.endXPos;
        _healthSystem.OnDeath += Die;
        if (_effect != null)
            _effect.ApplyEffect(effectPower, effectRate);
    }

    private void FixedUpdate()
    {
        if ((target.value & (1 << trumpLayer)) != 0)
        {
            transform.position += new Vector3(speed, 0, 0);
            if (transform.position.x >= endXPos.x + 0.5f)
            {
                StageManager.instance.DamageBoss(10);
                Destroy(gameObject);
            }
        }
            
        else if ((target.value & (1 << chessLayer)) != 0)
        {
            transform.position -= new Vector3(speed, 0, 0);
            if (transform.position.x <= startXPos.x - 0.5f)
            {
                StageManager.instance.DamageKing(10);
                Destroy(gameObject);
            }
        }
            
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
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if ((target.value & (1 << other.gameObject.layer)) != 0)
        {
            speed = 0;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((target.value & (1 << other.gameObject.layer)) != 0)
        {
            _enemyCount--;
            ResetSpeed();

        }
    }


    public void ResetSpeed()
    {
        speed = unitStats.speed;
    }

    private void OnDestroy()
    {
        OnDestroyed(gameObject);
    }
}
