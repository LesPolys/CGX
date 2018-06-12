using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent: Rigidbody2D]

public class TestEnemyWalk : MonoBehaviour {

	Rigidbody2D rb2d;
	float moveSpeed = 2f;

	void Awake(){
		rb2d = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate(){
		rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
	}
}
