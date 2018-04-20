using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour {

    public float speedH;
    public float speedV;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public Transform player;

    public bool block;

    //Camera Shake
    private bool shakeCam;
    private float shake;
    public float maxShake;
    private bool lower;

    private void Awake () {

        player = GameObject.Find("Player").transform;
    }
    private void FixedUpdate () {
        
        if(!block)
        {
            yaw += speedH * Input.GetAxis ("Mouse X");
            pitch -= speedV * Input.GetAxis ("Mouse Y");
            pitch = Mathf.Clamp (pitch, -90f, 90f);
            player.eulerAngles = new Vector3 (0.0f, yaw, 0.0f);
            if (shakeCam)
            {
                if (!lower)
                {
                    if(shake < maxShake)
                    {
                        shake += Time.deltaTime * 100;
                    }
                    else if(shake > maxShake)
                    {
                        shake = maxShake;
                    }
                    if(shake >= maxShake)
                    {
                        lower = true;
                    }
                }
                else
                {
                    if (shake > 0)
                    {
                        shake -= Time.deltaTime * 50;
                    }
                    else shake = 0;
                }
            }
            transform.eulerAngles = new Vector3(pitch - shake, yaw, 0.0f);
        }
    }

    public void StartShake()
    {
        shakeCam = true;
        if (lower)
        {
            lower = false;
        }
    }
}