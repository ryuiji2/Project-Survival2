using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour {

    public int waitForRegen;
    public float outOfCombat;
    public int regen;
    public int maxRegen;
    private bool beenHit = false;
    private IEnumerator Timer;
    private IEnumerator Regenerate;
    private PlayerStats playerstats;
    private Coroutine isTimer;

    void Start () {
        playerstats = GetComponent<PlayerStats>();
        Regenerate = RegenHealth ();
        outOfCombat = waitForRegen;
    }

    void Update () {
        Regenerating ();
        HitTest (); //verweideren na implimentation van EnemyStats
    }
    void HitTest () {
        //verweideren na implimentation van EnemyStats
        if (Input.GetButtonDown ("Test")) {
            EnemyAttack ();
        }
    }

    void EnemyAttack () {
            GetComponent<PlayerStats> ().health -= 25;
            outOfCombat = waitForRegen;
            StopCoroutine (Regenerate);
            beenHit = true;
    }

    void Regenerating () {

        if (beenHit == true) {
            if (isTimer != null) {
                StopCoroutine (isTimer);
            }
            isTimer = StartCoroutine (HitTime());
            beenHit = false;
        }
    }

    IEnumerator HitTime () {
        while(outOfCombat > 0) {
            yield return new WaitForSeconds (1);
            outOfCombat -= 1;
        }
        StartCoroutine (Regenerate);
    }

    IEnumerator RegenHealth () {
        while (playerstats.health < maxRegen) {
            yield return new WaitForSeconds (1);
            playerstats.health += regen;
        }
    }
}