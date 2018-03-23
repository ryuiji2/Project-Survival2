﻿//Made by Arne
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	//Things to be added:
	/*
	ENEMIES LEFT	

	*/

	//script reference
	private PlayerStats playerStats;
	public RotateCamera camRotateScript;
	public Wave wave;
	public Shooting shootScript;
	public CameraLook camLook;
	public Movement playerMove;
	public Manager manager;

	//HUD
	public Image healthBar;
	public Text ammoText;
	public Text waveText;
	public Text scoreText;
	public Image gunIconSlot;

	//Highscores
	public Text timeHS, waveHS, scoreHS;

	//UI stuff
	public List<RectTransform> allMenuItems = new List<RectTransform>();
	public RectTransform mainMenu, pauseMenu, ingame, settings, gameOver, controlInfo; //gonna add more to these 
	public enum UIState {MainMenu, Ingame, GameOver};
	public UIState _UIState;
	private bool cursorActive;
	public bool paused, settingsActive, controlInfoActive;
	public KeyCode esc;

	//Player 
	public GameObject player, cam;

	//Timer
	public float time; //needed?
	public float hour, minute, second;
	public bool timing;
	public Text timerText;

	//Score
	private int currentScore;


	//sets some things ready
	private void Awake () {	
		
		manager = GameObject.Find("GameManager").GetComponent<Manager>();
		player = GameObject.Find("Player");
		cam = GameObject.Find("Camera");

		shootScript = GameObject.Find("Gun").GetComponent<Shooting>();
		wave = GameObject.Find("WaveManager").GetComponent<Wave>();

		camLook = cam.GetComponent<CameraLook>();
		playerMove = player.GetComponent<Movement>();
		playerStats = player.GetComponent<PlayerStats>();	
		camRotateScript = player.GetComponent<RotateCamera>();
		//gunIconSlot = GameObject.Find("Gun Icon").GetComponent<Image>(); //not needed?

		cursorActive = false;

		CheckScore(currentScore); //so it updates in ui

		ResetTimer();
		CheckUIState();
	}
	//constantly updates
	private void Update () {

		PressEscape();
		ClockingTime();	
	}
	//Updates state of ui
	private void CheckUIState () {

		switch (_UIState) {

        case UIState.MainMenu:

			SetTimer(false);

			manager.ResetGame();
			playerStats.ResetPosition();

			BlockMovement(true);
			
			Time.timeScale = 1;

            List<RectTransform> mainmenulist = new List<RectTransform>() {mainMenu};
			EnableMenuItems(mainmenulist);

            break;

        case UIState.Ingame:

			SetTimer(true);

			BlockMovement(false);
			camRotateScript.enabled = false;
			wave.spawnEnemies = true;

			List<RectTransform> ingameList = new List<RectTransform>() {ingame};
			EnableMenuItems(ingame);
			
			SwitchCursorState();
            
            break;

		case UIState.GameOver:			//player cam stutters when dead

			SetTimer(false);

			HighScore();
			manager.ResetGame();
			playerStats.ResetPosition();

			List<RectTransform> gameOverList = new List<RectTransform>() {gameOver};
			EnableMenuItems(gameOverList);
			SwitchCursorState();

			camRotateScript.enabled = true;

			BlockMovement(true);

			break;
        }
	}
	//block movement and activates other things
	public void BlockMovement (bool state) {

		if(state) {

			camLook.block = true;
			playerMove.block = true;
			shootScript.block = true;
			camRotateScript.enabled = true;
			
		}
		if(!state) {

			camLook.block = false;
			playerMove.block = false;
			shootScript.block = false;
			camRotateScript.enabled = false;
		}
	}
	//sets the correct icon of gun used
	public void SetGunIcon (Sprite icon) {

		gunIconSlot.sprite = icon;
	}
	//makes you pause ingame or unpause
	private void PressEscape () {
		
		if(Input.GetKeyDown(esc) && _UIState == UIState.Ingame && paused == false) {

			paused = true;
			Time.timeScale = 0;
			pauseMenu.gameObject.SetActive(true);

			SwitchCursorState();
		}		
		else if(Input.GetKeyDown(esc) && settingsActive == true) {

			SettingMenu();
		}
		else if(Input.GetKeyDown(esc) && controlInfoActive == true) {

			ControlInfo();
		}
		else if(Input.GetKeyDown(esc) && _UIState == UIState.Ingame && paused == true) {

			paused = false;
			Time.timeScale = 1;
			pauseMenu.gameObject.SetActive(false);

			SwitchCursorState();
		}		
	}
	//sets state and checks the next state 
	public void SetState (UIState state) {

		_UIState = state;
		paused = false;
		settingsActive = false;
		controlInfoActive = false;
		CheckUIState();
	}
	//make you enable or disable the settings
	public void SettingMenu () {

		settingsActive = !settingsActive;
		settings.gameObject.SetActive(settingsActive);
		//enables settings rectTransform or disables it
	}
	//enables and disables the control info panel
	public void ControlInfo () {

		controlInfoActive = !controlInfoActive;
		controlInfo.gameObject.SetActive(controlInfoActive);
	}
	//button function
	public void MainMenu () {

		SetState(UIState.MainMenu);
	}
	//button function
	public void Ingame () {

		SetState(UIState.Ingame);
		Time.timeScale = 1;
	}
	//button function
	public void QuitGame () {

		Application.Quit();
	}
	//sets the right cursor state
	private void SwitchCursorState () {

		cursorActive = !cursorActive;
		if(!cursorActive) {

			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None; 
			
		}
		if(cursorActive) {

			Cursor.lockState = CursorLockMode.Locked; 
			Cursor.visible = false;
		}	
	}
	//receives items and will make a list of them that will get send to another function
	private void EnableMenuItems(RectTransform item) {

        List<RectTransform> items = new List<RectTransform>() { item };
        EnableMenuItems(items);
    }
    //gets a list that will in which the objects will get set true after everything is set false
    private void EnableMenuItems(List<RectTransform> items) {

        foreach (RectTransform rT in allMenuItems)
            rT.gameObject.SetActive(false);

        foreach (RectTransform rT in items)
            rT.gameObject.SetActive(true);
    }
	//checks health and updates ui
	public void CheckHealth () {

		healthBar.fillAmount = playerStats.healthPercentage;
	}
	//checks ammo and updates ui    	
	public void CheckAmmo (int current, int max) {

		ammoText.text = current + " / " + max;
	}
	//checks wave and updates ui
	public void CheckWave (int waveNumber) {

		waveText.text = "Wave : " + waveNumber;
	}
	//checks score and updates ui
	public void CheckScore (int points) {

		currentScore += points;
		scoreText.text = "Score: " + currentScore;
	}
	//Highscores
	public void HighScore () {  //should be reset after exiting gameover

		scoreHS.text = "Final Score : " + currentScore;
		timeHS.text = "Final Time : " + hour + ":" + minute + ":" + second;
		waveHS.text = "Final Wave : " + wave.currWave;
	}	
	#region Timer
	//sets bool 
	public void SetTimer (bool set) 
	{
		timing = set;
	}
	void ClockingTime () 
	{
		if(!timing)
		{
			
			return;
		}
		second += Time.deltaTime;
		
		//round time
		if(second >= 60f)
		{
			minute++;
		}
		if(minute >= 60f)
		{
			hour++;
		}
		SetTimerText();
	}
	void SetTimerText () 
	{
		timerText.text = "Time: " + hour + ":" + minute + ":" + (int)second;
	}
	public void ResetTimer () 
	{
		SetTimer(false);
		SetTimerText();
		hour = 0f;
		minute = 0f;
		second = 0f;
	}
	#endregion
}
