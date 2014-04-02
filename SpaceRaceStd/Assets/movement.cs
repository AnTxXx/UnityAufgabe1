using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	public game_state gameState;

	public string keyUp;
	public string keyDown;

	public GUIText scoreGUI;

	public int score;
	public Vector3 startposition;

	// Use this for initialization
	void Start () {
	
		gameState = (game_state) GameObject.Find("Control").GetComponent<game_state>();

		score = 0;
		startposition = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		if (gameState.mode == "play") {

			// move up
			if (Input.GetKey(keyUp)) {
				transform.Translate(Vector3.up * Time.deltaTime);
				// score a point
				if (transform.position.y >= 7.8) {
					transform.position = startposition;
					score++;
					scoreGUI.text = score.ToString();
				}
			}

			// move down
			if (Input.GetKey(keyDown)) {
				if (transform.position.y >= startposition.y) {
					transform.Translate(Vector3.down * Time.deltaTime);
				}
			}

		}

	}
	
}
