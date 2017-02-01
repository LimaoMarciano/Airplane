using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceObject : MonoBehaviour {

	public AerodynamicSurface surface;
	public Vector3 rotationAxis;
	public bool isInverted = false;

	private Quaternion initialRotation;

	// Use this for initialization
	void Start () {
		initialRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		float surfaceAngle = surface.SurfaceAngle;

		if (isInverted) {
			surfaceAngle *= -1;
		}

		transform.localRotation = initialRotation * Quaternion.AngleAxis (surfaceAngle, rotationAxis);

	}
}
