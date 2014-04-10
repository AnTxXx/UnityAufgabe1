using UnityEngine;
using System.Collections;

public class game_state : MonoBehaviour {

	public string mode; // "start", "play" or "finish"

	public GameObject player1;
	public GameObject player2;
	public GameObject timebar;
	public GUIText guiTitle;
	public GUIText guiPlayer1Mode;
	public GUIText guiPlayer2Mode;
	public GUIText guiContinue;

	private int scorePlayer1;
	private int scorePlayer2;

	private bool showContinue;
	private float lastBlinkChange;

	private int player1mode = 0;
	private int player2mode = 1;

	// Use this for initialization
	void Start () {
	
		setMode("start");

	}
	
	// Update is called once per frame
	void Update () {
	
		// start game
		if (mode == "start" && Input.GetKeyDown("space")) {
			setMode("play");
		}

		// change 1st player's mode
		if (mode == "start" && (Input.GetKeyDown("1") || Input.GetKeyDown(KeyCode.Keypad1) )) {

			player1mode++;
			if (player1mode > 2)
				player1mode = 0;

			if (player1mode == 0) {
				guiPlayer1Mode.text = "Player1: WASD keys (press 1 to change)";
				player1.GetComponent<movement>().ki = false;
				player1.GetComponent<movement>().keyUp = "w";
				player1.GetComponent<movement>().keyDown = "s";
				player1.GetComponent<movement>().keyLeft = "a";
				player1.GetComponent<movement>().keyRight = "d";
			} else if (player1mode == 1) {
				guiPlayer1Mode.text = "Player1: Arrow keys (press 1 to change)";
				player1.GetComponent<movement>().ki = false;
				player1.GetComponent<movement>().keyUp = "up";
				player1.GetComponent<movement>().keyDown = "down";
				player1.GetComponent<movement>().keyLeft = "left";
				player1.GetComponent<movement>().keyRight = "right";
			} else if (player1mode == 2) {
				guiPlayer1Mode.text = "Player1: CPU (press 1 to change)";
				player1.GetComponent<movement>().ki = true;
			}
		}

		// change 2nd player's mode
		if (mode == "start" && (Input.GetKeyDown("2") || Input.GetKeyDown(KeyCode.Keypad2) )) {
			
			player2mode++;
			if (player2mode > 2)
				player2mode = 0;
			
			if (player2mode == 0) {
				guiPlayer2Mode.text = "Player2: WASD keys (press 2 to change)";
				player2.GetComponent<movement>().ki = false;
				player2.GetComponent<movement>().keyUp = "w";
				player2.GetComponent<movement>().keyDown = "s";
				player2.GetComponent<movement>().keyLeft = "a";
				player2.GetComponent<movement>().keyRight = "d";
			} else if (player2mode == 1) {
				guiPlayer2Mode.text = "Player2: Arrow keys (press 2 to change)";
				player2.GetComponent<movement>().ki = false;
				player2.GetComponent<movement>().keyUp = "up";
				player2.GetComponent<movement>().keyDown = "down";
				player2.GetComponent<movement>().keyLeft = "left";
				player2.GetComponent<movement>().keyRight = "right";
			} else if (player2mode == 2) {
				guiPlayer2Mode.text = "Player2: CPU (press 2 to change)";
				player2.GetComponent<movement>().ki = true;
			}
		}

		// reset game
		if ((mode == "finish" && Input.GetKeyDown("space")) ||
		    (mode == "play" && Input.GetKeyDown("escape")) ) {
			setMode("start");
		}

		// blinking continue
		if (showContinue && lastBlinkChange + 1 < Time.time) {
			guiContinue.enabled = !guiContinue.enabled;
			lastBlinkChange = Time.time;
		}

	}

	public void setMode(string newMode) {

		if (newMode == "start") {

			guiTitle.text = "Space Race 2014";
			((movement) player1.GetComponent<movement>()).score = 0;
			((movement) player2.GetComponent<movement>()).score = 0;
			((movement) player1.GetComponent<movement>()).scoreGUI.text = "0";
			((movement) player2.GetComponent<movement>()).scoreGUI.text = "0";
			if (Time.time > 3) {
				player1.transform.position = ((movement) player1.GetComponent<movement>()).startposition;
				player2.transform.position = ((movement) player2.GetComponent<movement>()).startposition;
			}

			mode = "start";

			//timebar.renderer.enabled = false;
			guiTitle.enabled = true;
			guiPlayer1Mode.enabled = true;
			guiPlayer2Mode.enabled = true;

			showContinue = true;
			lastBlinkChange = Time.time;

			((timelimit) this.GetComponent<timelimit>()).reset();

		} else if (newMode == "play") {

			mode = "play";

			//timebar.renderer.enabled = true;
			guiTitle.enabled = false;
			guiPlayer1Mode.enabled = false;
			guiPlayer2Mode.enabled = false;

			showContinue = false;
			guiContinue.enabled = false;
			
		} else if (newMode == "finish") {

			scorePlayer1 = ((movement) GameObject.Find("Player1").GetComponent<movement>()).score;
			scorePlayer2 = ((movement) GameObject.Find("Player2").GetComponent<movement>()).score;
			
			if (scorePlayer1 > scorePlayer2)
				guiTitle.text = "Blue wins!";
			else if (scorePlayer2 > scorePlayer1)
				guiTitle.text = "Red wins!";
			else
				guiTitle.text = "It's a tie!";

			mode = "finish";

			//timebar.renderer.enabled = false;
			guiTitle.enabled = true;
			guiPlayer1Mode.enabled = false;
			guiPlayer2Mode.enabled = false;

			showContinue = true;
			lastBlinkChange = Time.time;
			
		}

	}

}
