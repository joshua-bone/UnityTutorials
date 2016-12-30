using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineWalker : MonoBehaviour {
	public BezierSpline spline;
	public SplineWalkerMode mode;
	private bool goingForward = true;
	public float duration;
	private float progress;
	private Color color;

	void Awake(){
		color = new Color (Random.value, Random.value, Random.value);
		GetComponent<MeshRenderer> ().material.color = color;
		foreach (MeshRenderer child in GetComponentsInChildren<MeshRenderer>()) {
			child.material.color = color;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (goingForward) {
			progress += Time.deltaTime / duration;
			if (progress > 1f) {
				if (mode == SplineWalkerMode.Once) {
					progress = 1f;
				} else if (mode == SplineWalkerMode.Loop) {
					progress -= 1f;
				} else { //Ping Pong
					progress = 2f - progress;
					goingForward = false;
				}
			} 
		} else {
			progress -= Time.deltaTime / duration;
			if (progress < 0f) {
				progress = -progress;
				goingForward = true;
			}
		}
		Vector3 position = spline.GetPoint (progress);
		transform.localPosition = position;
		transform.LookAt (position + spline.GetDirection (progress));
	}
}
