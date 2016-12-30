using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fractal : MonoBehaviour {
	public static int count = 0;

	public Mesh[] meshes;
	public Material material;

	public int maxDepth;
	private float childScale;

	private int depth = 0;
	private float spawnProbability;

	private float rotationSpeed;

	private Material[] materials;

	private void initializeMaterials(){
		materials = new Material[maxDepth + 1];
		Color c1 = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
		Color c2 = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
		for (int i = 0; i <= maxDepth; i++) {
			materials [i] = new Material (material);
			materials [i].color = Color.Lerp(c1, c2, (float) i / maxDepth);
		}
	}

	private Dictionary<Vector3, Quaternion> directions = new Dictionary<Vector3,Quaternion>(){
		{Vector3.up, Quaternion.Euler(0f, 0f, 0f)},
		{Vector3.right, Quaternion.Euler(0f, 0f, -90f)},
		{Vector3.left, Quaternion.Euler(0f, 0f, 90f)},
		{Vector3.forward, Quaternion.Euler(90f, 0f, 0f)},
		{Vector3.back, Quaternion.Euler(-90f, 0f, 0f)}
	};

	// Use this for initialization
	void Start () {
		if (materials == null)
			initializeMaterials ();
		transform.Rotate (Random.Range (-20f, 20f), 0f, 0f);
		rotationSpeed = Random.Range (0f, 120f) - 60f;
		count++;
		spawnProbability = Random.value + (maxDepth - depth) * 0.2f;
		gameObject.AddComponent<MeshFilter> ().mesh = meshes[Random.Range(0, meshes.Length)];
		gameObject.AddComponent<MeshRenderer> ().material = materials[depth];
		if (depth < maxDepth) {
			StartCoroutine (CreateChildren ());
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
	}

	private IEnumerator CreateChildren(){
		float waitTime = 0.5f;
		if (count <= 3) {
			yield return new WaitForSeconds (Random.Range(0.1f, 0.5f));
			new GameObject ("Fractal Child").AddComponent<Fractal> ().initialize (this, Vector3.down, Quaternion.Euler (0f, 0f, -180f));
		}
		foreach (KeyValuePair<Vector3, Quaternion> kvp in directions) {
			if (Random.value < spawnProbability) {
				yield return new WaitForSeconds (Random.Range (0.1f, 0.5f));
				new GameObject ("Fractal Child").AddComponent<Fractal> ().initialize (this, kvp.Key, kvp.Value);
			}
		}

	}

	//Y-AXIS
	//Vector3.up = 		(0, 1, 0)
	//Vector3.down = 	(0, -1, 0)
	//X-AXIS
	//Vector3.right = 		(1, 0, 0)
	//Vector3.left = (-1, 0, 0)
	//Z-AXIS
	//Vector3.forward = (0, 0, 1)
	//Vector3.back = (0, 0, -1)

	void initialize(Fractal parent, Vector3 direction, Quaternion orientation){
		materials = parent.materials;
		meshes = parent.meshes;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		childScale = 0.2f + Random.value;
		transform.parent = parent.transform;
		transform.localScale = new Vector3(Random.value + 0.5f, Random.value + 0.5f, Random.value + 0.5f) * childScale;
		transform.localPosition = direction * (0.5f + 0.5f * childScale);
		transform.localRotation = orientation;
	}
}
