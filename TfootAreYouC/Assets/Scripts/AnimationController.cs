using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    // basic: idle / die
    // effect: + effect
    // rook + effect / sitting / standing-up

    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private Animator animator;

    private void Awake()
    { 
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gameObject.GetComponent<HealthSystem>().OnDeath += OnDie;
    }

    private void OnDie()
    {
        animator.SetTrigger(IsDead);
    }
}
