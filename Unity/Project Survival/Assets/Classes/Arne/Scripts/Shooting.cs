using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public ParticleSystem shootParticle;
	public ParticleSystem enemyHit, envWoodhit, envStonehit;

	//weaponswitch test
	public enum Weapon {Pistol, Mp40};
	public Weapon _Weapon;
	public bool pistol, mp40;

	public int pistolMaxAmmo, pistolAmmo, mp40MaxAmmo, mp40Ammo;

	//USE INHERITANCE?
	public Camera cam;

	public KeyCode key, switchkey;

	//sets cursorstate
	private void Awake () {
		//Cursor.lockState = CursorLockMode.Locked; //should happen in ui manager
		WeaponState();
		pistolAmmo = pistolMaxAmmo;
		mp40Ammo = mp40MaxAmmo;
	}
	// Update is called once per frame
	private void Update () {
		
		CheckInput();
		SwitchWeapon();
	}
	//state for weapon update like ammo and the like
	public void WeaponState () {

		switch(_Weapon){

			case Weapon.Pistol:

				//play grab pistol animation
				//show visual weapon this weapon on other off
				//update ammo
				mp40 = false;
				pistol = true;

			break;

			case Weapon.Mp40:

				//play grab mp40 animation
				//show visual weapon
				//update ammo
				pistol = false;
				mp40 = true;

			break;
		}
	}
	//switches weapon
	public void SwitchWeapon () {

		if(Input.GetKeyDown(switchkey)) {

			if(pistol == true) {

				_Weapon = Weapon.Mp40;
			}
			if(mp40 == true) {

				_Weapon = Weapon.Pistol;
			}
			Debug.Log(_Weapon);
			WeaponState();
		}
	}
	//Checks for shooting Input
	public void CheckInput () {
	
        if(pistol == true && Input.GetKeyDown(key)) {

			Shoot();
			pistolAmmo--;
			Debug.Log("SHoot pistol");
		} 
		else if(mp40 == true && Input.GetKey(key)) {

			Shoot();
			mp40Ammo--;
			Debug.Log("Shoot mp40");
		}
	}
	//shoots and hit
	private void Shoot () {

		Debug.Log("Shoot");
		//random range for accuracy

		RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); // * accuracy

		if (Physics.Raycast(ray, out hit)) {

			if(hit.collider.tag == "Enemy") {

				GameObject objectHit = hit.collider.gameObject; //what you hit
				//objectHit.GetComponent<EnemyStats>.Health(damage);
			}
			else {
				//spawn plane that looks like bullethole
				
			}	
		}
	}
}
