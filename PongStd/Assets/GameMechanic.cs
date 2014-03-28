using UnityEngine;
using System.Collections;

public class GameMechanic : MonoBehaviour {

	public Vector2 direction;
	GameObject blockUP;
	GameObject blockDOWN;
	GameObject gameOverLEFT;
	GameObject gameOverRIGHT;

	GameObject player1;
	GameObject player2;

	// Use this for initialization
	void Start () {
		direction = new Vector2(3f, 3f);
		blockUP = GameObject.Find("BoarderUP");
		blockDOWN = GameObject.Find("BoarderDOWN");
		gameOverLEFT = GameObject.Find("BoarderLEFT");
		gameOverRIGHT = GameObject.Find("BoarderRIGHT");
		
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(direction*Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.name == blockUP.name || collision.name == blockDOWN.name){
			direction.y *= (-1);
		}
		if (collision.name == gameOverLEFT.name || collision.name == gameOverRIGHT.name){
			this.transform.position = new Vector2(0f, 0f);
			if(Random.Range(0,2)==0){
				direction *= (-1);
			}
		}
		if (collision.name == player1.name || collision.name == player2.name){
			direction.x *= (-1);
		}
	}
}
