﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour {

    public int waitForRegen;
    public float outOfCombat;
    public int regen;
    public int maxRegen;
    public bool beenHit = false;
    private IEnumerator Timer;
    private IEnumerator Regenerate;
    private PlayerStats playerstats;
    private Coroutine isTimer;

    void Start () {

        playerstats = GetComponent<PlayerStats>();
        Regenerate = RegenHealth ();
        outOfCombat = waitForRegen;
    }
    void EnemyAttack () {

            playerstats.PlayerHealth(25);
            outOfCombat = waitForRegen;
            StopCoroutine (Regenerate);
            beenHit = true;
    }
    public void Regenerating () {

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
            //play animation on healthcontainer icon
            playerstats.PlayerHealth(-regen);
        }
    }
}