using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	//weapons
	public List<GameObject> weapons = new List<GameObject>();
	public GameObject currentWeapon;

	//health 
	public float maxHP;
	public float health;
	public float healthPercentage;
	UIManager uim;

	private Vector3 startPos;



	private void Awake () {

		uim = GameObject.Find("Canvas").GetComponent<UIManager>(); //gives error needs to be manually put in
		maxHP = 100;
		health = maxHP;
	}
	public void PlayerHealth (float dmg) {

		health -= dmg;
		healthPercentage = health / maxHP;
		uim.CheckHealth();
	}
}
