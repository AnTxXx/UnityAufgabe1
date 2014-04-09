using UnityEngine;
using System.Collections;

public class bomb : MonoBehaviour {

	private const float BORDER_Y=5f;


	void Start () {
		if(transform.position.y > 0)
			rigidbody2D.velocity = new Vector2(0, -1.8f);
		else 
			rigidbody2D.velocity = new Vector2(0, 1.8f);
	}


	void Update () {
	
		if( (transform.position.y > BORDER_Y) || (transform.position.y < -BORDER_Y) )
			Destroy(this.gameObject);
	
	}
}
