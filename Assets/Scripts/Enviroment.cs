using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviroment : MonoBehaviour {

	public static Enviroment instance = null;

	public float airDensity = 1.2f;
	[Range (0, 100)]
	public float windSpeed = 0;
	public Vector3 windVelocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			
			instance = this;

		} else if (instance != this) {
			
			Destroy (gameObject);

		}
	}

	void Update () {
		windVelocity = transform.forward * windSpeed;
	}

}
