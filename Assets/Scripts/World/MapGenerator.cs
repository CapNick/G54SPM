using System.Collections.Generic;
using System.Linq;
using Misc;
using UnityEngine;


namespace World {
	public class MapGenerator : MonoBehaviour {

		public GameObject Player;
		
		public GameObject ChunkPrefab;
		[Range(1,10)]
		public int RenderRadius = 3;
		//generation variables
		[Space, Header("Terrain Generation")]
		public int Seed;
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

		private int _chunkLength;
		private int _chunkHeight;
		private int _chunkWidth;
		private Chunk _chunk;
		
		//store the information on the map including; blocks, width, height, length and seed.W
		private Map _map;
		
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
			
		}

		public void Update() {
			for (int x = (int)(Player.transform.position.x / _chunkLength - RenderRadius ); x < (int)(Player.transform.position.x / _chunkLength + RenderRadius ); x++) {
				for (int z = (int)(Player.transform.position.z / _chunkWidth - RenderRadius ); z < (int)(Player.transform.position.z / _chunkWidth + RenderRadius ); z++) {
					_chunk = _map.GetChunk(new Vector3(x*_chunkLength , 0, z*_chunkWidth));
					if (_chunk == null) {
						_chunk = GenerateChunk(x, z);
						StartCoroutine(_chunk.CreateMesh());
					}
					else if (!_chunk.gameObject.activeInHierarchy){
						_chunk.gameObject.SetActive(true);
					}
				}
			}
		}
		
		private Chunk GenerateChunk(int xPos, int zPos) {
			GameObject chunkGameObject = Instantiate(ChunkPrefab,transform);
			chunkGameObject.name = "Chunk" + ":" + xPos + ", " + zPos;
			chunkGameObject.GetComponent<Chunk>().SetUpChunk(_map, xPos, zPos,_chunkLength, _chunkHeight, _chunkWidth );
			Chunk chunk = chunkGameObject.GetComponent<Chunk>();
			_map.Chunks.Add(new Vector2Int(xPos,zPos).ToString(), chunk);
			chunk.GenerateChunk(NoiseGen.Generate2DHeightMap(_chunkLength, Seed, MethodType, Octaves, Persistance, Lacunarity, Strength, new Vector2(chunk.X+Offset.x,chunk.Z+Offset.y)));
			return chunk;
		}

		private void RenderChunk(int xPos, int zPos) {
			
		}

		private void UnrenderChunk(int xPos, int zPos) {
			
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
			if (Octaves < 0) {
				Octaves = 0;
			}
		}

	}
}
