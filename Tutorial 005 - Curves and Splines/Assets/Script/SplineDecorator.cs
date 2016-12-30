using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineDecorator : MonoBehaviour {
	public int frequency;
	private Color color;
	public GameObject[] items;

	public void decorate(BezierSpline spline){
		if (frequency <= 0 || items == null || items.Length == 0) {
			return;
		}
		color = new Color (Random.value, Random.value, Random.value);
		float stepSize = frequency * items.Length;
		if (spline.Loop || stepSize == 1) {
			stepSize = 1f / stepSize;
		}
		else {
			stepSize = 1f / (stepSize - 1);
		}
		for (int p = 0, f = 0; f < frequency; f++) {
			for (int i = 0; i < items.Length; i++, p++) {
				GameObject item = Instantiate(items[i]) as GameObject;
				item.GetComponent<MeshRenderer> ().material.color = color;
				Vector3 position = spline.GetPoint(p * stepSize);
				item.transform.localPosition = position;
				item.transform.LookAt(position + spline.GetDirection(p * stepSize));
				item.transform.localScale = Vector3.one * 0.5f;
				item.transform.Rotate (90, 0, 0);
			}
		}
	}
}
