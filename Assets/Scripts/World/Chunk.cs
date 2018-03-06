using System;
using System.Collections.Generic;
using UnityEngine;

namespace World {
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Chunk : MonoBehaviour {

        public int X;
        public int Z;
        
        private int _sizeX;
        private int _sizeY;
        private int _sizeZ;

        private readonly float BlockSize = 1;
        private readonly float TextureSize = 0.0625f;
        private Block[,,] _blocks;
        private Map _map;
        
        //rendering
        private Mesh _mesh;
        private MeshCollider _collider;
        private int _faceCounter;

        private List<Vector3> _verticies;
        private List<int> _triangles;
        private List<Vector2> _uvs;
        private List<Vector2Int> _textures;

      
        public void SetUpChunk(Map map, int x, int z, int sizeX, int sizeY, int sizeZ) {
            //give it the reference to the map object
            _map = map;
            
            //set position of chunk

            //save world position
            X = x;
            Z = z;
            transform.position = new Vector3(x * sizeX,0,z * sizeZ);

            
            //set chunk dimension
            _sizeX = sizeX;
            _sizeY = sizeY;
            _sizeZ = sizeZ;
            
            //block array
            _blocks = new Block[_sizeX,_sizeY,_sizeZ];
            _verticies = new List<Vector3>();
            _triangles = new List<int>();
            _uvs = new List<Vector2>();
            _textures = new List<Vector2Int>();
            _collider = GetComponent<MeshCollider> ();
            PoulateTextures();
            CreateBlocks();
        }
        
        public void GenerateChunk(float[,] generatedNoiseMap) {
            for (int x = 0; x < _sizeX; x++) {
                for (int y = 0; y < _sizeY; y++) {
                    for (int z = 0; z < _sizeZ; z++) {
                        if (y != 0) {
                            if (Math.Ceiling(generatedNoiseMap[x, z] * _sizeY)+64  >= y) {
                                _blocks[x,y,z].IsActive = true;
                                _blocks[x,y,z].Id = 1;
//                           Debug.Log("Making Block At: "+x+", "+y+", "+z+" TRUE");
//                           Debug.Log(""+Math.Ceiling(generatedNoiseMap[x, z] * _sizeY));
                            }
                        }
                    }
                }
            }

            GrassifyChunk();
            CreateMesh();
        }

        private void GrassifyChunk() {
            int counter = 0;
            for (int x = 0; x < _sizeX; x++) {
                for (int z = 0; z < _sizeZ; z++) {
                    for (int y = 0; y < _sizeY; y++) {
                        if (GetBlock(x,y,z).IsActive && !GetBlock(x,y+1,z).IsActive) {
                            //put the top 3rd and 2nd layer as dirt
                            if (counter < 2) {
                                _blocks[x,y+1,z].IsActive = true;
                                _blocks[x,y+1,z].Id = 2;
                                counter++;
                            }
                            //put the top layer as grass
                            else if (counter < 3){
                                _blocks[x,y+1,z].IsActive = true;
                                _blocks[x,y+1,z].Id = 3;
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
            for (int x = 0; x < 16; x++) {
                for (int y = 0; y < 16; y++) {
                    _textures.Add(new Vector2Int(x,y));
                }
            }
        }

        private void CreateBlocks() {
            //initalise the array of block structs
            for (int x = 0; x < _sizeX; x++) {
                for (int y = 0; y < _sizeY; y++) {
                    for (int z = 0; z < _sizeZ; z++) {
                        if (y == 0) {
                            _blocks[x,y,z] = new Block(11, true);
                        }
                        else if (y < 64) {
                            _blocks[x,y,z] = new Block(1, true);
                        }
                        else {
                            _blocks[x,y,z] = new Block(0, false);
                        }
                    }
                }
            }
        }

        private Block GetBlock(int x, int y, int z) {
            if ((x < _sizeX && x >= 0) && (y < _sizeY && y >= 0) && (z < _sizeZ && z >= 0)) {
                return _blocks[x, y, z];
            }
            return new Block(0, false);
        }

        private void CreateMesh() {
            _verticies.Clear();
            _triangles.Clear();
            _uvs.Clear();
            _mesh = GetComponent<MeshFilter>().mesh;
            _mesh.Clear();
            
            for (int x = 0; x < _sizeX; x++) {
                for (int y = 0; y < _sizeY; y++) {
                    for (int z = 0; z < _sizeZ; z++) {
                        Block block = _blocks[x, y, z];
                        if (block.IsActive) {
                            //TODO: Add checks for the side meshes too so they dont need to draw side faces if not needed
                            //check the block below is not active and if it is also not the bottom of the world
                            if (!GetBlock(x,y-1,z).IsActive && y != 0) {
                                CreateCubeBottom(x, y, z, _map.BlockDict[block.Id].BottomId);
                            }

                            if (!GetBlock(x,y,z-1).IsActive) {
                                CreateCubeLeft(x, y, z, _map.BlockDict[block.Id].LeftId);
                            }

                            if (!GetBlock(x-1,y,z).IsActive) {
                                CreateCubeFront(x, y, z, _map.BlockDict[block.Id].FrontId);
                            }

                            if (!GetBlock(x+1,y,z).IsActive) {
                                CreateCubeBack(x, y, z, _map.BlockDict[block.Id].BackId);
                            }

                            if (!GetBlock(x,y,z+1).IsActive) {
                                CreateCubeRight(x, y, z, _map.BlockDict[block.Id].RightId);
                            }

                            if (!GetBlock(x,y+1,z).IsActive) {
                                CreateCubeTop(x, y, z, _map.BlockDict[block.Id].TopId);
                            }
                        }
                    }
                }
            }
            
            
            _mesh.vertices = _verticies.ToArray();
            _mesh.triangles = _triangles.ToArray();
            _mesh.uv = _uvs.ToArray();
            _mesh.RecalculateNormals();
            _mesh.RecalculateTangents();
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