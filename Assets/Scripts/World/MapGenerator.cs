using System.Collections.Generic;
using UnityEngine;

namespace World {
	public class MapGenerator : MonoBehaviour {
		public GameObject ChunkPrefab;

		[Range(1,5)]
		public int RenderDistance = 3;
		//generation variables
		[Space, Header("Terrain Generation")]
//		public int MapWidth;
//		public int MapHeight;
		public int Seed;
//		public float Scale;
		[Range(1, 8)]
		public int Octaves;
		[Range(0f,1f)]
		public float Persistance;
		[Range(1f,4f)]
		public float Lacunarity;
		[Range(0f,1f)]
		public float Strength;
		public Vector2 Offset;

		[Space, Header("Chunk Information")]
		public int ChunkLength = 16;
		public int ChunkHeight = 16;
		public int ChunkWidth = 16;
		
		
		
		
		//stores the information on the map including; blocks, width, height, length and seed.

		public List<Chunk> Chunks;
		private Block[,,] _blocks;
		
		GameObject chunk;


		public void Awake() {
			
		}

		public void Start() {
			//create the random generation
			
			Chunks = new List<Chunk>();
			//set the map container for saving
			
			//generate map
			
			//center
//			GameObject chunk = Instantiate(ChunkPrefab);		
//			chunk.transform.SetParent(transform);
//			chunk.name = "Chunk" + ":" + 0 + ", " + 0;
//			chunk.GetComponent<Chunk>().SetUpChunk(0, 0,ChunkLength, ChunkHeight, ChunkWidth );
//			chunk.GetComponent<Chunk>().GenerateChunk(NoiseGen.GenerateNoiseMap(ChunkLength, ChunkWidth, Seed, Scale, Octaves, Persistance, Lacunarity, new Vector2(0,0)));
//			Chunks.Add(chunk.GetComponent<Chunk>());
			
			//front 
//			GameObject chunk = Instantiate(ChunkPrefab);
//			WorkingChunk = chunk;
//			chunk.transform.SetParent(transform);
//			chunk.name = "Chunk" + ":" + 1 + ", " + 0;
//			chunk.GetComponent<Chunk>().SetUpChunk(1, 0,ChunkLength, ChunkHeight, ChunkWidth );
//			chunk.GetComponent<Chunk>().GenerateChunk(NoiseGen.GenerateNoiseMap(ChunkLength, ChunkWidth, Seed, Scale, Octaves, Persistance, Lacunarity, new Vector2(0.5f,0)));
//			Chunks.Add(chunk.GetComponent<Chunk>());

			for (int x = -RenderDistance; x <= RenderDistance; x++) {
				for (int y = -RenderDistance; y <= RenderDistance; y++) {
					chunk = Instantiate(ChunkPrefab);
					chunk.transform.SetParent(transform);
					chunk.name = "Chunk" + ":" + x + ", " + y;
					chunk.GetComponent<Chunk>().SetUpChunk(x, y,ChunkLength, ChunkHeight, ChunkWidth );
					chunk.GetComponent<Chunk>().GenerateChunk(NoiseGen.GenerateSimplexHeightMap(ChunkLength, Seed, Octaves, Persistance, Lacunarity, Strength, new Vector2(x+Offset.x,y+Offset.y)));
					Chunks.Add(chunk.GetComponent<Chunk>());
				}
			}

			InvokeRepeating("ReloadCHunks", 0, 0.5f);


		}

		public void FixedUpdate() {
//				WorkingChunk.GetComponent<Chunk>().GenerateChunk(NoiseGen.GenerateNoiseMap(MapHeight, MapWidth, Seed, Scale, Octaves, Persistance, Lacunarity, Offset));
//			if (Input.GetKeyDown(KeyCode.Space)) {
//				Offset.x += 0.08f;
//				WorkingChunk.GetComponent<Chunk>().GenerateChunk(NoiseGen.PerlinNoise(ChunkLength,ChunkWidth,Scale, Offset));
//				WorkingChunk.GetComponent<Chunk>().GenerateChunk(NoiseGen.GenerateNoiseMap(ChunkLength, ChunkWidth, Seed, Scale, Octaves, Persistance, Lacunarity, Offset));
//			} 

		}

		private void ReloadCHunks() {
			Offset.x += 0.1f;
			if (Chunks.Count > 1) {

				foreach (Chunk chun in Chunks) {
					int x = chun.X;
					int y = chun.Z;
					chun.GenerateChunk(NoiseGen.GenerateSimplexHeightMap(ChunkLength, Seed, Octaves, Persistance, Lacunarity, Strength, new Vector2(x+Offset.x,y+Offset.y)));

				}
				
//				Debug.Log(WorkingChunk.name + "Change Now");
//				WorkingChunk.GetComponent<Chunk>().GenerateChunk(NoiseGen.GenerateNoiseMap(ChunkLength, ChunkWidth, Seed, Scale, Octaves, Persistance, Lacunarity, Offset));
			}
		}
		
		
		//make sure these variables are not out of bounds
		public void OnValidate() {
			if (ChunkWidth < 1) {
				ChunkWidth = 1;
			}
			if (ChunkLength < 1) {
				ChunkLength = 1;
			}
			if (ChunkLength < 1) {
				ChunkLength = 1;
			}
			if (Octaves < 0) {
				Octaves = 0;
			}

			ReloadCHunks();


		}

//		public Block GetBlock(Vector3 pos) {
//			
//		}
	}
}
