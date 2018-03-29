﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour {

    public float speedH;
    public float speedV;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public Transform player;

    public bool block;

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
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}