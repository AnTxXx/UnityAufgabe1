using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {


	public KeyCode moveUp, moveDown, moveLeft, moveRight, space;

	private Vector2 speed;
	private int turbo = 1, maxSpeed=3;

	public GameObject level;

	
	private const float BORDER_Y=3f, BORDER_X=6f;

	public GUIText guiPoints, guiLives;
	private int points=0, lives=3;

	private int timeout = 0;
	
	// Use this for initialization
	void Start () {
		speed = new Vector2(0, 0);
		Debug.Log("Init Player CTRL");

	}
	
	// Update is called once per frame
	void Update () {

		// Set the speed
		if(speed.x > 0)
			speed.x-=1;
		if(speed.x < 0)
			speed.x+=1;


		if(speed.y > 0)
			speed.y-=1;
		if(speed.y < 0)
			speed.y+=1;

		turbo=1;
		if(Input.GetKey(space)) {
			turbo=3;
		}


		if((Input.GetKey(moveUp)) && (speed.y < maxSpeed*turbo)) {
			speed.y+=2*turbo;
			transform.localEulerAngles = new Vector3(0, 0, 0);
		}
		if((Input.GetKey(moveDown)) && (speed.y > -maxSpeed*turbo)) {
			speed.y-=2*turbo;
			transform.localEulerAngles = new Vector3(0, 0, 180);
		}
		if((Input.GetKey(moveRight)) && (speed.x < maxSpeed*turbo)) {
			speed.x+=2*turbo;
			transform.localEulerAngles = new Vector3(0, 0, 270);
		}
		if((Input.GetKey(moveLeft)) && (speed.x > -maxSpeed*turbo)) {
			speed.x-=2*turbo;
			transform.localEulerAngles = new Vector3(0, 0, 90);
		}


		// Check edge-collision
		if(transform.position.y > BORDER_Y) {
			transform.position = new Vector3(transform.position.x, BORDER_Y, transform.position.z);
		} else if(transform.position.y < -BORDER_Y) {
			transform.position = new Vector3(transform.position.x, -BORDER_Y, transform.position.z);
		}

		if(transform.position.x > BORDER_X) {
			transform.position = new Vector3(BORDER_X, transform.position.y, transform.position.z);
		} else if(transform.position.x < -BORDER_X) {
			transform.position = new Vector3(-BORDER_X, transform.position.y, transform.position.z);
		}

		// Move the ship
		rigidbody2D.velocity = speed;

	}

	void OnTriggerEnter2D(Collider2D collision){
		Debug.Log("-- Collision 2");

		if(collision.gameObject.tag.Equals("brick")) {
			Debug.Log("-- Brick collected");
			points+=100;
			guiPoints.text=points.ToString();
			Destroy(collision.gameObject);

			/*
			GameObject[] gos;
			gos = GameObject.FindGameObjectsWithTag("brick");

			if(gos.Length==0) {
				guiPoints.text="Level Completed";
			}
			*/
			level.GetComponent<level>().removeBrick();


		}

		if(collision.gameObject.tag.Equals("bomb")) {
			Debug.Log("-- Ship destroyed");
			lives--;
			guiLives.text=lives.ToString() + " Lives";
			transform.position = new Vector3(0, 0, 0);

			if(lives>1)
				level.GetComponent<level>().setPauseCounter(50, lives.ToString() + " lives left!");
			else if (lives==1) {
				level.GetComponent<level>().setPauseCounter(50, lives.ToString() + " life left!");
			} else {
				level.GetComponent<level>().setGameOver();
			}

			
		}

	}


}
