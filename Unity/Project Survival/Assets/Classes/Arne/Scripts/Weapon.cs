using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public GameObject visual;
	public Shooting shootScript;


	private void Awake () {

		visual = this.gameObject;
		shootScript = gameObject.GetComponent<Shooting>();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
