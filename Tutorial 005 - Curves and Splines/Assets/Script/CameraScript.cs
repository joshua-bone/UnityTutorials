using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public float speed;

	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.LookAt(Vector3.zero);
		transform.Translate(Vector3.right * Time.deltaTime * speed);
		transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel"));
		transform.Translate(Vector3.up * Input.GetAxis("Vertical"));
	}
}
