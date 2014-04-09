using UnityEngine;
using System.Collections;

public class enemyKI : MonoBehaviour {

	private int moveCounter=0, bombCounter;
	private const float BORDER_X=6f, BORDER_Y_TOP=2.8f, BORDER_Y_BOTTOM=-4f;
	private bool horizontal=true;
	private Vector3 startPos, endPos, startRot, endRot; //From/To for animation

	private float startTime;

	public GameObject level;
	public Transform bomb; 


	void Start () {
		bombCounter=Random.Range(50, 350);
		rigidbody2D.velocity = new Vector2(Random.Range(-2, 2), 0);
		startPos=new Vector3(0,0,1);
	}

	void Update () {

		//Change to other axis
		if(level.GetComponent<level>().isHorizontal() != horizontal) {
			animatedMove();
		}


		else {
			//Move horizontal
			if(horizontal) 
			{
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

			//Move vertical
			} else {

				if( (transform.position.y > BORDER_Y_TOP) || (transform.position.y < BORDER_Y_BOTTOM) ) {
					moveCounter=0;
				}
				
				if(moveCounter <= 0) {
					moveCounter=Random.Range(0, 600);
					
					if(rigidbody2D.velocity.y > 0) {
						rigidbody2D.velocity = new Vector2(0, -2);
					} else {
						rigidbody2D.velocity = new Vector2(0, 2);
					}
					
					
				}

			}


			// Dropping the load, Bi4tch!
			if(bombCounter==0) {
				bombCounter=Random.Range(50, 350);
				Instantiate(bomb, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
			}
			
			
			bombCounter--;
			moveCounter--;

		}

	}

	void animatedMove() {


		if(startPos.z==1) {
			startPos = transform.position;
			startTime = Time.time;

			//startRot = new Vector3(0, 0, 0);
			endRot = new Vector3(0, 0, 90);

			rigidbody2D.velocity = new Vector2(0, 0);
			moveCounter=0;

			if(horizontal) {
				if(transform.position.y>0) {
					endPos=new Vector3(-5.9f, 0, 0);
				} else {
					endPos=new Vector3(5.9f, 0, 0);
					endRot = new Vector3(0, 0, 270);
				}
			} else {
				if(transform.position.x>0) {
					endPos=new Vector3(0, 4f, 0);
					endRot = new Vector3(0, 0, 0);
				} else {
					endPos=new Vector3(0, -4f, 0);
					endRot = new Vector3(0, 0, 180);
				}
			}

			//transform.Rotate(new Vector3(0, 0, 90));
		}

		transform.position = Vector3.Slerp(startPos, endPos, (Time.time - startTime));
		
		transform.Rotate(new Vector3(0,0,10));



		if((Time.time - startTime) >= 1) {
			startPos=new Vector3(0,0,1);
			horizontal = level.GetComponent<level>().isHorizontal();
			transform.eulerAngles = endRot;
			Debug.Log("-- - - Finished Moving ");
		}


	}
}
