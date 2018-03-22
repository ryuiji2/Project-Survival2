using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float time; //needed?
	public float hour, minute, second;

	public bool timing;

	public Text timerText;


	private void Awake () {

		timerText = GameObject.Find("Timer Text").GetComponent<Text>();
		ResetTimer();
	}
	// Update is called once per frame
	private void FixedUpdate () {

		ClockingTime();	
	}
	//sets bool 
	public void SetTimer (bool set) {

		timing = set;
	}
	private void ClockingTime () { //fix the decimals

		if(!timing) {
			
			return;
		}
		second += Time.deltaTime;
		
		//round time
		if(second >= 60f) {

			minute++;
		}
		if(minute >= 60f) {

			hour++;
		}
		SetTimerText();
	}
	private void SetTimerText () 
	{
		timerText.text = "Timer: " + hour + ":" + minute + ":" + (int)second;
	}
	public void ResetTimer () 
	{
		SetTimer(false);
		SetTimerText();
		hour = 0f;
		minute = 0f;
		second = 0f;
	}
}
