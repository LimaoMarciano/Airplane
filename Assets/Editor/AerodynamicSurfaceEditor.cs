using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AerodynamicSurface))]
public class AerodynamicSurfaceEditor : Editor {

	private AerodynamicSurface myTarget;

	public void OnEnable () {
		
		myTarget = (AerodynamicSurface)target;

		if (myTarget.liftPerAngle == null) {
			myTarget.liftPerAngle = new AnimationCurve ();
		}
	}

	public override void OnInspectorGUI () {

		EditorGUILayout.Space();
		EditorGUILayout.LabelField ("Lift", EditorStyles.boldLabel);

		myTarget.liftCoefficient = EditorGUILayout.FloatField ("Lift coefficient", myTarget.liftCoefficient);
		myTarget.liftPerAngle = EditorGUILayout.CurveField ("Lift per angle", myTarget.liftPerAngle, GUILayout.Height (70));
		myTarget.invertLift = EditorGUILayout.Toggle ("Invert lift", myTarget.invertLift);

		EditorGUILayout.Space();
		EditorGUILayout.LabelField ("Drag", EditorStyles.boldLabel);

		float frontalDrag = EditorGUILayout.FloatField ("Frontal side", myTarget.dragCoefficient.z);
		float sideDrag = EditorGUILayout.FloatField ("Lateral side", myTarget.dragCoefficient.x);
		float upDrag = EditorGUILayout.FloatField ("Upper side", myTarget.dragCoefficient.y);

		myTarget.dragCoefficient = new Vector3 (sideDrag, upDrag, frontalDrag);

		//Angle
		EditorGUILayout.Space();
		EditorGUILayout.LabelField ("Surface rotation", EditorStyles.boldLabel);

		myTarget.rotationAxis = EditorGUILayout.Vector3Field ("Rotation axis", myTarget.rotationAxis);

		myTarget.minAngle = EditorGUILayout.DelayedFloatField ("Min angle", myTarget.minAngle);
		myTarget.maxAngle = EditorGUILayout.DelayedFloatField ("Max angle", myTarget.maxAngle);
		EditorGUILayout.MinMaxSlider (ref myTarget.minAngle, ref myTarget.maxAngle, 0, 50);

		myTarget.minAngle = Mathf.Round (myTarget.minAngle);
		myTarget.maxAngle = Mathf.Round (myTarget.maxAngle);


		//Offset
		EditorGUILayout.Space();
		EditorGUILayout.LabelField ("Offset rotation", EditorStyles.boldLabel);

		myTarget.minOffset = EditorGUILayout.FloatField ("Min offset", myTarget.minOffset);
		myTarget.maxOffset = EditorGUILayout.FloatField ("Max offset", myTarget.maxOffset);
		EditorGUILayout.MinMaxSlider (ref myTarget.minOffset, ref myTarget.maxOffset, 0, 50);

		myTarget.minOffset = Mathf.Round (myTarget.minOffset);
		myTarget.maxOffset = Mathf.Round (myTarget.maxOffset);


		if (Application.isPlaying) {
			GUILayout.BeginVertical ("box");
			EditorGUILayout.LabelField ("Info", EditorStyles.boldLabel);

			EditorGUILayout.LabelField ("Airspeed: ", myTarget.AirSpeed.ToString("F1"));
			EditorGUILayout.LabelField ("Angle of attack: ", myTarget.AngleOfAttack.ToString("F1"));
			EditorGUILayout.LabelField ("Current lift: ", myTarget.liftForce.ToString("F1"));
			EditorGUILayout.LabelField ("Drag: ", myTarget.Drag.ToString ("F1"));

			GUILayout.EndVertical ();

		}

		EditorGUILayout.Space ();

	}

}
