using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineGenerator : MonoBehaviour {
	//List<BezierSpline> splineList = new List<BezierSpline>();
	public int numberOfSplines;
	public int segmentsPerSpline;
	public int scale;
	public int duration;
	public BezierSpline prefabSpline;
	public SplineWalker prefabWalker;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < numberOfSplines; i++) {
			BezierSpline newSpline = Instantiate<BezierSpline> (prefabSpline);
			for (int k = 1; k < segmentsPerSpline; k++) {
				newSpline.AddCurve ();
			}
			for (int j = 0; j < newSpline.ControlPointCount; j++){
				newSpline.SetControlPointMode (j, Bezier.ControlPointMode.Aligned);
			}
			for (int j = 0; j < newSpline.ControlPointCount; j++){
				newSpline.SetControlPoint (j, Random.onUnitSphere * scale);
			}
			SplineWalker newWalker = Instantiate<SplineWalker> (prefabWalker);
			newWalker.spline = newSpline;
			newWalker.duration = duration + Random.value * (duration / segmentsPerSpline) - (duration / segmentsPerSpline / 2);
			if (i == 0) {
				Camera.main.GetComponent<CameraLookAt> ().target = newWalker.gameObject;
			}
		}
	}

}
