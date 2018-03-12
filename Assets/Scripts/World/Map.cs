using System;
using System.Collections.Generic;
using UnityEngine;

namespace World {
    //Container Class which holds the world information so that it can be saved easilly
    public class Map : MonoBehaviour{
        public Dictionary<int, BlockType> BlockDict;

        
        [Space, Header("Chunk Information")]
        public List<Chunk> Chunks = new List<Chunk>();
        public int ChunkLength = 16;
        public int ChunkHeight = 16;
        public int ChunkWidth = 16;
        
        public void Awake() {
            BlockDict = BlockDictionary.LoadAllData("block_dictionary.json");
            if (BlockDict != null) {
                Debug.Log("<color=green>BlockDictionary ==> Block Data Loaded Sucessfully</color>");
            }
            else {
                Debug.Log("<color=red>BlockDictionary ==> ERROR ==> There was an issue with loading the block data</color>");
            }
        }

        public Chunk GetChunk(int x, int z) {
            //divide by the chunk width and height to get the chunk position
//            Debug.Log("<color=blue>Map ==> Get Chunk Before: ==> x:"+x+" z:"+z+"</color>");
            int chunkX = Mathf.FloorToInt(x / ChunkLength);
            if (x < 0) {
                chunkX--;
            }
            int chunkZ = Mathf.FloorToInt(z / ChunkWidth);
            if (z < 0) {
                chunkZ--;
            }
            Debug.Log("<color=blue>Map ==> Get Chunk ==> x:"+chunkX+" z:"+chunkZ+"</color>");

            return Chunks.Find(chunk => chunk.X == chunkX && chunk.Z == chunkZ);
        }

        public void UpdateChunk(int x, int z) {
            Debug.Log("<color=blue>Map ==> Update Chunk ==> x:"+x+" z:"+z+"</color>");
            GetChunk(x,z).RenderChunk();
        }

        public void RemoveBlock(Vector3 position) {
            //get the current chunk this block is located on
            Chunk chunk = GetChunk((int) position.x, (int) position.z);
            int breakPosX = (int)position.x - chunk.X * ChunkLength;
            int breakPosZ = (int)position.z - chunk.Z * ChunkWidth;
            //use math.abs to make sure the value is always positive
            breakPosX = Math.Abs(breakPosX);
            breakPosZ = Math.Abs(breakPosZ);
            
            Debug.Log("<color=blue>Map ==> Remove Block ==> Pos: (" + breakPosX + ", " + position.y + ", " +
                      breakPosZ+"), Chunk: ("+chunk.X+","+chunk.Z+")</color>");
            //set block to air
            Block block = chunk.GetBlock(breakPosX, (int) position.y, breakPosZ);
            if (block.IsActive) {
                chunk.UpdateBlock(breakPosX, (int)position.y, breakPosZ, false, BlockDict[0].Id);
                chunk.RenderChunk();
            }
        }

        public void AddBlock(Vector3 position, int blockId) {
            //get the current chunk this block is located on
            Chunk chunk = GetChunk((int) position.x, (int) position.z);
            int breakPosX = (int)position.x - chunk.X * ChunkLength;
            int breakPosZ = (int)position.z - chunk.Z * ChunkWidth;
            //use math.abs to make sure the value is always positive
            breakPosX = Math.Abs(breakPosX);
            breakPosZ = Math.Abs(breakPosZ);
            
            Debug.Log("<color=blue>Map ==> Add Block ==> ID:" + blockId + ", Pos: (" + breakPosX + ", " + position.y + ", " +
                      breakPosZ+"), Chunk: ("+chunk.X+","+chunk.Z+")</color>");
            
            Block block = chunk.GetBlock(breakPosX, (int) position.y, breakPosZ);
            if (!block.IsActive) {
                chunk.UpdateBlock(breakPosX, (int)position.y, breakPosZ, true, BlockDict[blockId].Id);
                chunk.RenderChunk();
            }
            
        }
    }
}