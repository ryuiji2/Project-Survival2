using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour {

	public Material material;
	private float a;
	private float b;
	public bool boolA;
	public bool boolB;
	public float timeMultiplier = 1;

	void Start () {
		a = (boolA) ? 0 : 1;
		material.SetFloat("_SliderOne", a);
		b = (boolB) ? 0 : 1;
		material.SetFloat("_SliderOne", b);
	}

	void Update () {
		SetBool();
		SetValue();
		material.SetFloat("_SliderOne", a);
		material.SetFloat("_SliderTwo", b);
	}

	void SetBool () {
		if(a < 0){
			boolA = true;
		}
		else if(a > 1) {
			boolA = false;
		}
		if(b < 0){
			boolB = true;
		}
		else if(b > 1) {
			boolB = false;
		}
	}

	void SetValue () {
		if(boolA){
			a += (Time.deltaTime * timeMultiplier);
		}
		else a -= (Time.deltaTime * timeMultiplier);
		if(boolB){
			b += (Time.deltaTime * timeMultiplier);
		}
		else b -= (Time.deltaTime * timeMultiplier);
	}
}
