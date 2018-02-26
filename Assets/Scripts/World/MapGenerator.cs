using System.Collections.Generic;
using UnityEngine;

namespace World {
	public class MapGenerator : MonoBehaviour {
		public GameObject ChunkPrefab;
		
		
		//generation variables
		[Space, Header("Terrain Generation")]
		public int MapWidth;
		public int MapHeight;
		public int Seed;
		public float Scale;
		public int Octaves;
		public float Persistance;
		public float Lacunarity;

		[Space, Header("Chunk Information")]
		public int ChunkLength = 16;
		public int ChunkHeight = 16;
		public int ChunkWidth = 16;
		
		
		//stores the information on the map including; blocks, width, height, length and seed.

		public List<Chunk> Chunks;

		public void Awake() {
			
		}

		public void Start() {
			//create the random generation
			
			Chunks = new List<Chunk>();
			//set the map container for saving
			
			//generate map
			
			//center
			GameObject chunk = Instantiate(ChunkPrefab, new Vector3(0*ChunkLength, 0, 0*ChunkWidth), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 0 + ", " + 0;
			chunk.GetComponent<Chunk>().SetUpChunk(ChunkLength, ChunkHeight, ChunkWidth );
			chunk.GetComponent<Chunk>().GenerateChunk(Noise.GenerateNoiseMap(MapHeight, MapWidth, Seed, Scale, Octaves, Persistance, Lacunarity, new Vector2(0*ChunkHeight,0*ChunkWidth)));
			Chunks.Add(chunk.GetComponent<Chunk>());
			//front 
			chunk = Instantiate(ChunkPrefab, new Vector3(1*ChunkLength, 0, 0*ChunkWidth), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 1 + ", " + 0;
			chunk.GetComponent<Chunk>().SetUpChunk(ChunkLength, ChunkHeight, ChunkWidth );
			chunk.GetComponent<Chunk>().GenerateChunk(Noise.GenerateNoiseMap(MapHeight, MapWidth, Seed, Scale, Octaves, Persistance, Lacunarity, new Vector2(1*ChunkHeight,0*ChunkWidth)));
			
			Chunks.Add(chunk.GetComponent<Chunk>());
//			//back
			chunk = Instantiate(ChunkPrefab, new Vector3(-1*ChunkLength, 0, 0*ChunkWidth), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + -1 + ", " + 0;
			chunk.GetComponent<Chunk>().SetUpChunk(ChunkLength, ChunkHeight, ChunkWidth );
			chunk.GetComponent<Chunk>().GenerateChunk(Noise.GenerateNoiseMap(MapHeight, MapWidth, Seed, Scale, Octaves, Persistance, Lacunarity, new Vector2(-1*ChunkHeight,0*ChunkWidth)));
			Chunks.Add(chunk.GetComponent<Chunk>());
//			//left 
			chunk = Instantiate(ChunkPrefab, new Vector3(0*ChunkLength, 0, 1*ChunkWidth), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 0 + ", " + 1;
			chunk.GetComponent<Chunk>().SetUpChunk(ChunkLength, ChunkHeight, ChunkWidth );
			chunk.GetComponent<Chunk>().GenerateChunk(Noise.GenerateNoiseMap(MapHeight, MapWidth, Seed, Scale, Octaves, Persistance, Lacunarity, new Vector2(-1*ChunkHeight,0*ChunkWidth)));
			Chunks.Add(chunk.GetComponent<Chunk>());
//			//right
			chunk = Instantiate(ChunkPrefab, new Vector3(0*ChunkLength, 0, -1*ChunkWidth), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 0 + ", " + -1;
			chunk.GetComponent<Chunk>().SetUpChunk(ChunkLength, ChunkHeight, ChunkWidth );
			chunk.GetComponent<Chunk>().GenerateChunk(Noise.GenerateNoiseMap(MapHeight, MapWidth, Seed, Scale, Octaves, Persistance, Lacunarity, new Vector2(0*ChunkHeight,-1*ChunkWidth)));
			Chunks.Add(chunk.GetComponent<Chunk>());
//			// front left 
			chunk = Instantiate(ChunkPrefab, new Vector3(1*ChunkLength, 0, 1*ChunkWidth), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 1 + ", " + 1;
			chunk.GetComponent<Chunk>().SetUpChunk(ChunkLength, ChunkHeight, ChunkWidth );
			chunk.GetComponent<Chunk>().GenerateChunk(Noise.GenerateNoiseMap(MapHeight, MapWidth, Seed, Scale, Octaves, Persistance, Lacunarity, new Vector2(1*ChunkHeight,1*ChunkWidth)));
			Chunks.Add(chunk.GetComponent<Chunk>());
//			// front right
			chunk = Instantiate(ChunkPrefab, new Vector3(1*ChunkLength, 0, -1*ChunkWidth), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 1 + ", " + -1;
			chunk.GetComponent<Chunk>().SetUpChunk(ChunkLength, ChunkHeight, ChunkWidth );
			chunk.GetComponent<Chunk>().GenerateChunk(Noise.GenerateNoiseMap(MapHeight, MapWidth, Seed, Scale, Octaves, Persistance, Lacunarity, new Vector2(1*ChunkHeight,-1*ChunkWidth)));
			Chunks.Add(chunk.GetComponent<Chunk>());
//			// back left 
			chunk = Instantiate(ChunkPrefab, new Vector3(-1*ChunkLength, 0, 1*ChunkWidth), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 1 + ", " + 1;
			chunk.GetComponent<Chunk>().SetUpChunk(ChunkLength, ChunkHeight, ChunkWidth );
			chunk.GetComponent<Chunk>().GenerateChunk(Noise.GenerateNoiseMap(MapHeight, MapWidth, Seed, Scale, Octaves, Persistance, Lacunarity, new Vector2(-1*ChunkHeight,1*ChunkWidth)));
			Chunks.Add(chunk.GetComponent<Chunk>());
//			// back right
			chunk = Instantiate(ChunkPrefab, new Vector3(-1*ChunkLength, 0, -1*ChunkWidth), transform.rotation);
			chunk.transform.SetParent(transform);
			chunk.name = "Chunk" + ":" + 1 + ", " + -1;
			chunk.GetComponent<Chunk>().SetUpChunk(ChunkLength, ChunkHeight, ChunkWidth );
			chunk.GetComponent<Chunk>().GenerateChunk(Noise.GenerateNoiseMap(MapHeight, MapWidth, Seed, Scale, Octaves, Persistance, Lacunarity, new Vector2(-1*ChunkHeight,-1*ChunkWidth)));
			Chunks.Add(chunk.GetComponent<Chunk>());

		}

		public void Update() {
			
		}
		
		
		//make sure these variables are not out of bounds
		public void OnValidate() {
			if (MapWidth < 1) {
				MapWidth = 1;
			}
			if (MapHeight < 1) {
				MapHeight = 1;
			}
			if (Lacunarity < 1) {
				Lacunarity = 1;
			}
			if (Octaves < 0) {
				Octaves = 0;
			}
		}
	}
}
