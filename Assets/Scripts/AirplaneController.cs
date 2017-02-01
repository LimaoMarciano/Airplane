using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour {

	public Engine engine;
	public AerodynamicSurface leftAileron;
	public AerodynamicSurface rightAileron;
	public AerodynamicSurface leftElevator;
	public AerodynamicSurface rightElevator;
	public AerodynamicSurface rudder;

	private bool isFlapActive = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		leftAileron.angleInput = -horizontal;
		rightAileron.angleInput = horizontal;

		leftElevator.angleInput = -vertical;
		rightElevator.angleInput = -vertical;

		if (Input.GetButtonDown ("Flaps")) {
			isFlapActive = !isFlapActive;
		}

		if (Input.GetButton ("IncreaseEngine")) {
			engine.input += 1 * Time.deltaTime;
		}

		if (Input.GetButton ("DecreaseEngine")) {
			engine.input -= 1 * Time.deltaTime;
		}

		if (isFlapActive) {
			leftAileron.offsetInput = -1;
			rightAileron.offsetInput = -1;
		} else {
			leftAileron.offsetInput = 0;
			rightAileron.offsetInput = 0;
		}
		
	}
}
