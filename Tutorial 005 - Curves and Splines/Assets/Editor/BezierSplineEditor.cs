using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierSpline))]
public class BezierSplineEditor : Editor {
	private const float directionScale = 0.5f; //scale the velocity tangents
	private const int stepsPerCurve = 10; //number of velocity tangents to draw per curve
	//private const int lineSteps = 10;

	//for the custom GUI elements
	private const float handleSize = 0.04f;
	private const float pickSize = 0.06f;
	private int selectedIndex = -1;

	private BezierSpline spline;
	private Transform handleTransform;
	private Quaternion handleRotation;

	private static Color[] modeColors = {
		Color.white,
		Color.yellow,
		Color.cyan
	};


	private void OnSceneGUI(){
		spline = target as BezierSpline;
		handleTransform = spline.transform;
		handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;
		Vector3 p0 = ShowPoint (0);
		for (int i = 1; i < spline.ControlPointCount; i += 3) {
			Vector3 p1 = ShowPoint (i);
			Vector3 p2 = ShowPoint (i + 1);
			Vector3 p3 = ShowPoint (i + 2);
			Handles.color = Color.gray;
			Handles.DrawLine (p0, p1);
			//Handles.DrawLine (p1, p2);
			Handles.DrawLine (p2, p3);
			Handles.DrawBezier (p0, p3, p1, p2, Color.white, null, 2f);
			p0 = p3;
		}
		ShowDirections ();

		//OLD WAY OF DRAWING BEZIERS IN SEGMENTS
		//		Vector3 lineStart = curve.GetPoint(0f);
		//		Handles.color = Color.green;
		//		Handles.DrawLine(lineStart, lineStart + curve.GetDirection(0f)); //draw the velocity tangents
		//		for (int i = 1; i <= lineSteps; i++) {
		//			Vector3 lineEnd = curve.GetPoint(i / (float)lineSteps);
		//			Handles.color = Color.white;
		//			Handles.DrawLine(lineStart, lineEnd);
		//			Handles.color = Color.green;
		//			Handles.DrawLine(lineEnd, lineEnd + curve.GetDirection(i / (float)lineSteps)); //draw the velocity tangents
		//			lineStart = lineEnd;
		//		}
	}

	private void ShowDirections(){
		Handles.color = Color.green;
		Vector3 point = spline.GetPoint (0f);
		int steps = stepsPerCurve * spline.CurveCount;
		Handles.DrawLine(point, point + spline.GetDirection(0f) * directionScale);
		for (int i = 1; i <= steps; i++) {
			point = spline.GetPoint(i / (float)steps);
			Handles.DrawLine(point, point + spline.GetDirection(i / (float)steps) * directionScale);
		}
	}



	private Vector3 ShowPoint(int index){
		Vector3 point = handleTransform.TransformPoint (spline.GetControlPoint(index));
		Handles.color = modeColors[(int)spline.GetControlPointMode(index)];
		float size = HandleUtility.GetHandleSize(point);
		if (index == 0) {
			size *= 2f;
		}

		if (Handles.Button (point, handleRotation, size * handleSize, size * pickSize, Handles.DotCap)) {
			selectedIndex = index;
			Repaint ();
		}
		if (selectedIndex == index) {
			EditorGUI.BeginChangeCheck ();
			point = Handles.DoPositionHandle (point, handleRotation);
			if (EditorGUI.EndChangeCheck ()) {
				Undo.RecordObject (spline, "Move Point");
				EditorUtility.SetDirty (spline);
				spline.SetControlPoint(index, handleTransform.InverseTransformPoint (point));
			}
		}

		return point;
	}

	public override void OnInspectorGUI () {
		spline = target as BezierSpline;
		EditorGUI.BeginChangeCheck();
		bool loop = EditorGUILayout.Toggle("Loop", spline.Loop);
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(spline, "Toggle Loop");
			EditorUtility.SetDirty(spline);
			spline.Loop = loop;
		}
		if (selectedIndex >= 0 && selectedIndex < spline.ControlPointCount) {
			DrawSelectedPointInspector ();
		}
		if (GUILayout.Button("Add Curve")) { //Adds a button to the 'Inspector' pane
			Undo.RecordObject(spline, "Add Curve");
			spline.AddCurve();
			EditorUtility.SetDirty(spline);
		}
	}

	private void DrawSelectedPointInspector(){
		GUILayout.Label ("Selected Point");
		EditorGUI.BeginChangeCheck ();
		Vector3 point = EditorGUILayout.Vector3Field ("Position", spline.GetControlPoint (selectedIndex));
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject (spline, "Move Point");
			EditorUtility.SetDirty (spline);
			spline.SetControlPoint (selectedIndex, point);
		}
		EditorGUI.BeginChangeCheck();
		Bezier.ControlPointMode mode = (Bezier.ControlPointMode) EditorGUILayout.EnumPopup("Mode", spline.GetControlPointMode(selectedIndex));
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(spline, "Change Point Mode");
			spline.SetControlPointMode(selectedIndex, mode);
			EditorUtility.SetDirty(spline);
		}
	}
}
