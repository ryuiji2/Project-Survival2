using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	[HideInInspector]
	public static int score;
	[Range(1,100)]
	public static int currentWave = 1;

/* 
	public bool inUI;					//was meant to disable movement from this script
	public Movement playerMove;
	public CameraLook camLook;

	public void InUIMode () {

		if(inUI) {

			//block movement
		}
		else {

			camLook.
		}
	}*/
}
