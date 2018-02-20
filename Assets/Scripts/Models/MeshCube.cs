using UnityEngine;

namespace Models {
    public class MeshCube {
        private float _length = 1.0f;
        private float _height = 1.0f;
        private float _width = 1.0f;
        
        private Mesh mesh;
        public Mesh CreateCube() {
            
            //since only we only need to go to 8 points
            //there is little point making multiple instances of a Vector3

            #region Verticies Made Easy
            //bottom part of the cube
            Vector3 v0 = new Vector3(-_length * 0.5f, -_width * 0.5f, _height * 0.5f);
            Vector3 v1 = new Vector3(_length * 0.5f, -_width * 0.5f, _height * 0.5f);
            Vector3 v2 = new Vector3(_length * 0.5f, -_width * 0.5f, -_height * 0.5f);
            Vector3 v3 = new Vector3(-_length * 0.5f, -_width * 0.5f, -_height * 0.5f);

            //top part of the cube
            Vector3 v4 = new Vector3(-_length * 0.5f, _width * 0.5f, _height * 0.5f);
            Vector3 v5 = new Vector3(_length * 0.5f, _width * 0.5f, _height * 0.5f);
            Vector3 v6 = new Vector3(_length * 0.5f, _width * 0.5f, -_height * 0.5f);
            Vector3 v7 = new Vector3(-_length * 0.5f, _width * 0.5f, -_height * 0.5f);
            #endregion
            
            //array with the verticies positions
            Vector3[] verts = {
                // Bottom
                v0, v1, v2, v3,
 
                // Left
                v7, v4, v0, v3,
 
                // Front
                v4, v5, v1, v0,
 
                // Back
                v6, v7, v3, v2,
 
                // Rvght
                v5, v6, v2, v1,
 
                // Top
                v7, v6, v5, v4
            };
            //array with the triagles of the connected verticies
            int[] triangles = {
                0, 2, 1, //face front
                0, 3, 2,
                2, 3, 4, //face top
                2, 4, 5,
                1, 2, 5, //face right
                1, 5, 6,
                0, 7, 4, //face left
                0, 4, 3,
                5, 4, 7, //face back
                5, 7, 6,
                0, 6, 7, //face bottom
                0, 1, 6
            };
            //array with the uvs so that textures can be applied
            
            mesh.vertices = verts;
            mesh.triangles = triangles;
            mesh.RecalculateNormals ();
            
            return mesh;
        }
    }
}