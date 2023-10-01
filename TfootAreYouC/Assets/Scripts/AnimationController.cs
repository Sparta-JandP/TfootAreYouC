using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    // basic: idle / die
    // effect: + effect
    // rook + effect / sitting / standing-up

    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int IsOnDefenseStart = Animator.StringToHash("IsOnDefenseStart");
    private static readonly int IsOnDefenseEnd = Animator.StringToHash("IsOnDefenseEnd");
    private static readonly int IsHealing = Animator.StringToHash("IsHealing");

    private Animator animator;

    private void Awake()
    { 
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gameObject.GetComponent<HealthSystem>().OnDeath += OnDie;
        if(gameObject.TryGetComponent<Defense>(out Defense defense))
        {
            defense.OnDefenseStart += OnDefenseStart;
            defense.OnDefenseEnd += OnDefenseEnd;
        }
        if(gameObject.TryGetComponent<Heal>(out Heal heal))
        {
            heal.OnHeal += OnHeal;
        }
    }

    private void OnDie()
    {
        animator.SetTrigger(IsDead);
    }

    private void OnDefenseStart()
    {
        animator.SetTrigger(IsOnDefenseStart);
    }

    private void OnDefenseEnd()
    {
        animator.SetTrigger(IsOnDefenseEnd);
    }

    private void OnHeal()
    {
        animator.SetTrigger(IsHealing);
    }
}
