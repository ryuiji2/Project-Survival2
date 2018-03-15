using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

	[Range(100f,0f)]
	public float health = 100f;
	public float damage = 10f;

	private PlayerStats player;

	public float attackRange;
	public LayerMask mask;

	private Regeneration regen;

	private float timer;

	private void Awake () {

		regen = GameObject.Find("Player").GetComponent<Regeneration>();
		player = GameObject.Find("Player").GetComponent<PlayerStats>();
	}
	private void FixedUpdate () {

		Attack();
	}
	public void Attack () {


		RaycastHit hit;

		Debug.DrawRay(transform.position, transform.forward, Color.green);
        if(Physics.Raycast(transform.position, transform.forward, out hit, attackRange, mask)) {

			//timer stuff
			//start timer
			if(timer > 0f)
			{
				
			}
			
				Debug.Log("Attack");
				//random range which attack anim
				regen.beenHit = true;
				regen.Regenerating();

				player.PlayerHealth(damage);
		}
	}
	public void EnemyHealth (float dmg) {

		health -= dmg;
		Debug.Log(health);
		if(health <= 0f)
		{
			Debug.Log("dead");
			Death();
		}
		//stagger?
	}
	public void Death() {

		// Die
		//play animation
		Destroy(gameObject);
	}
}
