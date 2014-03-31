using UnityEngine;
using System.Collections;

public class GameMechanic : MonoBehaviour {

	public Vector2 direction;
	Vector3 directionRotate;
	GameObject blockUP;
	GameObject blockDOWN;
	GameObject gameOverLEFT;
	GameObject gameOverRIGHT;

	GameObject player1;
	GameObject player2;

	GameObject cam;
	GameObject wholeScene;

	public GameObject plus;
	public GameObject minus;

	public int maxGameTime = 165;

	int rotateYa = 0;
	float moveYa = 0f;

	GameObject P1best;
	GameObject P1act;
	GameObject P2best;
	GameObject P2act;

	bool timeIsUp = false;
	bool leaderP1 = false;

	GameObject OutRestTime;
	GameObject GameOutput;

	float P1points = 0f, P1bestpoints = 0f, P2points = 0f, P2bestpoints = 0f;

	// Use this for initialization
	void Start () {
		direction = new Vector2(3f, 3f);
		blockUP = GameObject.Find("BoarderUP");
		blockDOWN = GameObject.Find("BoarderDOWN");
		gameOverLEFT = GameObject.Find("BoarderLEFT");
		gameOverRIGHT = GameObject.Find("BoarderRIGHT");
		
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");

		cam = GameObject.Find("SeCamera");

		directionRotate = new Vector3(0f,0f,1f);

		P1best = GameObject.Find("BestSurvivalP1");
		P1act = GameObject.Find("ActSurvivalP1");
		P2best = GameObject.Find("BestSurvivalP2");
		P2act = GameObject.Find("ActSurvivalP2");

		OutRestTime = GameObject.Find("RemainGameTime");
		GameOutput = GameObject.Find("GameOutput");

		P1points = 0;
		P1bestpoints = 0;
		P2points = 0;
		P2bestpoints = 0;

		P1best.guiText.text = "Player1 Best Time: " + 0f;
		P2best.guiText.text = "Player2 Best Time: " + 0f;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(direction*Time.deltaTime);
		int randy = Random.Range(0,1001);
		/*if(randy<8){
			if(directionRotate.z<0){
				directionRotate.z -= 1f;
			} else {
				directionRotate.z += 1f;
			}
			Debug.Log("Levelspeed changed to: " + directionRotate.z);
		}
		if(randy<3){
			directionRotate *= (-1);
		}
		if(randy<1){
			rotateZ = !rotateZ;
			if(!rotateZ) directionRotate.y = directionRotate.z;
			else directionRotate.z = directionRotate.y;
			Debug.Log("Rotation changed");
		}
		cam.transform.LookAt(new Vector3(0f,0f,0f));
		// Where to rotate
		if(rotateZ) cam.transform.RotateAround(this.transform.position, Vector3.left, directionRotate.z*Time.deltaTime);
		else {
			cam.transform.RotateAround(this.transform.position, Vector3.up, directionRotate.z*Time.deltaTime);
		}
		*/
		P1act.guiText.text = "Player1 Actual Time: " + (Time.time - P1points).ToString ("F2");
		P2act.guiText.text = "Player2 Actual Time: " + (Time.time - P2points).ToString ("F2");
		if((maxGameTime - Time.time) >= 0) OutRestTime.guiText.text = "Remaining Time Left: " + (maxGameTime - Time.time).ToString("F2");
		if((maxGameTime - Time.time) < 0) {
			timeIsUp = true;
			OutRestTime.guiText.text = "Time is up ... final death of follower finishes round";
		}
		if(P1bestpoints != 0f || P2bestpoints != 0f){
			if(P1bestpoints>P2bestpoints){
				P1best.guiText.fontSize = 20;
				P2best.guiText.fontSize = 0;
				P1act.guiText.fontSize = 0;
				P2act.guiText.fontSize = 0;
				leaderP1 = true;
			} else {
				P2best.guiText.fontSize = 20;
				P1best.guiText.fontSize = 0;
				P1act.guiText.fontSize = 0;
				P2act.guiText.fontSize = 0;
				leaderP1 = false;
			}
			if((Time.time - P1points) > P2bestpoints && P2bestpoints != 0){
				P1act.guiText.fontSize = 20;
				P2act.guiText.fontSize = 0;
				P2best.guiText.fontSize = 0;
				P1best.guiText.fontSize = 0;
				leaderP1 = true;
			}
			if((Time.time - P2points) > P1bestpoints && P1bestpoints != 0){
				P2act.guiText.fontSize = 20;
				P1act.guiText.fontSize = 0;
				P2best.guiText.fontSize = 0;
				P1best.guiText.fontSize = 0;
				leaderP1 = false;
			}
		}
		if(randy<8){
			if(moveYa<0){
				moveYa -= 1f;
			} else {
				moveYa += 1f;
			}
			Debug.Log("Levelspeed changed to: " + moveYa);
		}
		if(randy<3){
			moveYa *= (-1);
		}
		if(randy<1){
			rotateYa++;
			if(rotateYa==3) rotateYa=0;
			Debug.Log("Rotation changed");
		}
		if(rotateYa%3==0){
			cam.transform.RotateAround(this.transform.position, Vector3.left, moveYa*Time.deltaTime);
		} else if(rotateYa%3==1){
			cam.transform.RotateAround(this.transform.position, Vector3.up, moveYa*Time.deltaTime);
		} else {
			cam.transform.RotateAround(this.transform.position, Vector3.forward, moveYa*Time.deltaTime);
		}
		cam.transform.LookAt(new Vector3(0f,0f,0f));
		int randy2 = Random.Range(1,1001);
		if(randy2<3){
			if(randy2==2) Instantiate(plus);
			if(randy2==1) Instantiate(minus);
			if(randy2==0) {
				Instantiate(plus);
				Instantiate(minus);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.name == blockUP.name || collision.name == blockDOWN.name){
			this.audio.Play();
			direction.y *= (-1);
		}
		if (collision.name == gameOverLEFT.name || collision.name == gameOverRIGHT.name){
			this.transform.position = new Vector2(0f, 0f);
			if(Random.Range(0,2)==0){
				direction *= (-1);
			}
			direction = new Vector2(3f, 3f);
			if(collision.name == gameOverLEFT.name){
				float zws = Time.time - P1points;
				if(zws > P1bestpoints){
					P1bestpoints = zws;
					P1best.guiText.text = "Player1 Best Time: " + P1bestpoints;
					player1.transform.localScale = new Vector3(1f,3f,1f);
					player1.transform.position = new Vector3(-15f,0f,0f);
					player1.GetComponent<MoveScriptP1>().speed = new Vector3(0f, 10f);
				}
				P1points = Time.time;

				if(timeIsUp && !leaderP1){
					GameOutput.guiText.text = "Game Over! Player2 has won.";
				}
			}
			if(collision.name == gameOverRIGHT.name){
				float zws = Time.time - P2points;
				if(zws > P2bestpoints){
					P2bestpoints = zws;
					P2best.guiText.text = "Player2 Best Time: " + P2bestpoints;
					player2.transform.localScale = new Vector3(1f,3f,1f);
					player2.transform.position = new Vector3(15f,0f,0f);
					player2.GetComponent<MoveScriptP1>().speed = new Vector3(0f, 10f);
				}
				P2points = Time.time;

				if(timeIsUp && leaderP1){
					GameOutput.guiText.text = "Game Over! Player1 has won.";
				}
			}
		}
		if (collision.name == player1.name || collision.name == player2.name){
			this.audio.Play();
			direction.x *= (-1);
			if(direction.x > 0){
				direction.x++;
			}else {
				direction.x--;
			}
			if(direction.y > 0){
				direction.y++;
			} else {
				direction.y--;
			}
			Debug.Log("Ballspeed changed to: " + direction.x);
		}
	}
}
