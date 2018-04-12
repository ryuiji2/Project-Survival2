using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatieTest : MonoBehaviour
{
	public Animator anim;
    public CameraShake camShake;
	public bool mp40;
	private AnimationClip pistolReload;
	private AnimationClip mp40Reload;
	public int ammo = 20;
	private int _ammo;
	private bool reloading;

	void Start ()
	{
		anim.SetBool("MP40", mp40);
		_ammo = ammo;
		anim.SetBool("FA", true);
		pistolReload = GetAnimation("Handgun reload");
		mp40Reload = GetAnimation("Mp40 reload");
	}
	AnimationClip GetAnimation (string name) {
		foreach(AnimationClip clip in anim.runtimeAnimatorController.animationClips) {
			if(clip.name == name) {
				return clip;
			}
		}
		return null;
	}
	void Update ()
	{
        //Reload
        if (Input.GetButtonDown("Jump"))
        {
            mp40 = !mp40;
            anim.SetBool("MP40", mp40);
            anim.SetTrigger("Switch");
            
        }
        //Fire
		if(Input.GetButtonDown("Fire1")) {
			if(ammo >= 1) {
				ammo--;
				anim.SetTrigger("Shoot");
                camShake.Shake(10f);
				if(ammo < _ammo) {
					anim.SetBool("FA", false);
				}
				else if(ammo <= 0) {
					anim.SetBool("OOA", true);
				}
			}
		}
		//Zoom
		if(Input.GetButton("Fire2")) {
			anim.SetBool("Zoom", true);
		}
		else {
			anim.SetBool("Zoom", false);
		}
		//Reload
		if(Input.GetButtonDown("Reload")) {
			if(ammo < _ammo && reloading == false) {
				anim.SetTrigger("Reload");
				StartCoroutine(Reload());
			}
		}
	}

	IEnumerator Reload () {
		reloading = true;
		AnimationClip ac = (mp40) ? pistolReload : mp40Reload;
		print("Reloading!");
		yield return new WaitForSeconds(ac.length);
		ammo = _ammo;
		anim.SetBool("FA", true);
		anim.SetBool("OOA", false);
		reloading = false;
	}
}
