using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalDrops : MonoBehaviour {
    
    public int ammo;
    private Shooting mp40;
    public float enemyHealth;
    public Wave wave;

    private void Awake () {
        wave = GameObject.Find("_Manager").GetComponent<Wave> ();
    }

    private void OnCollisionEnter (Collision drop) {
        if (drop.transform.tag == "ammoDrop") {
            mp40 = FindObjectOfType<Shooting> ();
            mp40.mp40AmmoTotal += ammo;
            Destroy (drop.gameObject);
        }
        if(drop.transform.tag == "Finish") {
            StartCoroutine (InstaKill());
        }
    }

    IEnumerator InstaKill () {
        foreach (EnemyStats enemy in wave.zombieStats) {
            enemyHealth = enemy.health;
            enemy.health = 1;
        }
        yield return new WaitForSeconds (2);

        foreach (EnemyStats enemy in wave.zombieStats) {
            enemy.health = enemyHealth;
        }
    }
}