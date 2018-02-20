using System.Collections.Generic;
using UnityEngine;

namespace World {
	public class MapGenerator : MonoBehaviour {
		public GameObject ChunkPrefab;
		
		//map key information
		public int Width;
		public int Length;

		public int ChunkSize = 16;
		
		//generation variables
		public int Seed;
		
		
		//stores the information on the map including; blocks, width, height, length and seed.

		public List<Chunk> Chunks;
		

		public void Start() {
			Chunks = new List<Chunk>();
			//set the map container for saving
			//generate map
			for (int x = 0; x < Length; x++) {
				for (int z = 0; z < Width; z++) {
						GameObject chunk = Instantiate(ChunkPrefab, new Vector3(x*ChunkSize, 0, z*ChunkSize), transform.rotation);

						chunk.transform.SetParent(transform);
						chunk.name = "Chunk" + ":" + x + ", " + z;
						Chunks.Add(chunk.GetComponent<Chunk>());
//						Block block = blockGameObject.GetComponent<Block>();
//						block.SetPosition(x,y,z);
						

				}
			}
		}

		public void Update() {
			
		}

	}
}
