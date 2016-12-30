using UnityEngine;

public class Spawner : MonoBehaviour {

	public float timeBetweenSpawns;
	public float maxVelocity;

	public Stuff[] stuffPrefabs;

	float timeSinceLastSpawn;
	Color color;

	void Awake(){
		color = new Color(Random.value, Random.value, Random.value);
	}

	void FixedUpdate () {
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn >= timeBetweenSpawns) {
			timeSinceLastSpawn = 0f;
			SpawnStuff();
		}
	}

	void SpawnStuff () {
		Stuff prefab = stuffPrefabs[Random.Range(0, stuffPrefabs.Length)];
		Stuff spawn = prefab.GetPooledInstance<Stuff>();
		spawn.transform.localPosition = transform.position;
		spawn.body.velocity = transform.up * maxVelocity * (Random.value * 0.3f + 0.7f);

		MeshRenderer meshr = spawn.GetComponent<MeshRenderer> ();
		if (meshr) {
			meshr.material.color = Random.value < 0.9f ? color : new Color(Random.value, Random.value, Random.value);
			spawn.transform.localScale = Random.onUnitSphere + Vector3.one;
		} else {
			MeshRenderer[] children = spawn.GetComponentsInChildren<MeshRenderer> ();
			foreach (MeshRenderer child in children) {
				child.material.color = Random.value < 0.9f ? color : new Color(Random.value, Random.value, Random.value);;
			}
		}
	}
}