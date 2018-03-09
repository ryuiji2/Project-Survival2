//Made by Arne
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateCamera : MonoBehaviour {

	private CameraRotation lookScript;
	private MovementPlayer movement;

	private void Awake () {

		//disable moving and look scripts
	}
	//constantly rotates
	void FixedUpdate () 
	{
		transform.Rotate(Vector3.up * Time.deltaTime);	
	}
}
