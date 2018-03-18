//Made by Arne
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	//Things to be added:
	/*

	QUIT GAME SHOULD KILL ALL ENEMIES
	
	TIMER


	ENEMIES LEFT
	
	NEW UI AND FONT:
	CROSSHAIR, BUTTONS, HEALTHBAR, AMMOBAR, WEAPONSWITCH
	
	*/

	//script reference
	private PlayerStats playerStats;
	
	private RotateCamera camRotateScript;

	//health
	public Image healthBar;

	//ammo
	public Text ammoText;

	public Text waveText;


	//UI stuff
	public List<RectTransform> allMenuItems = new List<RectTransform>();
	public RectTransform mainMenu, pauseMenu, ingame, settings, gameOver; //gonna add more to these 
	public enum UIState {MainMenu, Ingame, GameOver};
	public UIState _UIState;

	public bool paused, settingsActive;
	public KeyCode esc;


	bool cursorActive;

	public Image gunIconSlot;

	public Shooting shootScript;

	string infinite;

	public CameraLook camLook;
	public Movement playerMove;


	//sets some things ready
	private void Awake () {	
		
		camLook = GameObject.Find("Player").GetComponent<CameraLook>();
		playerMove = GameObject.Find("Player").GetComponent<Movement>();

		infinite = "∞";
		shootScript = GameObject.Find("Gun").GetComponent<Shooting>();

		//camRotateScript = GameObject.Find("Camera").GetComponent<RotateCamera>();
		playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

		//gunIconSlot = GameObject.Find("Gun Icon").GetComponent<Image>();

		CheckUIState();

		cursorActive = false;
	}
	private void Update () {

		PressEscape();
	}
	private void CheckUIState () {

		switch (_UIState) {

        case UIState.MainMenu:

			//camRotateScript.gameObject.SetActive(true);

			Time.timeScale = 0;


            List<RectTransform> mainmenulist = new List<RectTransform>() {mainMenu};
			EnableMenuItems(mainmenulist);

            break;

        case UIState.Ingame:

			//camRotateScript.gameObject.SetActive(false);
			List<RectTransform> ingameList = new List<RectTransform>() {ingame};
			EnableMenuItems(ingame);
			
			SwitchCursorState();
            
            break;

		case UIState.GameOver:

			List<RectTransform> gameOverList = new List<RectTransform>() {gameOver};
			EnableMenuItems(gameOverList);
			SwitchCursorState();

			break;
        }
	}
	public void BlockMovement (bool state) {

		if(state) {

			camLook.block = true;
			playerMove.block = true;
			shootScript.block = true;
		}
		if(!state) {

			camLook.block = false;
			playerMove.block = false;
			shootScript.block = false;
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

			print(Time.timeScale);
			//pause game and turns pausemenu on
			//if statements so you can esc out of every window
		}		
		else if(Input.GetKeyDown(esc) && settingsActive == true) {

			SettingMenu();
		}
		else if(Input.GetKeyDown(esc) && _UIState == UIState.Ingame && paused == true) {

			paused = false;
			Time.timeScale = 1;
			pauseMenu.gameObject.SetActive(false);

			print(Time.timeScale);
			//pause game and turns pausemenu on
			//if statements so you can esc out of every window
		}		
	}
		//sets state and checks the next state 
	public void SetState (UIState state) {

		_UIState = state;
		paused = false;
		settingsActive = false;
		CheckUIState();
	}
	//make you enable or disable the settings
	public void SettingMenu () {

		settingsActive = !settingsActive;
		settings.gameObject.SetActive(settingsActive);
		//enables settings rectTransform or disables it
	}
	public void MainMenu () {

		SetState(UIState.MainMenu);
		//activate camera rotate script
	}
	public void Ingame () {

		SetState(UIState.Ingame);
		Time.timeScale = 1;
	}
	public void QuitGame () {

		Application.Quit();
	}
	private void SwitchCursorState () {

		cursorActive = !cursorActive;
		if(!cursorActive) {

			Cursor.lockState = CursorLockMode.None; 
		}
		else {

			Cursor.lockState = CursorLockMode.Locked; 
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
	public void CheckHealth () 
	{
		healthBar.fillAmount = playerStats.healthPercentage;
	}
	//checks ammo and updates ui    	NOT MADE YET
	public void CheckAmmo (int current, int max) //needs overload so it can receive health or damage
	{
		ammoText.text = current + " / " + max;
	}
	public void CheckWave (int waveNumber) {

		waveText.text = "Wave : " + waveNumber;
	}
}
