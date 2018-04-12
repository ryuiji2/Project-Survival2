using System.Collections;
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

    public Animation regenIcon, regenBar;
    public Animator regenIconR, regenBarR;


    void Start () {

        playerstats = GetComponent<PlayerStats>();
        Regenerate = RegenHealth ();
        outOfCombat = waitForRegen;
        //PlayAnimation(false);
    }
    public void EnemyAttack () {

        outOfCombat = waitForRegen;
        StopCoroutine (Regenerate);
        beenHit = true;
        Regenerating();
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
            PlayAnimation(true);
            playerstats.PlayerHealth(-regen);
        }
        PlayAnimation(false);
    }
    private void PlayAnimation (bool state) {

        //regenIcon.Play();
        //regenBar.Play();
        if(state) {
            
            Debug.Log("Play");
            regenBar.Play();
            regenIcon.Play();
        }
        if(!state) {
            
            Debug.Log("Stop");
            regenBar.Stop();
            regenIcon.Stop();
        }
    }
}