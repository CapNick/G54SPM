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
                Debug.Log("<color=green>BlockDictionary ==> Block Data Loaded Sucessfully</color>");
            }
            else {
                Debug.Log("<color=red>BlockDictionary ==> ERROR ==> There was an issue with loading the block data</color>");
            }
        }

        public Block GetBlock(Vector3Int position) {
            return new Block(0, false);
        }

        public Chunk GetChunk(int x, int z) {
            Debug.Log("<color=blue>Map ==> Get Chunk ==> x:"+x+" z:"+z+"</color>");
            return Chunks.Find(chunk => chunk.X == x && chunk.Z == z);
        }

        public void UpdateChunk(int x, int z) {
            Debug.Log("<color=blue>Map ==> Update Chunk ==> x:"+x+" z:"+z+"</color>");
            GetChunk(x,z).RenderChunk();
        }

        public void RemoveBlock(Vector3 position, Chunk chunk) {
            Debug.Log("<color=blue>Map ==> Remove Block ==> Pos: (" + position.x + ", " + position.y + ", " +
                      position.z+"), Chunk: ("+chunk.X+","+chunk.Z+")</color>");
            chunk.UpdateBlock((int)position.x, (int)position.y, (int)position.z, false, BlockDict[0].Id);
            chunk.RenderChunk();
        }

        public void AddBlock(Vector3 position, Chunk chunk, int blockId) {
            Debug.Log("<color=blue>Map ==> Add Block ==> ID:" + blockId + ", Pos: (" + position.x + ", " + position.y + ", " +
                      position.z+"), Chunk: ("+chunk.X+","+chunk.Z+")</color>");
            chunk.UpdateBlock((int)position.x, (int)position.y, (int)position.z, true, BlockDict[blockId].Id);
            chunk.RenderChunk();
        }
        
    }
}