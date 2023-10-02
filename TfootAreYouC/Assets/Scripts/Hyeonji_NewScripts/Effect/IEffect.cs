using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    public event Action OnApplyingEffect;
    public void ApplyEffect(int power, float rate);
}
