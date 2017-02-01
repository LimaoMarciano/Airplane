using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Airplane : MonoBehaviour {

	public Rigidbody rb;
	public Transform centerOfMass;
	public List<AerodynamicSurface> surfaces = new List<AerodynamicSurface>();

	void Awake() {
		foreach (WheelCollider w in GetComponentsInChildren<WheelCollider>()) 
			w.motorTorque = 0.000001f;
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.centerOfMass = centerOfMass.localPosition;



		foreach (AerodynamicSurface surface in surfaces) {
			surface.rb = rb;
		}
	}
	
	// Update is called once per frame
	void Update () {

		float totalDrag = 0;

		foreach (AerodynamicSurface surface in surfaces) {
			totalDrag += surface.Drag;
		}

		rb.drag = totalDrag;

		Debug.Log (rb.velocity.magnitude);
	}
}
