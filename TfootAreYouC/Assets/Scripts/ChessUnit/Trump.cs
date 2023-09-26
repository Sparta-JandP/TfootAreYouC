using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trump : MonoBehaviour
{
    public float speed;

    public int health;

    public int damage;

    public float range;

    public LayerMask chessMask;

    public float beatCooldown;

    private bool canBeat = true;

    public ChessUnit targetChessUnit;

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
