﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour {
    //animator toevoegen
    //deathzone tag aanmaken
    //deathzone aanmaken
    //random range voor attack

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
    private Animator anim;
    private UIManager UIM;
    private int deathScore;
    public GameObject ammoBox;
    public int maxRNG;
    private int toDrop;
    private bool death;

	private float timer, attackCooldown;

	private void Awake () {

		regen = GameObject.Find("Player").GetComponent<Regeneration>();
		player = GameObject.Find("Player").GetComponent<PlayerStats>();
		wave = GameObject.Find("WaveManager").GetComponent<Wave>();
        UIM = GameObject.Find ("Canvas").GetComponent<UIManager> ();
        agent = GetComponent<NavMeshAgent> ();
        anim = GetComponent<Animator> ();
		
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
            //Instantiate (ammoBox, transform.position, Quaternion.identity);
        }
    }
	public void Attack () { //bug not touching player and player receives damage


		RaycastHit hit;

		Debug.DrawRay(new Vector3(transform.position.x,transform.position.y+1,transform.position.z), transform.forward, Color.green);
        if(Physics.Raycast(new Vector3(transform.position.x,transform.position.y+1,transform.position.z), transform.forward, out hit, attackRange, mask)) {
            anim.SetBool ("InRange", true);
			if(timer <= 0f && health > 0) {

                agent.isStopped = true;
				Debug.Log("Attack");
				//random range which attack anim
                
				
				regen.EnemyAttack();

				player.PlayerHealth(damage);
				timer = attackCooldown;
                
			}	
		}/* 
        if (!anim.IsPlaying ("animationname")) {
            agent.isStopped = false;
        }*/
        timer -= Time.deltaTime;
    }
	public void EnemyHealth (float dmg) {


        if(health <= 0f) {
            GetComponentInChildren<Collider> ().isTrigger = true;
            agent.enabled = false;
            AmmoDrop ();
            wave.currEnemy--; //i think this fucks with wave
        }
            health -= dmg;
		//stagger?
	}
	public void Death() {

        if (health <= 0f) {
            deathTimer -= Time.deltaTime;

            if (death == false) {
                anim.SetTrigger ("Death");
                anim.SetBool ("InRange", false);
                anim.SetBool ("Attack", false);
                death = true;
            }
            //UIM.checkscore (deathScore); > voor score
        }
        if (deathTimer <= 0f) {
            gameObject.transform.position += (Vector3.down * Time.deltaTime);
        }
	}
}