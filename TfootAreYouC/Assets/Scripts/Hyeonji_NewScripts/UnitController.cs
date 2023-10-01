using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitController : MonoBehaviour
{
    [SerializeField] private UnitStats unitStats;
    [SerializeField] private LayerMask target;
    [SerializeField] private Tilemap map;

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
    private bool _isFighting;

    private int start;
    private int end;
    private Vector3 startXPos;
    private Vector3 endXPos;


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
        map = StageManager.instance.board;

        start = map.cellBounds.min.x;
        end = map.cellBounds.max.x - 1; 

        Vector3Int startPosition = new Vector3Int(start, 0, 0);
        startXPos = map.GetCellCenterWorld(startPosition);

        Vector3Int endPosition = new Vector3Int(end, 0, 0);
        endXPos = map.GetCellCenterWorld(endPosition);


        _healthSystem.OnDeath += Die;
        if (_effect != null)
            _effect.ApplyEffect(effectPower, effectRate);
    }

    private void FixedUpdate()
    {
        if (tag != "Enemy")
        {
            transform.position += new Vector3(speed, 0, 0);
            if (transform.position.x >= endXPos.x + 0.5f)
            {
                Destroy(gameObject);
                //TODO: Boss한테 데미지
            }
        }
            
        else
        {
            transform.position -= new Vector3(speed, 0, 0);
            if (transform.position.x <= startXPos.x - 0.5f)
            {
                Destroy(gameObject);
                //TODO: Boss한테 데미지
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
