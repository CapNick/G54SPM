using System.Collections.Generic;
using UnityEngine;

namespace World {
	public class MapGenerator : MonoBehaviour {
		public GameObject ChunkPrefab;
		
		
		//generation variables
		[Space, Header("Terrain Generation")]
		public int Seed;
		public int MapWidth;
		public int MapHeight;
		public float Scale;
		public int Octaves;
		public float Persistance;
		public float Lacunarity;
		public Vector2 Offset;
		
		
		//stores the information on the map including; blocks, width, height, length and seed.

		public List<Chunk> Chunks;
		

		public void Start() {
			//create the random generation
			float[,] map = Noise.GenerateNoiseMap(Seed, MapHeight, MapWidth, Scale, Octaves, Persistance, Lacunarity, Offset);
			
			Chunks = new List<Chunk>();
			int chunkSizeX = ChunkPrefab.GetComponent<Chunk>().SizeX;
			int chunkSizeZ = ChunkPrefab.GetComponent<Chunk>().SizeZ;
			//set the map container for saving
			//generate map
			
			//center
			GameObject chunk = Instantiate(ChunkPrefab, new Vector3(0*chunkSizeX, 0, 0*chunkSizeZ), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 0 + ", " + 0;
			Chunks.Add(chunk.GetComponent<Chunk>());
			
			//front 
			chunk = Instantiate(ChunkPrefab, new Vector3(1*chunkSizeX, 0, 0*chunkSizeZ), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 1 + ", " + 0;
			Chunks.Add(chunk.GetComponent<Chunk>());
			
			//back
			chunk = Instantiate(ChunkPrefab, new Vector3(-1*chunkSizeX, 0, 0*chunkSizeZ), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + -1 + ", " + 0;
			Chunks.Add(chunk.GetComponent<Chunk>());
			
			//left 
			chunk = Instantiate(ChunkPrefab, new Vector3(0*chunkSizeX, 0, 1*chunkSizeZ), transform.rotation);

			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 0 + ", " + 1;
			Chunks.Add(chunk.GetComponent<Chunk>());
			
			//right
			chunk = Instantiate(ChunkPrefab, new Vector3(0*chunkSizeX, 0, -1*chunkSizeZ), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 0 + ", " + -1;
			Chunks.Add(chunk.GetComponent<Chunk>());
			
			// front left 
			chunk = Instantiate(ChunkPrefab, new Vector3(1*chunkSizeX, 0, 1*chunkSizeZ), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 1 + ", " + 1;
			Chunks.Add(chunk.GetComponent<Chunk>());
			
			// front right
			chunk = Instantiate(ChunkPrefab, new Vector3(1*chunkSizeX, 0, -1*chunkSizeZ), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 1 + ", " + -1;
			Chunks.Add(chunk.GetComponent<Chunk>());
			
			// back left 
			chunk = Instantiate(ChunkPrefab, new Vector3(-1*chunkSizeX, 0, 1*chunkSizeZ), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 1 + ", " + 1;
			Chunks.Add(chunk.GetComponent<Chunk>());
			
			// back right
			chunk = Instantiate(ChunkPrefab, new Vector3(-1*chunkSizeX, 0, -1*chunkSizeZ), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 1 + ", " + -1;
			Chunks.Add(chunk.GetComponent<Chunk>());

		}

		public void Update() {
			
		}

	}
}
