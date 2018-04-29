using UnityEngine;

namespace World.Features {
    public static class Tree {
        public static void CreateTreeType1(Map map, Vector3 position, int leafType, int woodType) {
            map.AddBlock(position, woodType);
            //trunk of tree
            map.AddBlock(new Vector3(position.x, position.y+1, position.z), woodType);
            map.AddBlock(new Vector3(position.x, position.y+2, position.z), woodType);
            map.AddBlock(new Vector3(position.x, position.y+3, position.z), woodType);
            //leafs layer 2
            map.AddBlock(new Vector3(position.x+1, position.y+2, position.z+1), leafType);
            map.AddBlock(new Vector3(position.x-1, position.y+2, position.z+1), leafType);
            map.AddBlock(new Vector3(position.x+1, position.y+2, position.z-1), leafType);
            map.AddBlock(new Vector3(position.x-1, position.y+2, position.z-1), leafType);
            map.AddBlock(new Vector3(position.x, position.y+2, position.z-1), leafType);
            map.AddBlock(new Vector3(position.x, position.y+2, position.z+1), leafType);
            map.AddBlock(new Vector3(position.x+1, position.y+2, position.z), leafType);
            map.AddBlock(new Vector3(position.x-1, position.y+2, position.z), leafType);
            //leafs layer 2
            map.AddBlock(new Vector3(position.x+1, position.y+3, position.z+1), leafType);
            map.AddBlock(new Vector3(position.x-1, position.y+3, position.z+1), leafType);
            map.AddBlock(new Vector3(position.x+1, position.y+3, position.z-1), leafType);
            map.AddBlock(new Vector3(position.x-1, position.y+3, position.z-1), leafType);
            map.AddBlock(new Vector3(position.x, position.y+3, position.z-1), leafType);
            map.AddBlock(new Vector3(position.x, position.y+3, position.z+1), leafType);
            map.AddBlock(new Vector3(position.x+1, position.y+3, position.z), leafType);
            map.AddBlock(new Vector3(position.x-1, position.y+3, position.z), leafType);
            //top
            map.AddBlock(new Vector3(position.x, position.y+4, position.z), leafType);
            map.AddBlock(new Vector3(position.x, position.y+5, position.z), leafType);

            
        }
    }
}