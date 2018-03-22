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
	private Wave wave;

	private float timer, attackCooldown;

	private void Awake () {

		regen = GameObject.Find("Player").GetComponent<Regeneration>();
		player = GameObject.Find("Player").GetComponent<PlayerStats>();
		wave = GameObject.Find("WaveManager").GetComponent<Wave>();
		
		attackCooldown = 2f;
	}
	private void Start () {

		if(!wave.spawnEnemies) {

			wave.ResetEnemies();
			Death();
		}
	}
	private void FixedUpdate () {

		Attack();
	}
	public void Attack () { //bug not touching player and player receives damage


		RaycastHit hit;

		Debug.DrawRay(transform.position, transform.forward, Color.green);
        if(Physics.Raycast(transform.position, transform.forward, out hit, attackRange, mask)) {

			if(timer <= 0f) {

				Debug.Log("Attack");
				//random range which attack anim
                
				
				regen.EnemyAttack();

				player.PlayerHealth(damage);
				timer = attackCooldown;
			}	
		}
		timer -= Time.deltaTime;
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
		wave.currEnemy--;
		Destroy(gameObject);
	}
}
