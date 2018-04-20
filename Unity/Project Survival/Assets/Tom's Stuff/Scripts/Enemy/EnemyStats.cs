using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyStats : MonoBehaviour {

    //random range voor attack

	[Range(100f,0f)]

	public float health = 100f;
	public float damage = 10f;
    public float deathTimer;

	private PlayerStats player;
    public GameObject playerModel;
	public float attackRange;
	public LayerMask mask;

	private Regeneration regen;
	private Wave wave;
    private NavMeshAgent agent;
    private Animator anim;
    private UIManager uim;
    private int deathScore;
    public GameObject ammoBox;
    public int maxRNG;
    private int toDrop;
    private bool death;
    public int whichAttack;
	private float timer, attackCooldown;

    public int deathPoints;

    public Animation scoreAnim;

    public bool oneTime;

    private bool canTakeDmg = true;
    public List<Collider> gates = new List<Collider> ();


    private void Awake () {

        scoreAnim = GameObject.Find("Plus Score").GetComponent<Animation>();
		regen = GameObject.Find("Player").GetComponent<Regeneration>();
		player = GameObject.Find("Player").GetComponent<PlayerStats>();
		wave = GameObject.Find("WaveManager").GetComponent<Wave>();
        uim = GameObject.Find ("Canvas").GetComponent<UIManager> ();
        agent = GetComponent<NavMeshAgent> ();
        anim = GetComponent<Animator> ();
		
		attackCooldown = 2f;
	}
	private void Start () {

        scoreAnim = GameObject.Find("Plus Score").GetComponent<Animation>();
        oneTime = false;
        toDrop = Random.Range (0, maxRNG);

		if(!wave.spawnEnemies) {

			wave.ResetEnemies();
			Death();
		}

        foreach (Collider c in gates) {
            Physics.IgnoreCollision (c, gameObject.GetComponentInChildren<Collider> ());
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

		Debug.DrawRay(new Vector3(transform.position.x,transform.position.y+1,transform.position.z), transform.forward * attackRange, Color.green);
        if (Physics.Raycast (new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z), transform.forward, out hit, attackRange, mask)) {
            anim.SetBool ("InRange", true);
            if (timer <= 0f && health > 0) {
                agent.isStopped = true;

                whichAttack = Random.Range (0, 1);

                if (whichAttack == 0) {
                    anim.SetBool ("Attack", true);
                } else {
                    anim.SetBool ("Attack", false);
                }

                regen.EnemyAttack ();

                player.PlayerHealth (damage);
                timer = attackCooldown;

            }
        } else {
            if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
                anim.SetBool ("InRange", false);
                if (agent != null && health> 0) {
                    agent.isStopped = false;
                }
            }
            }
            timer -= Time.deltaTime;
    }
	public void EnemyHealth (float dmg) {

        health -= dmg;
        if (health <= 0f && canTakeDmg == true) {

            GetComponentInChildren<Collider> ().isTrigger = true;
            agent.enabled = false;
            AmmoDrop ();
            wave.currEnemy--;
            canTakeDmg = false;

        }
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
            if(!scoreAnim.isPlaying && !oneTime) {

                PlayAnimation(true);

                uim.bonusScore.text = deathPoints.ToString();
		        uim.CheckScore(deathPoints);
                
                oneTime = !oneTime;
            }
            if(!scoreAnim.isPlaying && oneTime) {

                PlayAnimation(false);
            }
        }
        if (deathTimer <= 0f) {
            gameObject.transform.position += (Vector3.down * Time.deltaTime);
        }
	}
    public void PlayAnimation (bool state) {

        if(scoreAnim.isPlaying) {

            
            scoreAnim.Rewind();
        }
        if(state) {
        
            Debug.Log("play");
            scoreAnim.Play();
        }
        if(!state) {
            Debug.Log("Stop");
            scoreAnim.Stop();
        }
    }
}