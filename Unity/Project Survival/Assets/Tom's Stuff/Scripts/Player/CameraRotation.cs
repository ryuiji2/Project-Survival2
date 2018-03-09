using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

	public float rotationSpeed = 2f;

	private void Update() {
		RotateCamera();
	}

	private void RotateCamera() {
		float xAxis = rotationSpeed * Input.GetAxis("Mouse Y");
		transform.eulerAngles -= new Vector3(xAxis,0f,0f);
	}
}
