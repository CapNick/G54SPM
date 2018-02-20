using System.Collections.Generic;
using UnityEngine;

namespace Models {
    [RequireComponent(typeof (MeshFilter))]
    [RequireComponent(typeof (MeshRenderer))]
    public class MeshTest : MonoBehaviour {
        public Mesh mesh;
        public Material mat;

        public float Size = 1;

        private List<Vector3> _verticies = new List<Vector3>();
        private List<int> _triagles = new List<int>();
        
        public void Start() {
            //we will create the mesh in here

            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            
            
            int vertexIndex = _verticies.Count;
            
            //top verticies
            Vector3 v0 = new Vector3(x, y + Size/2, z);
            Vector3 v1 = new Vector3(x, y + Size/2, z + Size);
            Vector3 v2 = new Vector3(x + Size, y + Size/2, z + Size);
            Vector3 v3 = new Vector3(x + Size, y + Size/2, z);
            //bottom verticies
            Vector3 v4 = new Vector3(x, y - Size/2, z);
            Vector3 v5 = new Vector3(x, y - Size/2, z + Size);
            Vector3 v6 = new Vector3(x + Size, y - Size/2, z + Size);
            Vector3 v7 = new Vector3(x + Size, y - Size/2, z);
            
            
            //top ======
            _verticies.Add(v0);
            _verticies.Add(v1);
            _verticies.Add(v2);
            _verticies.Add(v3);
 
            // first triangle for the block top
            _triagles.Add(vertexIndex);
            _triagles.Add(vertexIndex+1);
            _triagles.Add(vertexIndex+2);
                         
            // second triangle for the block top
            _triagles.Add(vertexIndex+2);
            _triagles.Add(vertexIndex+3);
            _triagles.Add(vertexIndex);
            
            //bottom ==========
            _verticies.Add(v4);
            _verticies.Add(v5);
            _verticies.Add(v6);
            _verticies.Add(v7);
            
            // first triangle for the block bottom
            _triagles.Add(vertexIndex+4+2);
            _triagles.Add(vertexIndex+4+1);
            _triagles.Add(vertexIndex+4);
                         
            // second triangle for the block bottom
            _triagles.Add(vertexIndex+4);
            _triagles.Add(vertexIndex+4+3);
            _triagles.Add(vertexIndex+4+2);


            mesh = GetComponent<MeshFilter>().mesh;
            mesh.vertices = _verticies.ToArray();
            mesh.triangles = _triagles.ToArray();
            mesh.RecalculateNormals();
//            mesh.RecalculateTangents();
            
        }
        public void Update() {
            
        }
    }
}