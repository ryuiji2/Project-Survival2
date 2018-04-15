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

	public Vector3 startPos;



	private void Awake () {

		uim = GameObject.Find("Canvas").GetComponent<UIManager>(); //gives error needs to be manually put in
		maxHP = 100;
		health = maxHP;
		startPos = transform.position;
	}
	public void PlayerHealth (float dmg) {

		health -= dmg;
		healthPercentage = health / maxHP;
		if(health <= 0f) {

			Debug.Log("Dead");	
			uim.SetState(UIManager.UIState.GameOver);
		}
		uim.CheckHealth();
	}
	public void PlayerReset () {

		transform.position = startPos;
		health = maxHP;
		PlayerHealth(0f);
		//playeranimator disable
	}
}
