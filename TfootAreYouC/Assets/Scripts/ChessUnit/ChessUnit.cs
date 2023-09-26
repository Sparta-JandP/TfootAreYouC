using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessUnit : MonoBehaviour
{
    /*
    public string unitName; // 유닛 이름
    public int health;  // 유닛 체력
    public int damage;  // 유닛 공격력
    public float speed; // 유닛 이동 속도

    // 유닛 초기화
    public void Initialize(string name, int initialHealth, int initialDamage, float initialSpeed)
    {
        unitName = name;
        health = initialHealth;
        damage = initialDamage;
        speed = initialSpeed;
    }
    */

    // 유닛 이동
    public float speed;

    private void FixedUpdate()
    {
        // 이동 로직 구현
        transform.position += new Vector3(speed, 0, 0);
    }

    public int health;

    private void Start()
    {
        gameObject.layer = 7;
    }

    //유닛 공격
    public void Hit(int damage)
    {
        // 공격 로직 구현
        health -= damage;
        if(health <= 0)
            Destroy(gameObject);
    }

    
}
