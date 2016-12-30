using UnityEngine;
using System.Collections;

public class FPSCounter : MonoBehaviour {

	public int frameRange = 60;
	public int[] fps;
	public int averageFPS { get; private set; }
	int[] fpsBuffer;
	int fpsBufferIndex = 0;

	// Use this for initialization
	void Start () {
		fps = new int[3];
		fps [2] = 99;
		fpsBuffer = new int[frameRange];
	}
	
	// Update is called once per frame
	void Update () {
		updateBuffer ();
	}

	void updateBuffer(){
		fpsBuffer[fpsBufferIndex] = (int)(1f / Time.unscaledDeltaTime);
		if (++fpsBufferIndex == frameRange) {
			fpsBufferIndex = 0;
			calculateFPS();
		}
	}

	void calculateFPS () {
		int sum = 0;
		for (int i = 0; i < frameRange; i++) {
			sum += fpsBuffer[i];
		}
		fps[1] = sum / frameRange;
		fps [2] = Mathf.Min (fps [1], fps [2]);
		fps [0] = Mathf.Max (fps [1], fps [0]);
	}


}
