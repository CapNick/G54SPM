using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {
	public int Width;
	public int Height;
	public int Length;


	public List<GameObject> BlockDictionary = new List<GameObject>();
	public List<GameObject> MapBlocks;
	// Use this for initialization
	void Start () {
		MapBlocks = new List<GameObject>(Width*Length);
		CalculateNoise();
	}


	private void CalculateNoise() {
		for (int w = 0; w < Width; w++) {
			for (int l = 0; l < Length; l++) {
				GameObject block = Instantiate(BlockDictionary[0], new Vector3(w, 0, l), transform.rotation);
				block.transform.SetParent(transform);
				Debug.Log("Block: "+block+" -- perlin value: "+Mathf.PerlinNoise(w, l));
			}
		}
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
