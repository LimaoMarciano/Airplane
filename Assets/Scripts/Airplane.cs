using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Airplane : MonoBehaviour {

	public Rigidbody rb;
	public Transform centerOfMass;
	private List<AerodynamicSurface> surfaces = new List<AerodynamicSurface>();

	void Awake() {
		foreach (WheelCollider w in GetComponentsInChildren<WheelCollider>()) 
			w.motorTorque = 0.000001f;
	}

	// Use this for initialization
	void Start () {
		if (rb != null) {
			
			rb = GetComponent<Rigidbody> ();
			rb.centerOfMass = centerOfMass.localPosition;

			foreach (AerodynamicSurface surface in GetComponentsInChildren<AerodynamicSurface>()) {
				surfaces.Add (surface);
				surface.rb = rb;
			}

		} else {
			
			Debug.LogWarning ("Airplane " + gameObject.name + " doesn't have a Rigidbody set. This Airplane won't do anything!");

		}

	}
	
	// Update is called once per frame
	void Update () {

		if (rb != null) {
			
			float totalDrag = 0;

			foreach (AerodynamicSurface surface in surfaces) {
				totalDrag += surface.Drag;
			}

			rb.drag = totalDrag;
		}

	}

	void OnDrawGizmosSelected () {
//		Gizmos.DrawIcon (transform.position + centerOfMass, "CenterOfMassIcon.png", true);
	}
}
