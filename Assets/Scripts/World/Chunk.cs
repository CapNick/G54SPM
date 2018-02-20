using System.Collections.Generic;
using UnityEngine;
using Models;

namespace World {
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Chunk : MonoBehaviour{

        public int SizeX = 16;
        public int SizeY = 16;
        public int SizeZ = 16;

        public float BlockSize = 1;
        
        private Block[,,] _blocks;
        
        //rendering
        private Mesh _mesh;
        private int _faceCounter = 0;

        private List<Vector3> _verticies;
        private List<int> _triangles;
        private List<Vector2> _uvs;

        public void Awake() {
            //block array
            _blocks = new Block[SizeX,SizeY,SizeZ];
            _verticies = new List<Vector3>();
            _triangles = new List<int>();
            _uvs = new List<Vector2>();
        }

        public void Start() {
            CreateBlocks();
            CreateMesh();
        }

        public void CreateBlocks() {
            //initalise the array of block structs
            for (int x = 0; x < SizeX; x++) {
                for (int y = 0; y < SizeY; y++) {
                    for (int z = 0; z < SizeZ; z++) {
                        _blocks[x,y,z] = new Block(x,y,z, true);
                    }
                }
            }
        }

        public Block GetBlock(int x, int y, int z) {
            return _blocks[x, y, z];
        }

        public void Render() {
            
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

            _mesh.vertices = _verticies.ToArray();
            _mesh.triangles = _triangles.ToArray();
            _mesh.uv = _uvs.ToArray();
            _faceCounter = 0;

        }

        private void CreateCubeBottom(int x, int y, int z) {
            //verticies
            _verticies.Add(new Vector3(x, y, z));
            _verticies.Add(new Vector3(x, y, z + BlockSize));
            _verticies.Add(new Vector3(x + BlockSize, y, z + BlockSize));
            _verticies.Add(new Vector3(x + BlockSize, y, z));
            SetTriAndUv();
        }
        private void CreateCubeLeft(int x, int y, int z) {
            //verticies
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z));
            _verticies.Add(new Vector3(x, y + BlockSize, z));
            _verticies.Add(new Vector3(x, y, z));
            _verticies.Add(new Vector3(x + BlockSize, y, z));
            SetTriAndUv();
        }
        private void CreateCubeFront(int x, int y, int z) {
            //verticies
            _verticies.Add(new Vector3(x, y + BlockSize, z));
            _verticies.Add(new Vector3(x, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x, y, z + BlockSize));
            _verticies.Add(new Vector3(x, y, z));
            SetTriAndUv();
        }
        private void CreateCubeBack(int x, int y, int z) {
            //verticies
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z));
            _verticies.Add(new Vector3(x + BlockSize, y, z));
            _verticies.Add(new Vector3(x + BlockSize, y, z + BlockSize));
            SetTriAndUv();
        }
        private void CreateCubeRight(int x, int y, int z) {
            //verticies
            _verticies.Add(new Vector3(x, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x + BlockSize, y, z + BlockSize));
            _verticies.Add(new Vector3(x, y, z + BlockSize));
            SetTriAndUv();
        }
        private void CreateCubeTop(int x, int y, int z) {
            //verticies
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z));
            _verticies.Add(new Vector3(x + BlockSize, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x, y + BlockSize, z + BlockSize));
            _verticies.Add(new Vector3(x, y + BlockSize, z));
            SetTriAndUv();
        }

        private void SetTriAndUv() {
            //triagles
            _triangles.Add(_faceCounter * 4 + 3);
            _triangles.Add(_faceCounter * 4 + 1);
            _triangles.Add(_faceCounter * 4 + 0);
            _triangles.Add(_faceCounter * 4 + 3);
            _triangles.Add(_faceCounter * 4 + 2);
            _triangles.Add(_faceCounter * 4 + 1);
            //uvs
            _uvs.Add(new Vector2(1f, 1f));
            _uvs.Add(new Vector2(0f, 1f));
            _uvs.Add(new Vector2(0f, 0f));
            _uvs.Add(new Vector2(1f, 0f));
            _faceCounter++;
        }
        
    }
}