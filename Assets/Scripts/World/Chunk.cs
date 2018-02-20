using System.Collections.Generic;
using UnityEngine;
using Models;

namespace World {
    public class Chunk {

        public int SizeX = 16;
        public int SizeY = 16;
        public int SizeZ = 16;

        public float BlockSize = 1;
        
        private Block[,,] _blocks;

        private ChunkRenderer _renderer;
        
        //rendering
        private Mesh _mesh;

        private List<Vector3> _verticies;
        private List<int> _triangles;
        private List<Vector2> _uvs;

        public Chunk() {
            //block array
            _blocks = new Block[SizeX,SizeY,SizeZ];
            _verticies = new List<Vector3>();
            _triangles = new List<int>();
            _uvs = new List<Vector2>();
            _renderer = new ChunkRenderer();
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
        
    }
}