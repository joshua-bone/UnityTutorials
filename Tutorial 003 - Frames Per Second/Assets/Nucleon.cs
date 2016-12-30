using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Nucleon : MonoBehaviour {
	private static int count = 0; 
	public float attractionForce;
	Rigidbody body;
	private float scale;

	// Use this for initialization
	void Awake () {
		body = GetComponent<Rigidbody> ();
		this.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
		this.scale = Random.value * 5 + 0.5f;
		GetComponent<Transform> ().localScale = Vector3.one * this.scale;
		body.mass *= this.scale;
	}
	
	// Update is called once per time interval
	void FixedUpdate () {
		body.AddForce (body.mass * transform.position * -attractionForce);
	}
}
