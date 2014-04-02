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

		// reset game
		if (mode == "finish" && Input.GetKeyDown("space")) {
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

			guiTitle.text = "Space Race";
			((movement) player1.GetComponent<movement>()).score = 0;
			((movement) player2.GetComponent<movement>()).score = 0;
			((movement) player1.GetComponent<movement>()).scoreGUI.text = "0";
			((movement) player2.GetComponent<movement>()).scoreGUI.text = "0";
			if (Time.time > 10) {
				player1.transform.position = ((movement) player1.GetComponent<movement>()).startposition;
				player2.transform.position = ((movement) player2.GetComponent<movement>()).startposition;
			}

			mode = "start";

			timebar.renderer.enabled = false;
			guiTitle.enabled = true;
			guiPlayer1Mode.enabled = true;
			guiPlayer2Mode.enabled = true;

			showContinue = true;
			lastBlinkChange = Time.time;

		} else if (newMode == "play") {

			mode = "play";

			timebar.renderer.enabled = true;
			guiTitle.enabled = false;
			guiPlayer1Mode.enabled = false;
			guiPlayer2Mode.enabled = false;

			((timelimit) this.GetComponent<timelimit>()).reset();

			showContinue = false;
			guiContinue.enabled = false;
			
		} else if (newMode == "finish") {

			scorePlayer1 = ((movement) GameObject.Find("Player1").GetComponent<movement>()).score;
			scorePlayer2 = ((movement) GameObject.Find("Player2").GetComponent<movement>()).score;
			
			if (scorePlayer1 > scorePlayer2)
				guiTitle.text = "Player 1 wins!";
			else if (scorePlayer2 > scorePlayer1)
				guiTitle.text = "Player 2 wins!";
			else
				guiTitle.text = "It's a tie!";

			mode = "finish";

			timebar.renderer.enabled = false;
			guiTitle.enabled = true;
			guiPlayer1Mode.enabled = false;
			guiPlayer2Mode.enabled = false;

			showContinue = true;
			lastBlinkChange = Time.time;
			
		}

	}

}
