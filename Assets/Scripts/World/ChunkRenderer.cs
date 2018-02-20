using System.Collections.Generic;
using UnityEngine;

namespace World {
    public class ChunkRenderer {
        
        private List<Vector3> _verticies;
        private List<int> _triangles;
        private List<Vector2> _uvs;
        
        
        
        
        public Mesh CreateMesh(Chunk chunk) {
            
            for (int x = 0; x < chunk.SizeX; x++) {
                for (int y = 0; y < chunk.SizeY; y++) {
                    for (int z = 0; z < chunk.SizeZ; z++) {
                        
                    }
                }
            }
            
            Mesh mesh = new Mesh();

            mesh.vertices = _verticies.ToArray();
            mesh.triangles = _triangles.ToArray();

            return mesh;

        }
    }
}