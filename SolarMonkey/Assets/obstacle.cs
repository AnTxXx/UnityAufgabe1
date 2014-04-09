using UnityEngine;
using System.Collections;

public class obstacle : MonoBehaviour {

	private bool horizontal=true;

	void Start() {

	}
	
	void OnTriggerEnter2D(Collider2D collision){
		GameObject level = GameObject.Find("LevelScript");

		if(level.GetComponent<level>().isHorizontal()==horizontal) {
			Destroy (gameObject);
		}
	}

	public void changeOrientation() {
		horizontal=!horizontal;	
	}

	public void setAsHorizontal(bool h) {
		if(h) {
			transform.eulerAngles = new Vector3(0, 0, 90);
		}
		horizontal=h;
	}

}
