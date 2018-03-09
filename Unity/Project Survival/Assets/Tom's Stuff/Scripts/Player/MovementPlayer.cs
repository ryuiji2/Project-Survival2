using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour {

	public float movementSpeed = 5f;
	public float rotationSpeed = 2f;

	private Rigidbody test;


	private void Start() {
		test = GetComponent<Rigidbody>();
	}

	private void FixedUpdate() {
		Movement();
	}

	private void Update() {
		RotateBody();
	}

	private void Movement() {
		float xAxis = Input.GetAxis("Horizontal");
		float zAxis = Input.GetAxis("Vertical");
		//transform.Translate(new Vector3(xAxis,0f,zAxis) * Time.deltaTime * movementSpeed, Space.Self);
		test.position += zAxis * transform.forward * Time.deltaTime * movementSpeed;
		test.position += xAxis * transform.right * Time.deltaTime * movementSpeed;
	}

	private void RotateBody() {
		float yAxis = rotationSpeed * Input.GetAxis("Mouse X");
		transform.eulerAngles += new Vector3(0f,yAxis,0f);
	}

}
