using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerodynamicSurface : MonoBehaviour {

	public Rigidbody rb;

	public float liftCoefficient = 1;
	public Vector3 dragCoefficient;
	public AnimationCurve liftPerAngle;

	public Vector3 rotationAxis;
	public float minAngle = 0;
	public float maxAngle = 0;
	public float angleInput = 0;

	public float minOffset = 0;
	public float maxOffset = 0;
	public float offsetInput = 0;

	public float angleOfAttack = 0;
	public float liftForce;
	public float drag;

	private Vector3 airVelocity;
	private float airSpeed;
	private Quaternion initialRotation;
	private float surfaceAngle;

	public float SurfaceAngle {
		get { return surfaceAngle; }
		private set { surfaceAngle = value; }
	}

	public float AngleOfAttack {
		get { return angleOfAttack; }
		private set { angleOfAttack = value; }
	}

	public float Drag {
		get { return drag; }
		private set { drag = value; }
	}

	public float AirSpeed {
		get { return airSpeed; }
		private set { airSpeed = value; }
	}


	// Use this for initialization
	void Start () {
		initialRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {

		surfaceAngle = ((maxAngle - minAngle) * angleInput) + minAngle;
		float offset = ((maxOffset - minOffset) * offsetInput) + minOffset;
		surfaceAngle += offset;

		transform.localRotation = initialRotation * Quaternion.AngleAxis (surfaceAngle, rotationAxis);

	}

	void FixedUpdate () {

		if (rb == null) {
			gameObject.SetActive (false);
			Debug.LogWarning ("Aerodynamic surface " + gameObject.name + " doesn't have a parent with Airplane component. Object will be disabled.");
			return;
		}


		Vector3 velocity = rb.velocity + Enviroment.instance.windVelocity;
		Vector3 localVelocity = transform.InverseTransformDirection (velocity);
		airSpeed = velocity.magnitude;

		//Lift
		angleOfAttack = -Mathf.Atan2 (localVelocity.y, localVelocity.z) * Mathf.Rad2Deg;
		liftForce = liftPerAngle.Evaluate (angleOfAttack) * liftCoefficient * Enviroment.instance.airDensity * (velocity.sqrMagnitude / 2);

		//Drag
		Vector3 dragVector = Vector3.Scale (dragCoefficient, localVelocity.normalized);
		drag = dragVector.magnitude;


		Vector3 force = transform.up * liftForce;
		rb.AddForceAtPosition (force, transform.position, ForceMode.Force);

	}

}
