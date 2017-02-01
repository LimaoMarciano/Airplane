using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AerodynamicSurface))]
public class AerodynamicSurfaceEditor : Editor {

	private bool isDragOpen = false;

	private float minAngle = 0;
	private float maxAngle = 15;
	private float minOffset = 0;
	private float maxOffset = 15;


	public override void OnInspectorGUI () {

		AerodynamicSurface myTarget = (AerodynamicSurface)target;

//		DrawDefaultInspector ();
		EditorGUILayout.Space();
		EditorGUILayout.LabelField ("Surface properties", EditorStyles.boldLabel);
		myTarget.liftCoefficient = EditorGUILayout.FloatField ("Lift coefficient", myTarget.liftCoefficient);

		isDragOpen = EditorGUILayout.Foldout (isDragOpen, "Parasitic Drag");
		if (isDragOpen) {
			float frontalDrag = EditorGUILayout.FloatField ("Frontal side", myTarget.dragCoefficient.z);
			float sideDrag = EditorGUILayout.FloatField ("Lateral side", myTarget.dragCoefficient.x);
			float upDrag = EditorGUILayout.FloatField ("Upper side", myTarget.dragCoefficient.y);

			myTarget.dragCoefficient = new Vector3 (sideDrag, frontalDrag, upDrag);
		}

		//Lift curve
		myTarget.liftPerAngle = EditorGUILayout.CurveField ("Lift per angle", myTarget.liftPerAngle, GUILayout.Height (70));

		//Angle
		EditorGUILayout.Space();
		EditorGUILayout.LabelField ("Surface rotation", EditorStyles.boldLabel);

		minAngle = EditorGUILayout.DelayedFloatField ("Min angle", minAngle);
		maxAngle = EditorGUILayout.DelayedFloatField ("Max angle", maxAngle);
		EditorGUILayout.MinMaxSlider (ref minAngle, ref maxAngle, 0, 50);

		minAngle = Mathf.Round (minAngle);
		maxAngle = Mathf.Round (maxAngle);

		myTarget.minAngle = minAngle;
		myTarget.maxAngle = maxAngle;

		//Offset
		EditorGUILayout.Space();
		EditorGUILayout.LabelField ("Offset rotation", EditorStyles.boldLabel);

		minOffset = EditorGUILayout.DelayedFloatField ("Min offset", minOffset);
		maxOffset = EditorGUILayout.DelayedFloatField ("Max offset", maxOffset);
		EditorGUILayout.MinMaxSlider (ref minOffset, ref maxOffset, 0, 50);

		minOffset = Mathf.Round (minOffset);
		maxOffset = Mathf.Round (maxOffset);

		myTarget.minOffset = minOffset;
		myTarget.maxOffset = maxOffset;

		if (Application.isPlaying) {
			EditorGUILayout.Space();
			EditorGUILayout.LabelField ("Info", EditorStyles.boldLabel);

			EditorGUILayout.LabelField ("Angle of attck: ", myTarget.AngleOfAttack.ToString("F1"));
			EditorGUILayout.LabelField ("Current lift: ", myTarget.liftForce.ToString("F0"));
			EditorGUILayout.LabelField ("Drag: ", myTarget.Drag.ToString ("F1"));

		}

	}

}
