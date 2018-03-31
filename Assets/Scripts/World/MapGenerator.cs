using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace World {
	public class MapGenerator : MonoBehaviour {

		public GameObject Player;
		
		public GameObject ChunkPrefab;
		[Range(1,10)]
		public int RenderRadius = 3;
		//generation variables
		[Header("Terrain Generation")]
		public int Seed;
		
		[Header("Surface Generation")]
		[Range(1, 8)]
		public int SurfaceOctaves;
		[Range(1f,4f)]
		public float SurfaceLacunarity;
		[Range(0f,1f)]
		public float SurfaceFrequency;
		[Header("Underground Generation")]
		[Range(1, 8)]
		public int UndergroundOctaves;
		[Range(1f,4f)]
		public float UndergroundLacunarity;
		[Range(0f,1f)]
		public float UndergroundFrequency;

		public FastNoise.NoiseType NType;
		public FastNoise.FractalType FType;
		
		public Vector2 Offset;

		public bool Edit = false;

		private int _chunkLength;
		private int _chunkHeight;
		private int _chunkWidth;
		
		private Chunk _chunk;
		
		//store the information on the map including; blocks, width, height, length and seed.W
		private Map _map;
		private FastNoise _groundMap;
		private FastNoise _undergroundMap;
		
		public void Start() {
			if (_map == null) {
				_map = GetComponent<Map>();
			}
			
			_chunkLength = _map.ChunkLength;
			_chunkHeight = _map.ChunkHeight;
			_chunkWidth = _map.ChunkWidth;
			
			
//			Debug.Log("x min"+Mathf.FloorToInt((Player.transform.position.x / _chunkLength) - RenderRadius  ));
//			Debug.Log("z min"+Mathf.FloorToInt((Player.transform.position.z / _chunkWidth) - RenderRadius ));
//			Debug.Log("x max"+Mathf.FloorToInt((Player.transform.position.x / _chunkLength) + RenderRadius ));
//			Debug.Log("z max"+Mathf.FloorToInt((Player.transform.position.z / _chunkWidth) + RenderRadius ));
			
			_groundMap = new FastNoise(Seed);
			_groundMap.SetNoiseType(NType);
			_groundMap.SetFractalType(FType);
			_groundMap.SetFractalOctaves(SurfaceOctaves);
			_groundMap.SetFractalLacunarity(SurfaceLacunarity);
			_groundMap.SetFrequency(SurfaceFrequency);

			
			_undergroundMap = new FastNoise(Seed);
			_undergroundMap.SetNoiseType(NType);
			_undergroundMap.SetFractalType(FType);
			_undergroundMap.SetFractalOctaves(UndergroundOctaves);
			_undergroundMap.SetFractalLacunarity(UndergroundLacunarity);
			_undergroundMap.SetFrequency(UndergroundFrequency);

			Gen();
			
		}

		private void Gen() {
			


			for (int l = -RenderRadius/2; l  < RenderRadius/2; l++) {
				for (int w = -RenderRadius/2; w < RenderRadius/2; w++) {
					for (int h = 0; h < _map.Height / _map.ChunkHeight/2; h++) {
						GenerateChunk(new Vector3Int(l, h, w));

//						GenerateChunk(new Vector3Int(1, h, 0));
//						GenerateChunk(new Vector3Int(0, h, 1));
//						GenerateChunk(new Vector3Int(-1, h, 0));
//						GenerateChunk(new Vector3Int(0, h, -1));
//						GenerateChunk(new Vector3Int(-1, h, 1));
//						GenerateChunk(new Vector3Int(1, h, -1));
//						GenerateChunk(new Vector3Int(-1, h, -1));
					}
				}
			}

			foreach (var chunk in _map.Chunks.Values) {
				chunk.CreateMesh();
			}
			
		}

		private Chunk GenerateChunk(Vector3Int pos) {
			
			
			GameObject chunkGameObject = Instantiate(ChunkPrefab,transform);
			chunkGameObject.name = "Chunk" + ":" + pos;
			chunkGameObject.GetComponent<Chunk>().SetUpChunk(_map, pos,_chunkLength, _chunkHeight, _chunkWidth );
			Chunk chunk = chunkGameObject.GetComponent<Chunk>();
			_map.Chunks.Add(pos.ToString(), chunk);
			chunk.GenerateChunk(_undergroundMap, _groundMap);
			return chunk;
		}

		private void RenderChunk(int xPos, int yPos, int zPos) {
			
		}

		private void UnrenderChunk(int xPos, int yPos, int zPos) {
			
		}
		
				
		//make sure these variables are not out of bounds
		public void OnValidate() {
			if (_chunkWidth < 1) {
				_chunkWidth = 1;
			}
			if (_chunkLength < 1) {
				_chunkLength = 1;
			}
			if (_chunkLength < 1) {
				_chunkLength = 1;
			}
			
			if (Edit) {
				_groundMap.SetNoiseType(NType);
				_groundMap.SetFractalType(FType);
				_groundMap.SetFractalOctaves(SurfaceOctaves);
				_groundMap.SetFractalLacunarity(SurfaceLacunarity);
				_groundMap.SetFrequency(SurfaceFrequency);
				_undergroundMap.SetNoiseType(NType);
				_undergroundMap.SetFractalType(FType);
				_undergroundMap.SetFractalOctaves(UndergroundOctaves);
				_undergroundMap.SetFractalLacunarity(UndergroundLacunarity);
				_undergroundMap.SetFrequency(UndergroundFrequency);
				
				foreach (var chunk in _map.Chunks.Values) {
					chunk.GenerateChunk(_undergroundMap, _groundMap);
					chunk.CreateMesh();
				}

			}
		}

	}
}
