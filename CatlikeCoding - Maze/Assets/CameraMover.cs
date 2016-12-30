using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		float xAxisValue = Input.GetAxis("Horizontal");
		float zAxisValue = Input.GetAxis("Vertical");
		if(Camera.current != null)
		{
			if (Input.GetKeyDown (KeyCode.Space)) {
				Camera.current.transform.Rotate (new Vector3 (xAxisValue, 0.0f, zAxisValue));
			} else {
				Camera.current.transform.Translate (new Vector3 (xAxisValue, 0.0f, zAxisValue));
			}
		}	
	}
}
