using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrumpType
{
    Spade,
    Heart,
    Clover,
    Dia
    /*보스 카드 보류 
    Jack,
    Queen,
    King,
    Jocker
    */
}

public class Trump : MonoBehaviour
{
    public TrumpType type;

    public float speed;

    public int health;

    public int damage;

    public float range;

    public LayerMask chessMask;

    public float beatCooldown;

    private bool canBeat = true;

    public ChessUnit targetChessUnit;

    private void Start()
    {
       /* switch (type)
        {
            case TrumpType.Spade:
                speed = 1;
                health = 1;
                damage = 1;
                break;

            case TrumpType.Heart:
                speed = 1;
                health = 2;
                damage = 1;
                break;

            case TrumpType.Clover:
                speed = 1;
                health = 1;
                damage = 2;
                break;

            case TrumpType.Dia:
                speed = 1;
                health = 2;
                damage = 2;
                break;
        }*/
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, range, chessMask);

        if (hit.collider)
        {
            targetChessUnit = hit.collider.GetComponent<ChessUnit>();
            Beat();
        }
    }

    void Beat()
    {
        if (!canBeat || !targetChessUnit)
            return;
        canBeat = false;
        Invoke("ResetBeatCooldown", beatCooldown);

        targetChessUnit.Hit(damage);
    }

    void ResetBeatCooldown()
    {
        canBeat = true;
    }

    private void FixedUpdate()
    {
        if(!targetChessUnit)
        transform.position -= new Vector3(speed, 0, 0);
    }

    public void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
    }
}
