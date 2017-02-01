using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVerticalFollow : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target) {

			transform.position = new Vector3 (transform.position.x, target.position.y, target.position.z);

		}
	}
}
