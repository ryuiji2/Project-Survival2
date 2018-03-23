using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public int currWave; //welke wave het is.
    public int currEnemy; //hoeveel enemies er zijn gespawned.
    public int maxEnemy; //maximale enemies op de map.
    public float waveEnemy; //hoeveel enemies per wave.
    public float extraSpawn; //hoeveel extra enemies er zijn na de enemy cap.
    public GameObject zombie; //de zombie.
    public List<EnemyStats> zombieStats = new List<EnemyStats> (); //list van EnemyStats, handig voor instakill drop (if implimented)
    public List<GameObject> spawnLoc = new List<GameObject>(); //in deze list komen de spawnpoints voor de zombies.
    public float waveTime; //hoeveel tijd er tussen de waves zit.
    public float spawnTime; //hoeveel tijd er tussen de spawning van zombies zit.

    private bool coroutineActive;
    private bool canSpawnExtra;

    public Transform player;
    private UIManager uim;

    public float starterEnemycount;

    public bool spawnEnemies;
    

    private void Awake () {

        uim = GameObject.Find("Canvas").GetComponent<UIManager>();
        player = GameObject.Find("Player").transform;
	}
	void Start () {

        waveEnemy = 2; //verrander dit als je het aantal begin enemies wilt verranderen
        starterEnemycount = waveEnemy;
        currWave = 0; //hoef je niet aan te passen
        //uim.CheckWave(currWave);
        //maxEnemy = 30; //maximum aantal enemies dat in 1 keer op de map kunnen zitten
	}
	void Update () {

        
        EnemyCheck();
        ExtraEnemy();
        Debug.Log("updating");

       /* if (Input.GetButtonDown ("Cancel")) {
            currEnemy--;
            dit word aangepast met EnemyStats (Voor --)
        }*/
	}
    void EnemyCheck () {

        if(currEnemy < 0) {
            
            currEnemy = 0;
        }
        if (currEnemy == 0 && !coroutineActive && spawnEnemies) {
            currWave++;
            uim.CheckWave(currWave);
            waveEnemy = Mathf.Ceil (waveEnemy *= 1.1f); //elke wave gaat het aantal zombies omhoog, afgerond naar boven.
            coroutineActive = true;
            StartCoroutine (NextWave());
            if (waveEnemy > maxEnemy) {
                extraSpawn = waveEnemy - maxEnemy;
            }
        }
    }
    void ExtraEnemy () {

        int randomSpawnLoc = Random.Range (0, spawnLoc.Count);
        if (canSpawnExtra == true) {
            if (extraSpawn > 0 && currEnemy < maxEnemy) {
                GameObject spawnedZombie = Instantiate (zombie, spawnLoc [randomSpawnLoc].transform.position, Quaternion.identity) as GameObject;
                zombieStats.Add (spawnedZombie.GetComponent<EnemyStats> ());
                currEnemy++;
                extraSpawn--;
            }
        }
    }
    IEnumerator NextWave() {

        canSpawnExtra = false;
        yield return new WaitForSeconds(waveTime);

        for (int i = 0; i < waveEnemy; i++) {
            if (currEnemy < maxEnemy) {
                int randomSpawnLoc = Random.Range (0, spawnLoc.Count);
                yield return new WaitForSeconds (spawnTime);
                Instantiate (zombie, spawnLoc [randomSpawnLoc].transform.position, Quaternion.identity);
                currEnemy++;
            }
        }
        canSpawnExtra = true;
        coroutineActive = false;
    }
    public void ResetEnemies () {

        Debug.Log("ResetEnemies");
        spawnEnemies = false;
        currEnemy--;
		waveEnemy = starterEnemycount;
        currWave = 0;
    }
}