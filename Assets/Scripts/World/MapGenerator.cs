using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace World {
	public class MapGenerator : MonoBehaviour {

		[Header("Player Information")]
		public GameObject Player;
		[Range(1,15)]
		public int DrawDistance;
		[Range(1,25)]
		public int GenerationDistance;
		
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
		[Range(-1,1)]
		public float SurfaceGain = 0.2f;

		public FastNoise.NoiseType SurfaceNoiseType;
		public FastNoise.FractalType SurfaceFractalType;
		public FastNoise.Interp SurfaceInterperlation;

		
		[Header("Underground Generation")]
		[Range(1, 8)]
		public int UndergroundOctaves;
		[Range(1f,4f)]
		public float UndergroundLacunarity;
		[Range(0f,1f)]
		public float UndergroundFrequency;
		[Range(-1,1)]
		public float UndergroundGain = 0.5f;

		public FastNoise.NoiseType UndergroundNoiseType;
		public FastNoise.FractalType UndergroundFractalType;
		public FastNoise.Interp UndergroundInterperlation;
		
		public Vector2 Offset;

		[Header("Other")]
		
		public float NoiseMultiplyer;
		public AnimationCurve HeightCurve;
		public GameObject ChunkPrefab;

		public int WaterLevel;
		public int LavalLevel = 3;

		private int _chunkLength;
		private int _chunkHeight;
		private int _chunkWidth;
		
		//store the information on the map including; blocks, width, height, length and seed.W
		private Map _map;
		private FastNoise _groundMap;
		private FastNoise _undergroundMap;
		private IEnumerator _enumerator;
//		private Chunk _chunk;
		
		public void Start() {
			if (_map == null) {
				_map = GetComponent<Map>();
			}

			_chunkLength = _map.ChunkLength;
			_chunkHeight = _map.ChunkHeight;
			_chunkWidth = _map.ChunkWidth;
			WaterLevel = _map.Height / 2;

//			Debug.Log("x min"+Mathf.FloorToInt((Player.transform.position.x / _chunkLength) - RenderRadius  ));
//			Debug.Log("z min"+Mathf.FloorToInt((Player.transform.position.z / _chunkWidth) - RenderRadius ));
//			Debug.Log("x max"+Mathf.FloorToInt((Player.transform.position.x / _chunkLength) + RenderRadius ));
//			Debug.Log("z max"+Mathf.FloorToInt((Player.transform.position.z / _chunkWidth) + RenderRadius ));
			_groundMap = new FastNoise(Seed);
			_groundMap.SetNoiseType(SurfaceNoiseType);
			_groundMap.SetFractalType(SurfaceFractalType);
			_groundMap.SetFractalOctaves(SurfaceOctaves);
			_groundMap.SetFractalLacunarity(SurfaceLacunarity);
			_groundMap.SetFractalGain(SurfaceGain);
			_groundMap.SetFrequency(SurfaceFrequency);
			_groundMap.SetInterp(SurfaceInterperlation);

			_undergroundMap = new FastNoise(Seed);
			_undergroundMap.SetNoiseType(UndergroundNoiseType);
			_undergroundMap.SetFractalType(UndergroundFractalType);
			_undergroundMap.SetFractalOctaves(UndergroundOctaves);
			_undergroundMap.SetFractalLacunarity(UndergroundLacunarity);
			_undergroundMap.SetFractalGain(UndergroundGain);
			_undergroundMap.SetFrequency(UndergroundFrequency);
			_undergroundMap.SetInterp(UndergroundInterperlation);
			GenerateChunks();
		}

		public void Update() {
//			GenerateChunks();
			StartCoroutine(RenderChunks());
		}
		
		public IEnumerator RenderChunks() {
			Vector3 playerPosition = Player.transform.position;
			for (int l = -DrawDistance; l <= DrawDistance; l++) {
				for (int w = -DrawDistance; w <= DrawDistance; w++) {
					for (int h = 0; h < _map.Height / _chunkHeight; h++) {
						Chunk chunk = _map.GetChunk(new Vector3(l * _chunkLength + playerPosition.x, h * _chunkHeight, w * _chunkWidth + playerPosition.z));
						if (chunk != null && !chunk.Loaded && !chunk.Empty) {
							yield return StartCoroutine(chunk.CreateMesh());	
						}
					}
				}
			}
		}

		public void GenerateChunks() {
			Vector3 playerPosition = Player.transform.position;	
			for (int l = -GenerationDistance; l <= GenerationDistance; l++) {
				for (int w = -GenerationDistance; w <= GenerationDistance; w++) {
					for (int h = 0; h < _map.Height / _chunkHeight; h++) {
						Chunk chunk = _map.GetChunk(new Vector3(l * _chunkLength + playerPosition.x, h * _chunkHeight, w * _chunkWidth + playerPosition.z));
						if (chunk == null) {
//							Debug.Log("("+l+", "+h+", "+w+")");
							TerraformChunk(CreateChunk(new Vector3Int(l,h,w)));
						}
					}
				}
			}
		}
		
		

		private Chunk CreateChunk(Vector3Int position) {
			GameObject chunkGameObject = Instantiate(ChunkPrefab,transform);
			chunkGameObject.name = "Chunk" + ":" + position;
			chunkGameObject.GetComponent<Chunk>().SetUpChunk(_map, position,_chunkLength, _chunkHeight, _chunkWidth );
			Chunk chunk = chunkGameObject.GetComponent<Chunk>();
			_map.Chunks.Add(position.ToString(),chunk);
			return chunk;
		}

		private void TerraformChunk(Chunk chunk) {
			for (int x = 0; x < _chunkLength; x++) {
				
				float xCoord = (Offset.x + x + chunk.Position.x*_chunkLength);
				for (int z = 0; z < _chunkWidth; z++) {
					
					float zCoord = (Offset.y + z + chunk.Position.z*_chunkWidth);
					
					for (int y = 0; y < _chunkHeight; y++) {
						chunk.UpdateBlock(x,y,z, 0);
					}
					
					//Underground Generation
					if (chunk.Position.y * _chunkHeight  < _map.Height / 2) {
						for (int y = 0; y < _chunkHeight; y++) {
							float yCoord = y + chunk.Position.y*_chunkHeight;
							if (chunk.Position.y == 0 && y == 0) {
								chunk.UpdateBlock(x,y,z,11);
							}
							//underground Generation
							else if (chunk.Position.y * _chunkHeight < _map.Height / 2) {
								float sample = _undergroundMap.GetNoise(xCoord,yCoord,zCoord);
								if (sample < -0.5f) {
									chunk.UpdateBlock(x,y,z,1);							
								}

							}
							if (chunk.Position.y == 0 && chunk.GetBlock(x,y,z) == 0 && y < LavalLevel && y != 0) {
								chunk.UpdateBlock(x,y,z,25);							
							}
						}
					}
					//Surface Generation
					else if(chunk.Position.y * _chunkHeight  < _map.Height / 2 + _map.Height / _chunkHeight){

						float sample = HeightCurve.Evaluate(_groundMap.GetNoise(xCoord,zCoord)*NoiseMultiplyer);

//						if (chunk.Position.y * _chunkHeight < _map.Height / 2) {
//							
//						}
						if (sample < 0) {
							chunk.UpdateBlock(x,0,z, 26);							
						}
						else if(sample < 0.05f) {
							for (int y = 0; y < Mathf.RoundToInt(_chunkHeight*sample); y++) {
								chunk.UpdateBlock(x,y,z,1);
							}
							chunk.UpdateBlock(x,Mathf.RoundToInt(_chunkHeight*sample),z,16);
						}
						else if(sample < 0.2f) {
							for (int y = 0; y < Mathf.RoundToInt(_chunkHeight*sample); y++) {
								chunk.UpdateBlock(x,y,z,1);
							}
							chunk.UpdateBlock(x,Mathf.RoundToInt(_chunkHeight*sample),z,3);
						}
						else if(sample < 0.3f) {
							for (int y = 0; y < Mathf.RoundToInt(_chunkHeight*sample); y++) {
								chunk.UpdateBlock(x,y,z,1);
							}
							chunk.UpdateBlock(x,Mathf.RoundToInt(_chunkHeight*sample),z,3);
						}
						else {
							for (int y = 0; y < Mathf.RoundToInt(_chunkHeight*sample); y++) {
								chunk.UpdateBlock(x,y,z,1);
							}
							chunk.UpdateBlock(x,Mathf.RoundToInt(_chunkHeight*sample),z,1);
						}
					}
				}
			}

//			yield return null;
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
		}
	}
}
