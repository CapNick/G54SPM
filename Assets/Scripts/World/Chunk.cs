using System.Collections.Generic;
using UnityEngine;
using Models;

namespace World {
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Chunk : MonoBehaviour {

        public int SizeX = 16;
        public int SizeY = 16;
        public int SizeZ = 16;

        public float BlockSize = 1;
        public float TextureSize = 0.0625f;
        
        private Block[,,] _blocks;
        
        //rendering
        private Mesh _mesh;
        private MeshCollider _collider;
        private int _faceCounter;

        private List<Vector3> _verticies;
        private List<int> _triangles;
        private List<Vector2> _uvs;
        private List<Vector2Int> _textures;
      

        public void Awake() {
            //block array
            _blocks = new Block[SizeX,SizeY,SizeZ];
            _verticies = new List<Vector3>();
            _triangles = new List<int>();
            _uvs = new List<Vector2>();
            _textures = new List<Vector2Int>();
            _collider = GetComponent<MeshCollider> ();
        }

        public void Start() {
            PoulateTextures();
            CreateBlocks();
            CreateMesh();
        }

        public void PoulateTextures() {
            for (int x = 0; x < 16; x++) {
                for (int y = 0; y < 16; y++) {
                    _textures.Add(new Vector2Int(x,y));
                }
            }
        }

        public void CreateBlocks() {
            //initalise the array of block structs
            for (int x = 0; x < SizeX; x++) {
                for (int y = 0; y < SizeY; y++) {
                    for (int z = 0; z < SizeZ; z++) {
                        _blocks[x,y,z] = new Block(x,y,z, 0,true);
                    }
                }
            }
        }

        public Block GetBlock(int x, int y, int z) {
            if ((x < SizeX && x >= 0) && (y < SizeY && y >= 0) && (z < SizeZ && z >= 0)) {
                return _blocks[x, y, z];
            }

            return new Block(-1,-1,-1, -1,false);
        }
        
        public void CreateMesh() {
            for (int x = 0; x < SizeX; x++) {
                for (int y = 0; y < SizeY; y++) {
                    for (int z = 0; z < SizeZ; z++) {
                        if (y == 0) {
                            CreateCubeBottom(x, y, z);
                        }
                        if (z == 0) {
                            CreateCubeLeft(x, y, z);
                        }
                        if (x == 0) {
                            CreateCubeFront(x, y, z);
                        }
                        if (x == SizeX-1) {
                            CreateCubeBack(x, y, z);
                        }
                        if (z == SizeZ-1) {
                            CreateCubeRight(x, y, z);
                        }
                        if (y == SizeY-1) {
                            CreateCubeTop(x, y, z);
                        }
                    }
                }
            }
            
            _mesh = GetComponent<MeshFilter>().mesh;
            _mesh.Clear();
            _mesh.vertices = _verticies.ToArray();
            _mesh.triangles = _triangles.ToArray();
            _mesh.uv = _uvs.ToArray();
            _mesh.RecalculateNormals();
            _mesh.RecalculateTangents();
            _collider.sharedMesh = _mesh;
            _faceCounter = 0;

        }

        private void CreateCubeBottom(int x, int y, int z) {
            //verticies
            _verticies.Add(new Vector3(x, y, z));
            _verticies.Add(new Vector3(x, y, z + BlockSize));
            _verticies.Add(new Vector3(x + BlockSize, y, z + BlockSize));
            _verticies.Add(new Vector3(x + BlockSize, y, z));
            SetTri();
            SetUV(_textures[31]);
        }
        private void CreateCubeLeft(int x, int y, int z) {
            //verticies
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z));
            _verticies.Add(new Vector3(x, y + BlockSize, z));
            _verticies.Add(new Vector3(x, y, z));
            _verticies.Add(new Vector3(x + BlockSize, y, z));
            SetTri();
            SetUV(_textures[31]);
        }
        private void CreateCubeFront(int x, int y, int z) {
            //verticies
            _verticies.Add(new Vector3(x, y + BlockSize, z));
            _verticies.Add(new Vector3(x, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x, y, z + BlockSize));
            _verticies.Add(new Vector3(x, y, z));
            SetTri();
            SetUV(_textures[31]);
        }
        private void CreateCubeBack(int x, int y, int z) {
            //verticies
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z));
            _verticies.Add(new Vector3(x + BlockSize, y, z));
            _verticies.Add(new Vector3(x + BlockSize, y, z + BlockSize));
            SetTri();
            SetUV(_textures[31]);
        }
        private void CreateCubeRight(int x, int y, int z) {
            //verticies
            _verticies.Add(new Vector3(x, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x + BlockSize, y, z + BlockSize));
            _verticies.Add(new Vector3(x, y, z + BlockSize));
            SetTri();
            SetUV(_textures[31]);
        }
        private void CreateCubeTop(int x, int y, int z) {
            //verticies
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z));
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x, y + BlockSize, z));
            SetTri();
            SetUV(_textures[31]);
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
            
//            _uvs.Add(new Vector2(0.0625f * id, 0.0625f * id));
//            _uvs.Add(new Vector2(0625f + 0.0625f * id, 0.0625f * id));
//            _uvs.Add(new Vector2(0.0625f * id, 0625f + 0.0625f * id));
//            _uvs.Add(new Vector2(0625f + 0.0625f * id, 0625f + 0.0625f * id));
        }
        
    }
}