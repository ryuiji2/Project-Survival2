using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

	[Range(100f,0f)]
	public float health = 100f;
	public float damage = 10f;

	public void Attack() {
		// Do damage
	}

	public void Death() {
		// Die
		Destroy(gameObject);
	}
}
