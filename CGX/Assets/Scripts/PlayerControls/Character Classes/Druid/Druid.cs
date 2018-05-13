using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Druid : PlayerClass
{

    public float attackRange;

    Druid()
    {
        actionKey = KeyCode.Q;
    }


    public override void Ability()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyMask);
        if (colliders.Length > 0)
        {

        }

    }


}