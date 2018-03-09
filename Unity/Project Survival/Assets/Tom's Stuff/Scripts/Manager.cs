using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	[HideInInspector]
	public static int score;
	[Range(1,100)]
	public static int currentWave = 1;
}
