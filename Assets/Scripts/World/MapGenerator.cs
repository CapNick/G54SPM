using System.Collections.Generic;
using Misc;
using UnityEngine;


namespace World {
	public class MapGenerator : MonoBehaviour {
		public GameObject ChunkPrefab;

		[Range(1,10)]
		public int RenderRadius = 3;
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
		public NoiseMethodType MethodType;

		private int ChunkLength;
		private int ChunkHeight;
		private int ChunkWidth;
		
		//store the information on the map including; blocks, width, height, length and seed.W
		private Map Map;
		
		public void Start() {
			if (Map == null) {
				Map = GetComponent<Map>();
			}

			ChunkLength = Map.ChunkLength;
			ChunkHeight = Map.ChunkHeight;
			ChunkWidth = Map.ChunkWidth;
			GenerateChunks();
		}

		public void GenerateChunks() {
			//create the random generation
			
			//set the map container for saving
			GameObject chunk;
//			generate map
			for (int x = -RenderRadius; x <= RenderRadius; x++) {
				for (int y = -RenderRadius; y <= RenderRadius; y++) {
					chunk = Instantiate(ChunkPrefab);
					chunk.transform.SetParent(transform);
					chunk.name = "Chunk" + ":" + x + ", " + y;
					chunk.GetComponent<Chunk>().SetUpChunk(Map, x, y,ChunkLength, ChunkHeight, ChunkWidth );
					chunk.GetComponent<Chunk>().GenerateChunk(NoiseGen.GenerateSimplexHeightMap(ChunkLength, Seed, MethodType, Octaves, Persistance, Lacunarity, Strength, new Vector2(x+Offset.x,y+Offset.y)));
					Map.Chunks.Add(chunk.GetComponent<Chunk>());
				}
			}
			
//			Test Chunks			
//			chunk = Instantiate(ChunkPrefab);
//			chunk.transform.SetParent(transform);
//			chunk.name = "Chunk" + ":" + 0 + ", " + 0;
//			chunk.GetComponent<Chunk>().SetUpChunk(Map, 0, 0,ChunkLength, ChunkHeight, ChunkWidth );
//			chunk.GetComponent<Chunk>().GenerateChunk(NoiseGen.GenerateSimplexHeightMap(ChunkLength, Seed, MethodType, Octaves, Persistance, Lacunarity, Strength, new Vector2(0+Offset.x,0+Offset.y)));
//			Map.Chunks.Add(chunk.GetComponent<Chunk>());
//			
//			chunk = Instantiate(ChunkPrefab);
//			chunk.transform.SetParent(transform);
//			chunk.name = "Chunk" + ":" + -1 + ", " + 0;
//			chunk.GetComponent<Chunk>().SetUpChunk(Map, -1, 0,ChunkLength, ChunkHeight, ChunkWidth );
//			chunk.GetComponent<Chunk>().GenerateChunk(NoiseGen.GenerateSimplexHeightMap(ChunkLength, Seed, MethodType, Octaves, Persistance, Lacunarity, Strength, new Vector2(-1+Offset.x,0+Offset.y)));
//			Map.Chunks.Add(chunk.GetComponent<Chunk>());

		}
		

		private void RecalculateChunks() {
			if (Map.Chunks.Count > 1) {
				foreach (Chunk chun in Map.Chunks) {
					int x = chun.X;
					int y = chun.Z;
					chun.GenerateChunk(NoiseGen.GenerateSimplexHeightMap(ChunkLength, Seed, MethodType, Octaves, Persistance, Lacunarity, Strength, new Vector2(x+Offset.x,y+Offset.y)));
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

//			ReloadCalculateChunks();
		}

	}
}
