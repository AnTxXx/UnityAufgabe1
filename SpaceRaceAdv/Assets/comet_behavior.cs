/**
defines movement and other behavior of comets
 **/

using UnityEngine;
using System.Collections;

public class comet_behavior : MonoBehaviour {

	public Transform comet;

	private Vector3 direction;
	private float speed;
	private float rotation;
	private float size;

	private float creationTime;

	// Use this for initialization
	void Start () {
	
		if (transform.position.x < 0)
			//direction = Vector3.right;
			direction = new Vector3(1, Random.Range(-0.3f, 0.3f), 0);
		else
			//direction = Vector3.left;
			direction = new Vector3(-1, Random.Range(-0.3f, 0.3f), 0);

		speed = Random.Range(0.5f, 2f);
		rotation = Random.Range(0f, 20f);
		size = Random.Range(0.2f, 0.5f);

		transform.localScale = new Vector3(size, size, 1);

		creationTime = Time.time;

	}
	
	// Update is called once per frame
	void Update () {

		// rotate
		transform.Rotate(new Vector3(0f, 0f, rotation * Time.deltaTime));

		// move
		//transform.Translate(direction * Time.deltaTime * speed);
		transform.position += (direction * Time.deltaTime * speed);

		if (transform.position.y <= -3 && size > transform.position.y + 3.5) {
			size = transform.position.y + 3.5f;
			transform.localScale = new Vector3(size, size, 1);
		}

		if (transform.position.y <= -3.5 || size < 0.1) {
			Destroy(this.gameObject);
		}

		// destroy comet when outside the view
		if (transform.localPosition.x > 10 || transform.localPosition.x < -10 || transform.localPosition.y > 10) {
			Destroy(this.gameObject);
		}
	
	}
	/*
	void OnTriggerEnter2D (Collider2D col) {

		if (Time.time - creationTime > 1) {

			GameObject cometNew;

			cometNew = (GameObject) Instantiate(comet.gameObject, transform.position, transform.localRotation);
			cometNew.GetComponent<comet_behavior>().setSize(size / 2);
			cometNew.GetComponent<comet_behavior>().setDirection(direction * -1);
			//cometNew.renderer.material.color = new Color(100f, 0f, 0f);
			cometNew.GetComponent<comet_behavior>().setColor(new Color(0f, 100f, 0f));

			this.GetComponent<BoxCollider2D>().enabled = false;

			Destroy(this.gameObject);

		}

	}

	public void setSize(float s) {
		s = 0.1f;
		size = 0.1f;
		//transform.localScale = new Vector3(s, s, s);
		this.transform.localScale = new Vector3(0.1f, 0.1f);
		print ("Wird aufgerufen");
	}

	public void setDirection(Vector3 d) {
		direction = Vector3.right;
	}

	public void setColor(Color c) {
		renderer.material.color = c;
	}*/

}
