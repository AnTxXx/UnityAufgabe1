using UnityEngine;
using System.Collections;

public class timelimit : MonoBehaviour {

	public game_state gameState;
	public GameObject timebar;
	public GUIText timeText;
	public GUIText title;

	private int roundTime; // Time of a round in minutes
	private float counter;

	private float lastUpdate;
	private float lossPerSecond;

	private Vector3 timebarOriginalPosition;

	// Use this for initialization
	void Start () {
	
		gameState = (game_state) this.gameObject.GetComponent<game_state>();

		roundTime = 1;
		counter = roundTime * 60;
		lossPerSecond = timebar.transform.localScale.y / (roundTime * -60);
		lastUpdate = Time.time;

		timebarOriginalPosition = timebar.transform.position;
		timeText.text = counter.ToString();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (gameState.mode == "play" && Time.time >= lastUpdate + 1 && counter > 0) {

			timebar.transform.Translate(new Vector3(0, lossPerSecond, 0));
			lastUpdate = Time.time;
			counter--;
			timeText.text = counter.ToString();

			if (counter == 0) {

				gameState.setMode("finish");

			}

		}

	}

	public void reset() {

		counter = roundTime * 60;
		lastUpdate = Time.time;
		timebar.transform.position = timebarOriginalPosition;
		timeText.text = counter.ToString();

	}

}
