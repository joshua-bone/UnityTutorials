using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour {
	public Text fpsMax;
	public Text fpsAvg;
	public Text fpsMin;

	FPSCounter fpsCounter;

	// Use this for initialization
	void Start () {
		fpsCounter = GetComponent<FPSCounter> ();
	}
	
	// Update is called once per frame
	void Update () {
		fpsMax.text = fpsCounter.fps[0].ToString ();
		fpsAvg.text = fpsCounter.fps[1].ToString ();
		fpsMin.text = fpsCounter.fps[2].ToString ();

	}
}
