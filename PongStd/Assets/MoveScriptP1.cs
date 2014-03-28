using UnityEngine;
using System.Collections;

public class MoveScriptP1 : MonoBehaviour {

	GameObject ball;

	GameObject blockUP;
	GameObject blockDOWN;

	bool moveUP = true;
	bool moveDOWN = true;

	Vector2 speed = new Vector2(0f, 3f);

	public bool amIplayer1 = true;
	public bool KI = false;

	// Use this for initialization
	void Start () {
		ball = GameObject.Find("Ball");

		blockUP = GameObject.Find("BoarderUP");
		blockDOWN = GameObject.Find("BoarderDOWN");
	}
	
	// Update is called once per frame
	void Update () {
		if(KI){
			if((this.transform.position.y < ball.transform.position.y) && moveUP){
				this.transform.Translate(speed*Time.deltaTime);
			}
			if((this.transform.position.y > ball.transform.position.y) && moveDOWN){
				this.transform.Translate(speed*(-1)*Time.deltaTime);
			}
		}
		else {
			if(!amIplayer1){
				if(Input.GetKey(KeyCode.UpArrow) && moveUP){
					this.transform.Translate(speed*Time.deltaTime);
				}
				if(Input.GetKey(KeyCode.DownArrow) && moveDOWN){
					this.transform.Translate(speed*(-1)*Time.deltaTime);
				}
			}
			else {
				if(Input.GetKey(KeyCode.W) && moveUP){
					this.transform.Translate(speed*Time.deltaTime);
				}
				if(Input.GetKey(KeyCode.S) && moveDOWN){
					this.transform.Translate(speed*(-1)*Time.deltaTime);
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision){
		if(collision.name == blockUP.name){
			moveUP = false;
		}
		if(collision.name == blockDOWN.name){
			moveDOWN = false;
		}
	}

	void OnTriggerExit2D(Collider2D collision){
		if(collision.name == blockUP.name){
			moveUP = true;
		}
		if(collision.name == blockDOWN.name){
			moveDOWN = true;
		}
	}
}
