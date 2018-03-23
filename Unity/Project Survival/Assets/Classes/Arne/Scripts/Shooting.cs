using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {


	public ParticleSystem shootParticle;
	public ParticleSystem enemyHit, envWoodhit, envStonehit;

	//weaponswitch 
	public enum Weapon {Pistol, Mp40};
	public Weapon _Weapon;
	public bool pistol, mp40;
	public int pistolCurrentAmmo, pistolMagAmmo, pistolAmmoTotal, mp40CurrentAmmo, mp40MagAmmo, mp40AmmoTotal;
	public int pistolDMG, mp40DMG;

	//USE INHERITANCE?
	public Camera cam;

	public KeyCode key, switchkey;

	public UIManager uim;

	public Sprite pistolIcon, mp40Icon;

	public float damage;

	public bool reload;

	public GameObject bulletHole;

	public bool block;

	public float fireRate, fireRateTime;

	public bool aimed; // if we can aim

    public int damageMulti;


	//sets cursorstate and other variables that need a certain value at start of game
	private void Awake () {

		uim = GameObject.Find("Canvas").GetComponent<UIManager>();//gives error needs to be manually put in

		fireRate = fireRateTime;

		pistolCurrentAmmo = pistolMagAmmo; //needs to be how much you picked
		mp40CurrentAmmo = mp40MagAmmo;

	
		
		uim.gunIconSlot.sprite = null;

		WeaponState();
	}
	// Update is called once per frame
	private void Update () {
		
		if(!block)
		{
			CheckInput();
			SwitchWeapon();
			Reload();
		}
	}
	//state for weapon update like ammo and the like
	public void WeaponState () {

		switch(_Weapon){

			case Weapon.Pistol:

				mp40 = false;
				pistol = true;
				damage = pistolDMG;
				//play grab pistol animation
				//show visual weapon this weapon on other off
				Debug.Log("Start pistol");
				uim.SetGunIcon(pistolIcon);
				SendAmmoValues();


			break;

			case Weapon.Mp40:

				pistol = false;
				mp40 = true;
				damage = mp40DMG;
				//play grab mp40 animation
				//show visual weapon
				uim.SetGunIcon(mp40Icon);
				SendAmmoValues();

			break;
		}
	}
	//switches weapon
	public void SwitchWeapon () { //scroll and a value goes up and scroll in a list of weapons, if hit limit of list goes back to 0

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
	//updates the current ammo values in UIManager
	public void SendAmmoValues () {

		if(pistol) {

			Debug.Log("Start");
			uim.CheckAmmo(pistolCurrentAmmo, pistolAmmoTotal);
		}
		if(mp40) {

			uim.CheckAmmo(mp40CurrentAmmo, mp40AmmoTotal);
		}
	}
	//Checks for shooting Input
	public void CheckInput () {
	
        if(pistol == true && Input.GetKeyDown(key)) {

			CheckAmmoCount();
			SendAmmoValues();
		} 
		else if(mp40 == true && Input.GetKey(key)) {

			//how quick it shoots
			fireRate -= Time.deltaTime;
			if(fireRate < 0) {

				CheckAmmoCount();
				SendAmmoValues();
				fireRate = fireRateTime;
			}
		}	
	}
	//Checks ammo count so you can't shoot when you don't have ammo
	public void CheckAmmoCount () {

		if(mp40CurrentAmmo < 0) {

			mp40CurrentAmmo = 0;
			Debug.Log("reload");
		}
		if(pistolCurrentAmmo < 0) {

			pistolCurrentAmmo = 0;
			Debug.Log("Reload");
		}
		if(pistolCurrentAmmo > 0 & pistol || mp40CurrentAmmo > 0 & mp40) {

			Shoot();
		}
	}
	//Will fill your magazine again with bullets
	private void Reload () {

		if(Input.GetButtonDown("Reload")) {

			if(pistol) {
				//play animation
				Debug.Log("Reload pistol");
				pistolCurrentAmmo = pistolMagAmmo;
				
			}
			if(mp40) {

				//play animation
				Debug.Log("Reload mp40");
				
				//calculates with math that it won't grab ammo that doesn't exist
				int extraFilling = mp40MagAmmo - mp40CurrentAmmo;

				if(mp40AmmoTotal < extraFilling)
				{
					extraFilling = mp40AmmoTotal;
				}
				mp40CurrentAmmo += extraFilling;
				mp40AmmoTotal -= extraFilling;		
			}
			SendAmmoValues();
		}
	}
	//shoots and hit
	private void Shoot () {

		Debug.Log("Shoot");
		if(pistol)
		{
			pistolCurrentAmmo --;
		}
		if(mp40)
		{
			mp40CurrentAmmo --;
		}

		//random range for accuracy
		float offsetX = Random.Range(-.05f, .05f);
		float offsetY = Random.Range(-.05f, .05f);
		float offsetZ = Random.Range(-.05f, .05f);
		//offsetZ = 0;

		Debug.Log(offsetX + "X");
		Debug.Log(offsetY + "Y");
		Debug.Log(offsetZ + "Z");
		if(!aimed)
		{
			offsetX = 0;
			offsetY = 0;
			offsetZ = 0;
		}


		RaycastHit hit;

		if (Physics.Raycast(cam.transform.position, new Vector3(cam.transform.forward.x + offsetX, cam.transform.forward.y + offsetY, cam.transform.forward.z + offsetZ), out hit)) {

            GameObject objectHit = hit.collider.gameObject; //what you hit
            EnemyStats enemy = objectHit.GetComponent<EnemyStats> ();

            if (hit.collider.tag == "Enemy") {

				enemy.EnemyHealth(damage);
				//particles
			}
            if(hit.collider.tag == "Head") {

                enemy.EnemyHealth (damage * damageMulti);
            }
			else {

				//spawn plane that looks like bullethole
				Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
			}	
		}
	}
}
