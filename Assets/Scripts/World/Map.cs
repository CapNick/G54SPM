using System.Collections.Generic;
using UnityEngine;

namespace World {
    //Container Class which holds the world information so that it can be saved easilly
    public class Map : MonoBehaviour{
        public List<Chunk> Chunks = new List<Chunk>();
        public Dictionary<int, BlockType> BlockDict;

        public void Awake() {
            BlockDict = BlockDictionary.LoadAllData("block_dictionary.json");
            if (BlockDict != null) {
                Debug.Log("BlockDictionary ==> Block Data Loaded Sucessfully");
            }
            else {
                Debug.Log("BlockDictionary ==> ERROR ==> There was an issue with loading the block data");
            }
        }

        public Block GetBlock(Vector3 positon) {
            return new Block(0, false);
        }
    }
}