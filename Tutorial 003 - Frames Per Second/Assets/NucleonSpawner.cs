using UnityEngine;
using System.Collections;

public class NucleonSpawner : MonoBehaviour {
	public Nucleon prefab;
	public float maxTimeBetweenSpawns;
	public float maxSpawnDistance;
	private float timeSinceLastSpawn;
	private float thisInterval;

	// Use this for initialization
	void Awake () {
		thisInterval = Random.value * maxTimeBetweenSpawns; 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn > thisInterval) {
			timeSinceLastSpawn = 0f;
			thisInterval = Random.value * maxTimeBetweenSpawns;
			SpawnNucleon ();
		}
	}

	void SpawnNucleon(){
		Nucleon spawn = Instantiate<Nucleon> (prefab);
		spawn.transform.localPosition = Random.onUnitSphere * maxSpawnDistance * Random.value;
	}
}
