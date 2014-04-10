﻿using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {


	public KeyCode moveUp, moveDown, moveLeft, moveRight, space;

	private Vector2 speed;
	private int turbo = 1, maxSpeed=3;
	private int flameCounter=0;
	private float dontDieTwiceTimer;

	public GameObject level, background, explosion, flareFlash, flareFire, flareStream;


	
	private const float BORDER_Y=3f, BORDER_X=6f;

	public GUIText guiPoints, guiLives;
	private int points=0, lives=3;


	// Use this for initialization
	void Start () {
		speed = new Vector2(0, 0);
		Debug.Log("Init Player CTRL");
		background = GameObject.Find("background");
		dontDieTwiceTimer=Time.time-2;
	}
	
	// Update is called once per frame
	void Update () {

		background.transform.position = new Vector3((-0.07f)*transform.position.x, (-0.07f)*transform.position.y, 0);


		if((flameCounter--)>0)  {
			flareFlash.SetActive(true);
			flareFire.SetActive(true);
		} else {
			flareFlash.SetActive(false);
			flareFire.SetActive(false);
		}


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

			flameCounter=10;
		}
		if((Input.GetKey(moveDown)) && (speed.y > -maxSpeed*turbo)) {
			speed.y-=2*turbo;
			transform.localEulerAngles = new Vector3(0, 0, 180);

			flameCounter=10;
		}
		if((Input.GetKey(moveRight)) && (speed.x < maxSpeed*turbo)) {
			speed.x+=2*turbo;
			transform.localEulerAngles = new Vector3(0, 0, 270);

			flameCounter=10;
		}
		if((Input.GetKey(moveLeft)) && (speed.x > -maxSpeed*turbo)) {
			speed.x-=2*turbo;
			transform.localEulerAngles = new Vector3(0, 0, 90);

			flameCounter=10;
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

		if(collision.gameObject.tag.Equals("bomb") || collision.gameObject.tag.Equals("obstacle")) {

			if((Time.time-dontDieTwiceTimer)>2) {
				dontDieTwiceTimer=Time.time;

				Debug.Log("-- Ship destroyed");

				Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

				lives--;
				guiLives.text=lives.ToString() + " Lives";
				transform.GetComponent<SpriteRenderer>().enabled=false;
				flareStream.SetActive(false);

				Invoke("died", 2);
			}
		}
	}

	void died() {
	
		if(lives>1)
			level.GetComponent<level>().setPauseCounter(50, lives.ToString() + " lives left!");
		else if (lives==1) {
			level.GetComponent<level>().setPauseCounter(50, lives.ToString() + " life left!");
		} else {
			level.GetComponent<level>().setGameOver();
		}

		transform.GetComponent<SpriteRenderer>().enabled=true;
		flareStream.SetActive(true);
		transform.position = new Vector3(0, 0, 0);

		// Remove all bombs after collision to prevent death on respawn
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("bomb");
		foreach (GameObject go in gos) {
			Destroy(go);
		}

	}


}
