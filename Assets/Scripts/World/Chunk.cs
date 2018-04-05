using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace World {
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(NavMeshSurface))]
    public class Chunk : MonoBehaviour {

        public Vector3Int Position;
        public bool Loaded;
        public bool Empty;
        
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
        private List<Vector2> _textures;

      
        public void SetUpChunk(Map map, Vector3Int pos, int sizeX, int sizeY, int sizeZ) {
            //give it the reference to the map object
            _map = map;
            //save world position
            Position = pos;
            Loaded = false;
            Empty = true;
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
        }
        


        public void UpdateBlock(int x, int y , int z, int id) {
            if ((x < _sizeX && x >= 0) && (y < _sizeY && y >= 0) && (z < _sizeZ && z >= 0)) {
                if (id != 0) {
                    Empty = false;                   
                }
                _blocks[x, y, z] = id; 
            }
        } 
        
        public int GetBlock(int x, int y, int z) {
            if ((x < _sizeX && x >= 0) && (y < _sizeY && y >= 0) && (z < _sizeZ && z >= 0)) {
                return _blocks[x, y, z];
            }
            return 0;
        }

        public IEnumerator CreateMesh() {
            if (_mesh == null) {
                _mesh = GetComponent<MeshFilter>().mesh;
            }
            _verticies.Clear();
            _triangles.Clear();
            _uvs.Clear();
            _mesh.Clear();
            for (int x = 0; x < _sizeX; x++) {
                for (int y = 0; y < _sizeY; y++) {
                    for (int z = 0; z < _sizeZ; z++) {
                        int block = _blocks[x, y, z];
                        if (block != 0) {
                            // get the block dictionary so we can check if the blocks are transparent or not
                            //this way we do not have to store the transparancey in the block data structure.
                            
                            //check if bottom is touching air                           
                            if (GetBlock(x,y-1,z) == 0 || GetBlock(x,y-1,z) > 0 && BlockDictionary.Instance.GetBlockType(GetBlock(x,y-1,z)).IsTransparent) {
                                CreateCubeBottom(x, y, z, _map.BlockDict[block].BottomId);
                            }

                            if (GetBlock(x,y,z-1) == 0 || GetBlock(x,y,z-1) > 0 && BlockDictionary.Instance.GetBlockType(GetBlock(x,y,z-1)).IsTransparent) {
                                CreateCubeLeft(x, y, z, _map.BlockDict[block].LeftId);
                            }

                            if (GetBlock(x-1,y,z) == 0 || GetBlock(x-1,y,z) > 0 && BlockDictionary.Instance.GetBlockType(GetBlock(x-1,y,z)).IsTransparent) {
                                CreateCubeFront(x, y, z, _map.BlockDict[block].FrontId);
                            }

                            if (GetBlock(x+1,y,z) == 0 || GetBlock(x+1,y,z) > 0 && BlockDictionary.Instance.GetBlockType(GetBlock(x+1,y,z)).IsTransparent) {
                                CreateCubeBack(x, y, z, _map.BlockDict[block].BackId);
                            }

                            if (GetBlock(x,y,z+1)  == 0 || GetBlock(x,y,z+1) > 0 && BlockDictionary.Instance.GetBlockType(GetBlock(x,y,z+1)).IsTransparent) {
                                CreateCubeRight(x, y, z, _map.BlockDict[block].RightId);
                            }

                            if (GetBlock(x,y+1,z) == 0 || GetBlock(x,y+1,z) > 0 && BlockDictionary.Instance.GetBlockType(GetBlock(x,y+1,z)).IsTransparent) {
                                CreateCubeTop(x, y, z, _map.BlockDict[block].TopId);
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
            Loaded = true;
            yield return new WaitForSeconds(0.5f);
        }

        //load in textures from atlas
        private void PoulateTextures() {
            _textures = new List<Vector2>();
            for (int x = 0; x < 16; x++) {
                for (int y = 0; y < 16; y++) {
                    _textures.Add(new Vector2(x,y));
                }
            }
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

        private void SetUV(Vector2 textCoords) {
            //uvs
            _uvs.Add(new Vector2(TextureSize * textCoords.x, TextureSize + TextureSize * textCoords.y));
            _uvs.Add(new Vector2(TextureSize + TextureSize * textCoords.x, TextureSize + TextureSize * textCoords.y));
            _uvs.Add(new Vector2(TextureSize + TextureSize * textCoords.x, TextureSize * textCoords.y));
            _uvs.Add(new Vector2(TextureSize * textCoords.x, TextureSize * textCoords.y));

        }
    }
}