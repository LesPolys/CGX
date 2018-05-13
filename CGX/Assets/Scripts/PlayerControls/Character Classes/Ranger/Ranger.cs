using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ranger : PlayerClass
{

    public GameObject arrowPrefab;
    public int numArrows;

    public float attackRange;


    Ranger()
    {
        actionKey = KeyCode.E;
    }

    public override void Ability()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyMask);
        if (colliders.Length > 0)
        {
            Shoot(colliders[colliders.Length-1].transform);
        }

    }


    void Shoot(Transform target)
    {
        // Create the Bullet from the Bullet Prefab
        var arrow = (GameObject)Instantiate(
            arrowPrefab,
            transform.position,
            Quaternion.identity);

        // Add velocity to the bullet
        //arrow.GetComponent<Rigidbody>().velocity = arrow.transform.forward * 6;
        arrow.GetComponent<Arrow>().Target(target);

        // Destroy the bullet after 2 seconds
        Destroy(arrow, 2.0f);
    }

}