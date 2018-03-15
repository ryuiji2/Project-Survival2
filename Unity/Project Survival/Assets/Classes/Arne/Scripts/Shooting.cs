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

	public int pistolCurrentAmmo, pistolMagAmmo, pistolAmmo, mp40CurrentAmmo, mp40MagAmmo, mp40Ammo;

	//USE INHERITANCE?
	public Camera cam;

	public KeyCode key, switchkey;

	public UIManager uim;

	public Sprite pistolIcon, mp40Icon;

	public float damage;



	//sets cursorstate
	private void Awake () {

		uim = GameObject.Find("Canvas").GetComponent<UIManager>();

		
		pistolCurrentAmmo = pistolMagAmmo; //needs to be how much you picked
		mp40CurrentAmmo = mp40MagAmmo;

		uim.gunIconSlot.sprite = null;

		WeaponState();
	}
	// Update is called once per frame
	private void Update () {
		
		CheckInput();
		SwitchWeapon();
		Reload();
	}
	//state for weapon update like ammo and the like
	public void WeaponState () {

		switch(_Weapon){

			case Weapon.Pistol:

				mp40 = false;
				pistol = true;
				//play grab pistol animation
				//show visual weapon this weapon on other off
				Debug.Log("Start pistol");
				uim.SetGunIcon(pistolIcon);
				SendAmmoValues();


			break;

			case Weapon.Mp40:

				pistol = false;
				mp40 = true;
				//play grab mp40 animation
				//show visual weapon
				uim.SetGunIcon(mp40Icon);
				SendAmmoValues();

			break;
		}
	}
	//switches weapon
	public void SwitchWeapon () {

		if(Input.GetKeyDown(switchkey)) {

			if(pistol == true) {

				_Weapon = Weapon.Mp40;
				//maybe other guns false
			}
			if(mp40 == true) {

				_Weapon = Weapon.Pistol;
			}
			Debug.Log(_Weapon);
			WeaponState();
		}
	}
	public void SendAmmoValues () {

		if(pistol) {

			Debug.Log("Start");
			uim.CheckAmmo(pistolCurrentAmmo, pistolAmmo);
		}
		if(mp40) {

			uim.CheckAmmo(mp40CurrentAmmo, mp40Ammo);
		}
	}
	//Checks for shooting Input
	public void CheckInput () {
	
        if(pistol == true && Input.GetKeyDown(key)) {

			pistolCurrentAmmo --;
			CheckAmmoCount();
			SendAmmoValues();
		} 
		else if(mp40 == true && Input.GetKey(key)) {

			mp40CurrentAmmo --;
			CheckAmmoCount();
			SendAmmoValues();
		}	
	}
	public void CheckAmmoCount () {


		if(mp40CurrentAmmo <= 0) {

			mp40CurrentAmmo = 0;
			Debug.Log("reload");
		}
		if(pistolCurrentAmmo <= 0) {

			pistolCurrentAmmo = 0;
			Debug.Log("Reload");
		}
		if(pistolCurrentAmmo > 0 & pistol|| mp40CurrentAmmo > 0 & mp40){

			Shoot();
		}
	}
	private void Reload () {


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
				EnemyStats enemy = objectHit.GetComponent<EnemyStats>();
				enemy.EnemyHealth(damage);
				//particles
				//objectHit.GetComponent<EnemyStats>.Health(damage);
			}
			else {
				//spawn plane that looks like bullethole
				
			}	
		}
	}
}
