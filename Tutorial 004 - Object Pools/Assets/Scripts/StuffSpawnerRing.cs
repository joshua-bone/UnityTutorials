using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSpawnerRing : MonoBehaviour {

	public int numberOfSpawners;
	public float radius, tiltAngle;
	public Spawner prefab;


	// Use this for initialization
	void Awake () {
		for (int i = 0; i < numberOfSpawners; i++) {
			createSpawner (i);
		}
	}

	public void createSpawner(int index){
		Transform rotator = new GameObject ("Rotator").transform;
		rotator.SetParent (transform, false);
		rotator.localRotation = Quaternion.Euler (0f, index * 360f / numberOfSpawners, 0f);
		Spawner spawner = Instantiate<Spawner> (prefab);
		spawner.transform.SetParent(rotator, false);
		spawner.transform.localPosition = new Vector3(0f, 0f, radius);
		spawner.transform.localRotation = Quaternion.Euler(tiltAngle, 0f, 0f);
	}
}
