using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Invoke("Destroy",10f);
	}
	void Destroy () {

		Destroy(gameObject);
	}
	void FadeOut () {


	}
}
