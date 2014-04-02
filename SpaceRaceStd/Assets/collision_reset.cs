/**
resets an objects position after an collision
 **/


using UnityEngine;
using System.Collections;

public class collision_reset : MonoBehaviour {
	
	private Vector3 startposition;
	private bool waiting;
	private float collisionTime;

	// Use this for initialization
	void Start () {

		startposition = transform.position;
		waiting = false;
		collisionTime = 0;

	}
	
	// Update is called once per frame
	void Update () {
	
		if (waiting && Time.time >= collisionTime + 2) {
			transform.position = startposition;
			waiting = false;
		}

	}

	void OnTriggerEnter2D (Collider2D col) {

		transform.position = new Vector3(0f, -100f, 0f);

		collisionTime = Time.time;
		waiting = true;

	}


}
