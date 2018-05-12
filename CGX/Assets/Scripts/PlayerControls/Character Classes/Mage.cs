using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Mage : PlayerClass
{
    float abilityRange;

    Mage()
    {
        actionKey = KeyCode.W;
    }

    public override void Ability()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, abilityRange, enemyMask);
        if (colliders.Length > 0)
        {

        }
    }
}