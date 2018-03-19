//Made by Arne
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateCamera : MonoBehaviour {

	private GameObject cam;

	private void Awake () {

		cam = GameObject.Find("Camera");
	}
	//constantly rotates
	void FixedUpdate () 
	{
		cam.transform.Rotate(Vector3.up * Time.deltaTime * 2);	
	}
}
