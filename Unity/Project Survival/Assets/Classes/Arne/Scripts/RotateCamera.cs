//Made by Arne
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateCamera : MonoBehaviour {

	private GameObject cam;

	private void Awake () {

		cam = GameObject.Find("Camera");
	}
	private void OnEnable () {

		cam.transform.rotation = Quaternion.identity;
	}
	//constantly rotates
	void FixedUpdate () 
	{
		cam.transform.Rotate(Vector3.up * Time.deltaTime * 2);	
	}
}
