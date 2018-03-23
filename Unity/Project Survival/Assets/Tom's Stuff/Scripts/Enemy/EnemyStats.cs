﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour {
    //animator toevoegen
	[Range(100f,0f)]
	public float health = 100f;
	public float damage = 10f;
    public float deathTimer;

	private PlayerStats player;

	public float attackRange;
	public LayerMask mask;

	private Regeneration regen;
	private Wave wave;
    private NavMeshAgent agent;
    private Animation anim;
    private UIManager UIM;
    private int deathScore;
    public GameObject ammoBox;
    public int maxRNG;
    private int toDrop;

	private float timer, attackCooldown;

	private void Awake () {

		regen = GameObject.Find("Player").GetComponent<Regeneration>();
		player = GameObject.Find("Player").GetComponent<PlayerStats>();
		wave = GameObject.Find("WaveManager").GetComponent<Wave>();
        UIM = GameObject.Find ("Canvas").GetComponent<UIManager> ();
        agent = GetComponent<NavMeshAgent> ();
        anim = GetComponent<Animation> ();
		
		attackCooldown = 2f;
	}
	private void Start () {
        toDrop = Random.Range (0, maxRNG);

		if(!wave.spawnEnemies) {

			wave.ResetEnemies();
			Death();
		}
	}
	public void FixedUpdate () {

        Death ();
        Attack ();

    }

    public void AmmoDrop () {

        if(toDrop == 0) {
            Instantiate (ammoBox, transform.position, Quaternion.identity);
        }
    }
	public void Attack () { //bug not touching player and player receives damage


		RaycastHit hit;

		Debug.DrawRay(transform.position, transform.forward, Color.green);
        if(Physics.Raycast(transform.position, transform.forward, out hit, attackRange, mask)) {

			if(timer <= 0f && health > 0) {

                agent.isStopped = true;
				Debug.Log("Attack");
				//random range which attack anim
                
				
				regen.EnemyAttack();

				player.PlayerHealth(damage);
				timer = attackCooldown;
                
			}	
		}
        if (!anim.IsPlaying ("animationname")) { //change animation name to attack name.
            agent.isStopped = false;
        }
        timer -= Time.deltaTime;
    }
	public void EnemyHealth (float dmg) {


        if(health <= 0f) {
            agent.enabled = false;
            GetComponent<Collider> ().isTrigger = true;
            AmmoDrop ();
            wave.currEnemy--;
        }
            health -= dmg;
		//stagger?
	}
	public void Death() {

        if (health <= 0f) {
            deathTimer -= Time.deltaTime;

            //play animation
            //UIM.checkscore (deathScore);
        }
        if (deathTimer <= 0f) {
            Debug.Log ("go down");
            gameObject.transform.position += (Vector3.down * Time.deltaTime);
        }
	}

    public void OnTriggerEnter (Collider deathZone) {
        if (deathZone.gameObject.tag == "deathZone") { //deathzone tag aanmaken.
            Destroy (this.gameObject);
        }
    }
}