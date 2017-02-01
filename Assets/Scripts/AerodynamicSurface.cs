using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerodynamicSurface : MonoBehaviour {

	public Rigidbody rb;

	[Header ("Surface properties")]
	public float liftCoefficient = 1;
	public Vector3 dragCoefficient;
	public AnimationCurve liftPerAngle;

	[Header ("Surface rotation")]
	public Vector3 rotationAxis;
	[Range (0, 30)]
	public float minAngle = 0;
	[Range (0, 30)]
	public float maxAngle = 0;
	[Range (-1, 1)]
	public float angleInput = 0;

	[Header ("Surface rotation offset")]
	[Range (0, 30)]
	public float minOffset = 0;
	[Range (0, 30)]
	public float maxOffset = 0;
	[Range (-1, 1)]
	public float offsetInput = 0;

	[ReadOnly]public float angleOfAttack = 0;
	[ReadOnly]public float liftForce;
	[ReadOnly]public float drag;

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

//		transform.localEulerAngles = initialAngle + new Vector3 (surfaceAngle, 0, 0);
//		Debug.Log (initialAngle + new Vector3 (surfaceAngle, 0, 0));

	}

	void FixedUpdate () {
		Vector3 velocity = rb.velocity + Enviroment.instance.windVelocity;
		Vector3 localVelocity = transform.InverseTransformDirection (velocity);

		//Lift
		angleOfAttack = -Mathf.Atan2 (localVelocity.y, localVelocity.z) * Mathf.Rad2Deg;
		liftForce = liftPerAngle.Evaluate (angleOfAttack) * liftCoefficient * Enviroment.instance.airDensity * (velocity.sqrMagnitude / 2);

		//Drag
		Vector3 dragVector = Vector3.Scale (dragCoefficient, localVelocity.normalized);
		drag = dragVector.magnitude;
		//		rb.drag = dragMagnitude;

		if (rb != null) {
			Vector3 force = transform.up * liftForce;
			rb.AddForceAtPosition (force, transform.position, ForceMode.Force);
		}
	}

}
