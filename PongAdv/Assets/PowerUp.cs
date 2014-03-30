using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public Vector2 direction;
	// Use this for initialization
	GameObject player1;
	GameObject player2;

	GameObject blockUP;
	GameObject blockDOWN;
	GameObject gameOverLEFT;
	GameObject gameOverRIGHT;

	void Start () {
		direction = new Vector2(3f, 3f);
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");

		blockUP = GameObject.Find("BoarderUP");
		blockDOWN = GameObject.Find("BoarderDOWN");
		gameOverLEFT = GameObject.Find("BoarderLEFT");
		gameOverRIGHT = GameObject.Find("BoarderRIGHT");

		if(Random.Range(0,2)==0){
			direction.x *= (-1);
		}
		if(Random.Range(0,2)==0){
			direction.y *= (-1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(direction*Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.name == blockUP.name || collision.name == blockDOWN.name){
			this.audio.Play();
			direction.y *= (-1);
		}
		if (collision.name == gameOverLEFT.name || collision.name == gameOverRIGHT.name){
			direction.x *= (-1);
		}
		if (collision.name == player1.name){
			if(this.name.StartsWith("plus")){
				if(Random.Range(0,2)==1){
					player1.transform.localScale = new Vector3(player1.transform.localScale.x, player1.transform.localScale.y+1f, player1.transform.localScale.z);
				} else {
					player1.GetComponent<MoveScriptP1>().speed.y += 2f;
				}
			}
			if(this.name.StartsWith("minus")){
				if(Random.Range(0,2)==1){
					player1.transform.localScale = new Vector3(player1.transform.localScale.x, player1.transform.localScale.y-1f, player1.transform.localScale.z);
				} else {
					player1.GetComponent<MoveScriptP1>().speed.y -= 2f;
				}
			}
			Destroy(this.gameObject);
		}
		if (collision.name == player2.name){
			if(this.name.StartsWith("plus")){
				if(Random.Range(0,2)==1){
					player2.transform.localScale = new Vector3(player2.transform.localScale.x, player2.transform.localScale.y+1f, player2.transform.localScale.z);
				} else {
					player2.GetComponent<MoveScriptP1>().speed.y += 2f;
				}
			}
			if(this.name.StartsWith("minus")){
				if(Random.Range(0,2)==1){
					player2.transform.localScale = new Vector3(player2.transform.localScale.x, player2.transform.localScale.y-1f, player2.transform.localScale.z);
				} else {
					player2.GetComponent<MoveScriptP1>().speed.y -= 2f;
				}
			}
			Destroy(this.gameObject);
		}
	}
}
