using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {
	// Enums
	enum Screen { MainMenu, Password, Win };

	// Game state
	Screen currentScreen = Screen.MainMenu;
	int currentLevel;

	// Use this for initialization
	void Start () {
		ShowMainMenu ();
	}

	void OnUserInput(string input) {
		print(input);

		input = input.ToLower ().Trim ();

		if (input == "menu")
			ShowMainMenu ();

		else if (currentScreen == Screen.MainMenu) {
			HandleMainMenu (input);
		}

		else if (currentScreen == Screen.Password) {
			// soon
		}
	}

	void ShowMainMenu(){
		currentScreen = Screen.MainMenu;

		Terminal.ClearScreen ();

		string greeting;
		greeting = "Welcome, N00B Hacker!";
		Terminal.WriteLine (greeting + "\n");

		Terminal.WriteLine ("Please select a place to start \nyour journey into hacking:\n");
		Terminal.WriteLine ("1) Depto. de Hacienda de Puerto Rico \n2) Evertec, Inc. \n3) NASA\n");
		Terminal.WriteLine ("Enter your selection's number below:");
	}

	void HandleMainMenu (string input)
	{
		switch (input) {
		case "1":
			StartLevel (1);
			break;
		case "2":
			StartLevel (2);
			break;
		case "3":
			StartLevel (3);
			break;
		case "menu":
			ShowMainMenu ();
			break;
		case "valar morghulis":
			Terminal.WriteLine ("Valar Dohaeris.");
			break;
		default:
			Terminal.WriteLine ("Please choose a valid option:");
			print ("Invalid level number.");
			break;
		}
	}

	void StartLevel(int level) {
		// Update game state
		currentScreen = Screen.Password;
		currentLevel = level;

		// Level info
		string title = "";

		switch (level) {
			case 1:
				title = "DEPARTAMENTO DE HACIENDA\n DE PUERTO RICO";
				break;
			case 2:
				title = "EVERTEC, INC. [INTRANET]";
				break;
			case 3:
				title = "NASA";
				break;
		}

		Terminal.ClearScreen ();
		Terminal.WriteLine (title + "\n");
		Terminal.WriteLine ("Please enter password:");
	}
}
