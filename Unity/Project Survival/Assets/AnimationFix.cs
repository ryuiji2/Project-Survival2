using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFix : MonoBehaviour
{
    //Player
    public Shooting shootRef;
    public CameraLook camRef;

    //Sound
    public AudioSource audioSource;
    public AudioClip pistolShoot, pistolReload, mp40Shoot, mp40Reload;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    #region Player
    public void Reload()
    {
        shootRef.Reloaded();
    }

    public void Block(int i)
    {
        if(i < 5)
        {
            shootRef.block = true;
        }
        else shootRef.block = false;
    }

    public void Switch()
    {
        shootRef.switching = false;
        shootRef.block = false;
    }

    public void Shoot(float f)
    {
        camRef.StartShake();
        camRef.maxShake = f;
    }
    #endregion

    #region Sound
    public void PistolShoot()
    {
        audioSource.clip = pistolShoot;
        audioSource.Play();
    }
    public void PistolReload()
    {
        audioSource.clip = pistolReload;
        audioSource.Play();
    }
    public void Mp40Shoot()
    {
        audioSource.clip = mp40Shoot;
        audioSource.Play();
    }

    public void Mp40Reload()
    {
        audioSource.clip = mp40Reload;
        audioSource.Play();
    }
    #endregion
}
