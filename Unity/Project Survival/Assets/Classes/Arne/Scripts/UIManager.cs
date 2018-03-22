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
	public RotateCamera camRotateScript;
	public Wave wave;

	//health
	public Image healthBar;

	//ammo
	public Text ammoText;
	public Text waveText;

	//UI stuff
	public List<RectTransform> allMenuItems = new List<RectTransform>();
	public RectTransform mainMenu, pauseMenu, ingame, settings, gameOver, controlInfo; //gonna add more to these 
	public enum UIState {MainMenu, Ingame, GameOver};
	public UIState _UIState;

	public bool paused, settingsActive, controlInfoActive;
	public KeyCode esc;

	bool cursorActive;

	public Image gunIconSlot;

	public Shooting shootScript;

	string infinite;

	public CameraLook camLook;
	public Movement playerMove;

	public GameObject player, cam;

	public Manager manager;
	public Timer timer;


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
		timer.GetComponent<Timer>();
			



		camRotateScript = player.GetComponent<RotateCamera>();



		//gunIconSlot = GameObject.Find("Gun Icon").GetComponent<Image>();

		cursorActive = false;

		CheckUIState();
	}
	private void Update () {

		PressEscape();
	}
	private void CheckUIState () {

		switch (_UIState) {

        case UIState.MainMenu:

			manager.ResetGame();
			playerStats.ResetPosition();
			BlockMovement(true);
			//wave.enabled = false;


			Time.timeScale = 1;


            List<RectTransform> mainmenulist = new List<RectTransform>() {mainMenu};
			EnableMenuItems(mainmenulist);

            break;

        case UIState.Ingame:

			BlockMovement(false);
			camRotateScript.enabled = false;
			wave.spawnEnemies = true;
			//wave.enabled = true;
			timer.SetTimer(true);

			List<RectTransform> ingameList = new List<RectTransform>() {ingame};
			EnableMenuItems(ingame);
			
			SwitchCursorState();
            
            break;

		case UIState.GameOver: //needs to be made

			timer.SetTimer(false);
			manager.ResetGame();

			List<RectTransform> gameOverList = new List<RectTransform>() {gameOver};
			EnableMenuItems(gameOverList);
			SwitchCursorState();
			//wave.enabled = false;

			camRotateScript.enabled = true;

			
			manager.ResetGame();

			break;
        }
	}
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

			//pause game and turns pausemenu on
			//if statements so you can esc out of every window
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
			//pause game and turns pausemenu on
			//if statements so you can esc out of every window
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
		//activate camera rotate script
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
}
