using UnityEngine;
using System.Collections;

public class bomb : MonoBehaviour {

	private const float BORDER_Y=5f;
	public GameObject level;

	void Start () {

		level = GameObject.Find("LevelScript");


		if(level.GetComponent<level>().isHorizontal()) {

			if(transform.position.y > 0)
				rigidbody2D.velocity = new Vector2(0, -1.8f);
			else 
				rigidbody2D.velocity = new Vector2(0, 1.8f);

		} else {

			if(transform.position.x > 0)
				rigidbody2D.velocity = new Vector2(-1.8f, 0);
			else 
				rigidbody2D.velocity = new Vector2(1.8f, 0);

		}

	}


	void Update () {
	
		transform.Rotate(new Vector3(0,0,1));

		if( (transform.position.y > BORDER_Y) || (transform.position.y < -BORDER_Y) )
			Destroy(this.gameObject);
	
	}
}
