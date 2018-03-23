using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour {

	public float duration;

	// Use this for initialization
	void Start () 
	{
		Invoke("Destroy",duration);
	}
	void Destroy () {

		Destroy(gameObject);
	}
	void FadeOut () {


	}
}
