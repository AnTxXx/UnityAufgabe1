using UnityEngine;
using System.Collections;

public class enemyKI : MonoBehaviour {

	private int moveCounter=0, bombCounter;
	private const float BORDER_X=6f;
	public Transform bomb; 

	void Start () {
		bombCounter=Random.Range(50, 350);
		rigidbody2D.velocity = new Vector2(Random.Range(-2, 2), 0);
	}

	void Update () {


		if( (transform.position.x > BORDER_X) || (transform.position.x < -BORDER_X) ) {
			moveCounter=0;
		}

		if(moveCounter <= 0) {
			moveCounter=Random.Range(0, 600);

			if(rigidbody2D.velocity.x > 0) {
				rigidbody2D.velocity = new Vector2(-2, 0);
			} else {
				rigidbody2D.velocity = new Vector2(2, 0);
			}


		}

		if(bombCounter==0) {
			bombCounter=Random.Range(50, 350);
			Instantiate(bomb, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
		}


		bombCounter--;
		moveCounter--;

	}
}
