using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour {

	public Rigidbody rb;

	public float engineForce = 500.0f;
	[Range(0, 1)]
	public float input = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		input = Mathf.Clamp01 (input);

		rb.AddForce (-transform.forward * engineForce * input, ForceMode.Force);
	}
}
