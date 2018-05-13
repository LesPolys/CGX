using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Mage : PlayerClass
{
    float abilityRange;

    [SerializeField]
    protected LayerMask partyMask = 0;

    Mage()
    {
        actionKey = KeyCode.W;
    }

    public override void Ability()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, abilityRange, partyMask);
        if (colliders.Length > 0)
        {

        }
    }
}