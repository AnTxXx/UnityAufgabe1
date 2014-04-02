/**
defines movement and other behavior of comets
 **/

using UnityEngine;
using System.Collections;

public class comet_behavior : MonoBehaviour {

	private Vector3 direction;

	// Use this for initialization
	void Start () {
	
		if (transform.position.x < 0)
			direction = Vector3.right;
		else
			direction = Vector3.left;

	}
	
	// Update is called once per frame
	void Update () {

		// move
		transform.Translate(direction * Time.deltaTime);

		// destroy comet when outside the view
		if (transform.localPosition.x > 10 || transform.localPosition.x < -10) {
			Destroy(this.gameObject);
		}
	
	}
}
