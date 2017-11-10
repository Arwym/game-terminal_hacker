using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {
	// Enums
	enum Screen { MainMenu, Password, Win, GameOver };

	// Structs
	struct Level {
		public string title;
		public string[] passwords;
		public int maxTries;
		public string slogan;

		public Level(string t, string[]pwds, int mt, string s) {
			title = t;
			passwords = pwds;
			maxTries = mt;
			slogan = s;
		}
	}

	// Game configuration data
	const string MENU_HINT = "\nYou may type \"menu\" at any time.";
	Level LEVEL1 = new Level(
		"Depto. de Hacienda de Puerto Rico", 
		new string[] { "admin", "password", "hacienda", "test", "taxes" }, 
		5,
		"Integridad - Lealtad - Justicia"
	);
	Level LEVEL2 = new Level(
		"Evertec, Inc. [Intranet]", 
		new string[] { "software", "transaction", "athmovil", "popular", "technology" }, 
		7,
		"Transaction Solutions Simplified"
	);
	Level LEVEL3 = new Level(
		"NASA", 
		new string[] { "environment", "telescope", "astronauts", "starfield", "exploration" }, 
		3,
		"For the Benefit of All"
	);

	// Game state
	Screen currentScreen = Screen.MainMenu;
	int currentLevel;
	string currentPassword;
	int numTries = 0;
	int maxTries = 5;

	// Use this for initialization
	void Start () {
		ShowMainMenu ();
	}

	void OnUserInput(string input) {
		Debug.Log(input);

		input = input.ToLower ().Trim ();

		if (input == "menu" || input == "play" || input == "start" || input == "main")
			ShowMainMenu ();

		else if (currentScreen == Screen.MainMenu) {
			HandleMainMenu (input);
		}

		else if (currentScreen == Screen.Password) {
			CheckPassword (input);
		}
	}

	void ShowMainMenu(){
		currentScreen = Screen.MainMenu;

		Terminal.ClearScreen ();

		string greeting;
		greeting = "Welcome, N00B Hacker!";
		Terminal.WriteLine (greeting + "\n");

		Terminal.WriteLine ("Please select a place to start \nyour journey into hacking:\n");
		Terminal.WriteLine ("1) " + LEVEL1.title + " \n2) " + LEVEL2.title + " \n3) " + LEVEL3.title + "\n");
		Terminal.WriteLine ("Enter your selection's number below:");
	}

	void HandleMainMenu (string input)
	{
		bool isValidLevel = (input == "1" || input == "2" || input == "3");
		if (isValidLevel) {
			currentLevel = int.Parse (input);
			StartLevel ();				
		} else if (input == "exit" || input == "quit" || input == "close") {
			Application.Quit ();
			Terminal.WriteLine ("Web user: you can close the browser tab at any time.");
		} else if (input == "valar morghulis")
			Terminal.WriteLine ("Valar Dohaeris.");
		else {
			Terminal.WriteLine ("Please choose a valid option:");
			Debug.Log("Invalid level number.");
		}
	}

	void StartLevel() {
		// Update game state
		currentScreen = Screen.Password;

		// Level info
		string title = "";

		// Level setup
		SetupLevel (ref title);

		// Show screen
		Terminal.ClearScreen ();
		Terminal.WriteLine (title);
		Terminal.WriteLine (MENU_HINT);
		Terminal.WriteLine ("\nPlease enter password. \nHint: " + currentPassword.Anagram());
	}

	void CheckPassword(string input) {
		numTries++;
		if (input == currentPassword) {
			WinGame ();
		} else {
			if (numTries == maxTries) {
				GameOver ();
			} else {
				Terminal.WriteLine ("\nWrong password. " + (maxTries - numTries).ToString() + " tries left. \nHint: " + currentPassword.Anagram());
			}
		}
	}

	void SetupLevel (ref string title)
	{
		numTries = 0;
		switch (currentLevel) {
		case 1:
			title = LEVEL1.title.ToUpper ();
			currentPassword = LEVEL1.passwords [Random.Range (0, LEVEL1.passwords.Length)];
			maxTries = LEVEL1.maxTries;
			break;
		case 2:
			title = LEVEL2.title.ToUpper ();
			currentPassword = LEVEL2.passwords [Random.Range (0, LEVEL2.passwords.Length)];
			maxTries = LEVEL2.maxTries;
			break;
		case 3:
			title = LEVEL3.title.ToUpper ();
			currentPassword = LEVEL3.passwords [Random.Range (0, LEVEL3.passwords.Length)];
			maxTries = LEVEL3.maxTries;
			break;
		default:
			Debug.LogError ("Invalid level number in password screen.");
			break;
		}
	}

	void WinGame ()
	{
		currentScreen = Screen.Win;
		Terminal.ClearScreen ();
		Terminal.WriteLine ("CORRECT!\n");
		ShowLevelReward ();
		Terminal.WriteLine(MENU_HINT);
	}

	void ShowLevelReward()
	{
		switch (currentLevel) {
			case 1:
				Terminal.WriteLine ("\"" + LEVEL1.slogan + "\"");
				break;
			case 2:
				Terminal.WriteLine ("\"" + LEVEL2.slogan + "\"");
				break;
			case 3:
				Terminal.WriteLine ("\"" + LEVEL3.slogan + "\"");
				break;
			default:
				Debug.LogError ("Invalid level reached.");
				break;
		}
	}

	void GameOver()
	{
		currentScreen = Screen.GameOver;
		Terminal.ClearScreen();
		Terminal.WriteLine ("GAME OVER.");
		Terminal.WriteLine (MENU_HINT);
	}
}
