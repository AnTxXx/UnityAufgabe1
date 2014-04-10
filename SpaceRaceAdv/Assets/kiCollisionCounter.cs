using UnityEngine;
using System.Collections;

public class kiCollisionCounter : MonoBehaviour {

	public float count;

	private Vector3 startposition;

	// Use this for initialization
	void Start () {
	
		count = 0;
		//startposition = transform.position;
		//collisionPosition = new Vector3(0, -100, 0);

	}
	
	// Update is called once per frame
	void Update () {

		if (this.GetComponent<BoxCollider2D>().enabled == false) {
			this.GetComponent<BoxCollider2D>().enabled = true;
		}

		if (count > 0) {
			//print ("Kollision");
			count -= 1;
		}

		//print (count);

	}

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.name != "Up" &&
		    col.gameObject.name != "Down" &&
		    col.gameObject.name != "Left" &&
		    col.gameObject.name != "Right") {

			count += 1.3f;
			this.GetComponent<BoxCollider2D>().enabled = false;

		}

	}

	public float getCount() {
		return count;
	}

}
