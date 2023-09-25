using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Spade,
    Heart,
    Clover,
    Dia,
    /*보스 카드 보류 
    Jack,
    Qeen,
    King,
    Jocker
    */
}

public class Enemy : MonoBehaviour
{

    public EnemyType type;
    public int Defensive;
    public int Health;
    public int Speed;
    public int Attack;

    void Start()
    {
        switch (type)
        {
            case EnemyType.Spade:
                Defensive = 1;
                Health = 1;
                Speed = 1;
                Attack = 1;
                break;

            case EnemyType.Heart:
                Defensive = 1;
                Health = 1;
                Speed = 1;
                Attack = 2;
                break;

            case EnemyType.Clover:
                Defensive = 2;
                Health = 2;
                Speed = 1;
                Attack = 2;
                break;

            case EnemyType.Dia:
                Defensive = 2;
                Health = 3;
                Speed = 2;
                Attack = 3;
                break;

        }
    }
}
