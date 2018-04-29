using UnityEngine;

namespace World.Features {
    public class Tree {
        public void CreateTreeType1(Map map, Vector3 position, int leafType, int woodType) {
            map.AddBlock(position, woodType);
            map.AddBlock(new Vector3(position.x, position.y+1, position.z), woodType);
            map.AddBlock(new Vector3(position.x, position.y+2, position.z), woodType);
            map.AddBlock(new Vector3(position.x, position.y+3, position.z), woodType);
            
        }
    }
}