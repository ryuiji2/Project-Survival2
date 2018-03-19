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

	private void Awake () {

		wave = GameObject.Find("WaveManager").GetComponent<Wave>();
	}
	public void ResetGame () {

		//make list of enemies and kill
		var objects = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (var enemy in objects) {
			
			wave.ResetEnemies();
			Destroy(enemy);
			Debug.Log("reset");
		}
		
	}
}
