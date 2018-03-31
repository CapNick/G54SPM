using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace World {
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(NavMeshSurface))]
    public class Chunk : MonoBehaviour {

        public Vector3Int Position;
        
        private int _sizeX;
        private int _sizeY;
        private int _sizeZ;

        private readonly float BlockSize = 1;
        private readonly float TextureSize = 0.0625f;
        private int[,,] _blocks;
        private Map _map;
        
        //render
        private Mesh _mesh;
        private MeshCollider _collider;
        private int _faceCounter;

        private List<Vector3> _verticies;
        private List<int> _triangles;
        private List<Vector2> _uvs;
        private List<Vector2Int> _textures;

      
        public void SetUpChunk(Map map, Vector3Int pos, int sizeX, int sizeY, int sizeZ) {
            //give it the reference to the map object
            _map = map;
            //save world position
            Position = pos;
            
            //set chunk dimension
            _sizeX = sizeX;
            _sizeY = sizeY;
            _sizeZ = sizeZ;
            
            //set position of chunk
            transform.position = new Vector3(pos.x * sizeX,pos.y * sizeY,pos.z * sizeZ);
            
            //block array
            _blocks = new int[_sizeX,_sizeY,_sizeZ];
            _verticies = new List<Vector3>();
            _triangles = new List<int>();
            _uvs = new List<Vector2>();
            _collider = GetComponent<MeshCollider> ();
            PoulateTextures();
//            CreateBlocks();
        }
        
        public void GenerateChunk(FastNoise undergroundNoise, FastNoise groundNoise) {
            for (int x = 0; x < _sizeX; x++) {
                for (int z = 0; z < _sizeZ; z++) {
                    for (int y = 0; y < _sizeY; y++) {
//                        Debug.Log();
                        if (y == 0 && Position.y == 0){
                            UpdateBlock(x, y, z, 11);
                        }
                        else if(undergroundNoise.GetNoise(x+_sizeX*Position.x,y+_sizeY*Position.y,z+_sizeZ*Position.z) > 0) {
                            UpdateBlock(x, y, z, 1);
                        }
                        else {
                            UpdateBlock(x,y,z, 0);
                        }
//                        Debug.Log("("+x+","+y+","+z+") ==> ("+undergroundNoise.GetNoise(x+_sizeX*Position.x,y+_sizeY*Position.y,z+_sizeZ*Position.z)+")");
                    }
                }
            }

//            GrassifyChunk();
        }

        public void UpdateBlock(int x, int y , int z, int id) { 
            _blocks[x, y, z] = id; 
        } 
        
        public int GetBlock(int x, int y, int z) {
            if ((x < _sizeX && x >= 0) && (y < _sizeY && y >= 0) && (z < _sizeZ && z >= 0)) {
                return _blocks[x, y, z];
            }
            return 0;
        }

//        public void OnDrawGizmosSelected() {
//            Gizmos.color = new Color(255,0,0,0.25f);
//            Gizmos.DrawCube(new Vector3(X*_sizeX/2, _sizeY/2, Z*_sizeZ/2) , new Vector3(_sizeX, _sizeY, _sizeZ));
//        }

        private void GrassifyChunk() {
            int counter = 0;
            for (int x = 0; x < _sizeX; x++) {
                for (int z = 0; z < _sizeZ; z++) {
                    for (int y = 0; y < _sizeY; y++) {
                        if (GetBlock(x,y,z) != 0 && GetBlock(x,y+1,z) != 0) {
                            //put the top 3rd and 2nd layer as dirt
                            if (counter < 1) {
                                _blocks[x,y+1,z]= 2;
                                counter++;
                            }
                            //put the top layer as grass
                            else if (counter < 2){
                                _blocks[x,y+1,z] = 3;
                                counter++;
                            }
                            else {
                                counter = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }
        
        //load in textures from atlas
        private void PoulateTextures() {
            _textures = new List<Vector2Int>();
            for (int x = 0; x < 16; x++) {
                for (int y = 0; y < 16; y++) {
                    _textures.Add(new Vector2Int(x,y));
                }
            }
        }
        
        public void CreateMesh() {
            if (_mesh == null) {
                _mesh = GetComponent<MeshFilter>().mesh;
            }
            _verticies.Clear();
            _triangles.Clear();
            _uvs.Clear();
            _mesh.Clear();

//            Chunk chunkBottom = _map.GetChunk(new Vector3(_sizeX * Position.x, _sizeY * Position.y - 1 ,_sizeZ * Position.z));
//            Chunk chunkLeft = _map.GetChunk(new Vector3(_sizeX * Position.x, _sizeY * Position.y,_sizeZ * Position.z - 1));
//            Chunk chunkFront = _map.GetChunk(new Vector3(_sizeX * Position.x - 1, _sizeY * Position.y,_sizeZ * Position.z));
//            Chunk chunkBack = _map.GetChunk(new Vector3(_sizeX * Position.x + 1, _sizeY * Position.y,_sizeZ * Position.z));
//            Chunk chunkRight = _map.GetChunk(new Vector3(_sizeX * Position.x, _sizeY * Position.y,_sizeZ * Position.z + 1));
//            Chunk chunkTop = _map.GetChunk(new Vector3(_sizeX * Position.x, _sizeY * Position.y + 1,_sizeZ * Position.z)); 
            
            for (int x = 0; x < _sizeX; x++) {
                for (int y = 0; y < _sizeY; y++) {
                    for (int z = 0; z < _sizeZ; z++) {
                        int block = _blocks[x, y, z];
                        if (block != 0) {
                                                     
                            // get the block dictionary so we can check if the blocks are transparent or not
                            //this way we do not have to store the transparancey in the block data structure.
                            BlockDictionary dict = BlockDictionary.Instance;
                            //check if bottom is touching air                           
                            if (GetBlock(x,y-1,z) == 0 || GetBlock(x,y-1,z) > 0 && dict.GetBlockType(GetBlock(x,y-1,z)).IsTransparent) {
                                //Botched attempt at getting the blocks to render more efficently over chunks
//                                if (_map.GetBlock(new Vector3(x + _sizeX * Position.x, y + _sizeY * Position.y - 1,
//                                        z + _sizeZ * Position.z)) == 0) {
                                    CreateCubeBottom(x, y, z, _map.BlockDict[block].BottomId);
//                                }
                            }

                            if (GetBlock(x,y,z-1) == 0 || GetBlock(x,y,z-1) > 0 && dict.GetBlockType(GetBlock(x,y,z-1)).IsTransparent) {
                                    CreateCubeLeft(x, y, z, _map.BlockDict[block].LeftId);
                            }

                            if (GetBlock(x-1,y,z) == 0 || GetBlock(x-1,y,z) > 0 && dict.GetBlockType(GetBlock(x-1,y,z)).IsTransparent) {
                                    CreateCubeFront(x, y, z, _map.BlockDict[block].FrontId);
                            }

                            if (GetBlock(x+1,y,z) == 0 || GetBlock(x+1,y,z) > 0 && dict.GetBlockType(GetBlock(x+1,y,z)).IsTransparent) {
                                    CreateCubeBack(x, y, z, _map.BlockDict[block].BackId);
                            }

                            if (GetBlock(x,y,z+1)  == 0 || GetBlock(x,y,z+1) > 0 && dict.GetBlockType(GetBlock(x,y,z+1)).IsTransparent) {
                                    CreateCubeRight(x, y, z, _map.BlockDict[block].RightId);
                            }

                            if (GetBlock(x,y+1,z) == 0 || GetBlock(x,y+1,z) > 0 && dict.GetBlockType(GetBlock(x,y+1,z)).IsTransparent) {
//                                if (_map.GetBlock(new Vector3(x + _sizeX * Position.x, y + _sizeY * Position.y,
//                                        z + _sizeZ * Position.z - 1)) == 0) {
                                    CreateCubeTop(x, y, z, _map.BlockDict[block].TopId);
//                                }
                            }
                        }
                    }
                }

            }
            _mesh.vertices = _verticies.ToArray();
            _mesh.triangles = _triangles.ToArray();
            _mesh.uv = _uvs.ToArray();
            _mesh.RecalculateTangents();
            _mesh.RecalculateNormals();
            _collider.sharedMesh = _mesh;
            _faceCounter = 0;
        }

        private void CreateCubeBottom(int x, int y, int z, int id) {
            //verticies
            _verticies.Add(new Vector3(x, y, z));
            _verticies.Add(new Vector3(x, y, z + BlockSize));
            _verticies.Add(new Vector3(x + BlockSize, y, z + BlockSize));
            _verticies.Add(new Vector3(x + BlockSize, y, z));
            SetTri();
            SetUV(_textures[id]);
        }
        private void CreateCubeLeft(int x, int y, int z, int id) {
            //verticies
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z));
            _verticies.Add(new Vector3(x, y + BlockSize, z));
            _verticies.Add(new Vector3(x, y, z));
            _verticies.Add(new Vector3(x + BlockSize, y, z));
            SetTri();
            SetUV(_textures[id]);
        }
        private void CreateCubeFront(int x, int y, int z, int id) {
            //verticies
            _verticies.Add(new Vector3(x, y + BlockSize, z));
            _verticies.Add(new Vector3(x, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x, y, z + BlockSize));
            _verticies.Add(new Vector3(x, y, z));
            SetTri();
            SetUV(_textures[id]);
        }
        private void CreateCubeBack(int x, int y, int z, int id) {
            //verticies
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z));
            _verticies.Add(new Vector3(x + BlockSize, y, z));
            _verticies.Add(new Vector3(x + BlockSize, y, z + BlockSize));
            SetTri();
            SetUV(_textures[id]);
        }
        private void CreateCubeRight(int x, int y, int z, int id) {
            //verticies
            _verticies.Add(new Vector3(x, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x + BlockSize, y, z + BlockSize));
            _verticies.Add(new Vector3(x, y, z + BlockSize));
            SetTri();
            SetUV(_textures[id]);
        }
        private void CreateCubeTop(int x, int y, int z, int id) {
            //verticies
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z));
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x, y + BlockSize, z));
            SetTri();
            SetUV(_textures[id]);
        }

        private void SetTri() {
            //triagles
            _triangles.Add(_faceCounter * 4 + 3);
            _triangles.Add(_faceCounter * 4 + 1);
            _triangles.Add(_faceCounter * 4 + 0);
            _triangles.Add(_faceCounter * 4 + 3);
            _triangles.Add(_faceCounter * 4 + 2);
            _triangles.Add(_faceCounter * 4 + 1);
            _faceCounter++;
        }

        private void SetUV(Vector2Int textCoords) {
            //uvs
            _uvs.Add(new Vector2(TextureSize * textCoords.x, TextureSize + TextureSize * textCoords.y));
            _uvs.Add(new Vector2(TextureSize + TextureSize * textCoords.x, TextureSize + TextureSize * textCoords.y));
            _uvs.Add(new Vector2(TextureSize + TextureSize * textCoords.x, TextureSize * textCoords.y));
            _uvs.Add(new Vector2(TextureSize * textCoords.x, TextureSize * textCoords.y));

        }
    }
}