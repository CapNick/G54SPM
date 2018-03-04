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
		public Dictionary<int, BlockType> BlockDictionary = new Dictionary<int, BlockType>();
		private Block[,,] _blocks;
		
		public void Awake() {
			
		}

		public void Start() {
			//create the random generation
			BlockDictionary = new Dictionary<int, BlockType>();
			Chunks = new List<Chunk>();
			//set the map container for saving
			GameObject chunk;
			//generate map
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
		}

		private void ReloadCHunks() {
			if (Chunks.Count > 1) {

				foreach (Chunk chun in Chunks) {
					int x = chun.X;
					int y = chun.Z;
					chun.GenerateChunk(NoiseGen.GenerateSimplexHeightMap(ChunkLength, Seed, Octaves, Persistance, Lacunarity, Strength, new Vector2(x+Offset.x,y+Offset.y)));

				}
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
