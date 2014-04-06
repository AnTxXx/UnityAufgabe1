/**
Instantiates comets
 **/

using UnityEngine;
using System.Collections;

public class create_comets : MonoBehaviour {

	public Transform comet;

	private GameObject instanceComet;
	private float lastTime;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < 20; i++) {
			Instantiate(comet, new Vector3(Random.Range(-10f, 10f), Random.Range(-3.56f, 5f)), Quaternion.identity);
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time - lastTime >= 1) {
			Instantiate(comet, new Vector3(-10f, Random.Range(-3.56f, 5f)), Quaternion.identity);
			Instantiate(comet, new Vector3( 10f, Random.Range(-3.56f, 5f)), Quaternion.identity);
			lastTime = Time.time;
		}

	}
}
