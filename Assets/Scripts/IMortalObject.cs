using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMortalObject
{
    bool Dead { get; set; }

    void Damage(int value);

    void Heal(int value);

    void Die();
}
