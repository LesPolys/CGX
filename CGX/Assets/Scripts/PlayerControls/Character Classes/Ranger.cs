using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ranger : PlayerClass
{

    float attackRange;


    Ranger()
    {
        actionKey = KeyCode.E;
    }

    public override void Ability()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyMask);
        if (colliders.Length > 0)
        {

        }

    }

}


