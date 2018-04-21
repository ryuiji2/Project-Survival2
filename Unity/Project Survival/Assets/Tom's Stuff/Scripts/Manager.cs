using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	[HideInInspector]
	public static int score;
	[Range(1,100)]
	public static int currentWave = 1;

	private List<GameObject> enemyObject = new List <GameObject>();
	private GameObject enemy;
	private Wave wave;

	private UIManager uim;
	private GameObject player;
	private PlayerStats playerStats;

	public Shooting shootScript;

	private void Awake () {

		//shootScript = GameObject.FindWithTag("Gun").GetComponent<Shooting>();
		wave = GameObject.Find("WaveManager").GetComponent<Wave>();
		uim = GameObject.Find("Canvas").GetComponent<UIManager>();

		player = GameObject.Find("Player");
		playerStats = player.GetComponent<PlayerStats>();	
	}
	public void KillEnemies () { //crashes game

		//make list of enemies and kill
		var objects = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (var enemy in objects) {
			
			wave.ResetEnemies();
			Destroy(enemy);
		}
	}
	public void ResetHUD () {

		//reset Highscore, Timer, Wave, Enemies, Playerhealth
		uim.currentScore = 0;
		uim.CheckScore(uim.currentScore);
		uim.ResetTimer();
		uim.wave.ResetEnemies();
		playerStats.PlayerReset();
		shootScript.ResetGuns();
	}
	public void SetTimeScale (int scale) { //needed?

		Time.timeScale = scale;
	}

}
