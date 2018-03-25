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
            BlockDictionary.Instance.LoadAllData("block_dictionary.json");
            BlockDict = BlockDictionary.Instance.GetAllData();
        }

        public Chunk GetChunk(Vector3 position) {
            //divide by the chunk width and height to get the chunk position
            int chunkX = Mathf.FloorToInt(position.x / ChunkLength);
            int chunkZ = Mathf.FloorToInt(position.z / ChunkWidth);
            Debug.Log("<color=blue>Map ==> Get Chunk ==> x:"+chunkX+" z:"+chunkZ+"</color>");

            return Chunks.Find(chunk => chunk.X == chunkX && chunk.Z == chunkZ);
        }

        public Block GetBlock(Vector3 position) {
            Chunk chunk = GetChunk(position);
            Vector3 blockPos = RealPosToChunkPos(position, chunk);
            return chunk.GetBlock((int)blockPos.x, (int)blockPos.y, (int)blockPos.z);
        }

        public void UpdateChunk(Vector3 position) {
            Debug.Log("<color=blue>Map ==> Update Chunk ==> x:"+position.x+" z:"+position.z+"</color>");
            GetChunk(position).RenderChunk();
        }

        public void RemoveBlock(Vector3 position) {
            //get the current chunk this block is located on
            Chunk chunk = GetChunk(position);
            Vector3 pos = RealPosToChunkPos(position, chunk);
            
            Debug.Log("<color=blue>Map ==> Remove Block ==> Pos: (" + pos.x + ", " + pos.y + ", " +
                      pos.z+"), Chunk: ("+chunk.X+","+chunk.Z+")</color>");
            //set block to air
            Block block = chunk.GetBlock((int)pos.x, (int)pos.y, (int)pos.z);
            if (block.IsActive) {
                
                chunk.UpdateBlock((int)pos.x, (int)pos.y, (int)pos.z, BlockDict[0].Id, false);
                chunk.RenderChunk();
            }
        }

        public void AddBlock(Vector3 position, int blockId) {
            //get the current chunk this block is located on           
            Chunk chunk = GetChunk(position);
            Vector3 pos = RealPosToChunkPos(position, chunk);
            
            Debug.Log("<color=blue>Map ==> Add Block ==> ID:" + blockId + ", Pos: (" + (int)pos.x + ", " + (int)pos.y + ", " +
                      (int)pos.z+"), Chunk: ("+chunk.X+","+chunk.Z+")</color>");
            //set block to air
            Block block = chunk.GetBlock((int)pos.x, (int)pos.y, (int)pos.z);
            if (!block.IsActive) {
                
                chunk.UpdateBlock((int)pos.x, (int)pos.y, (int)pos.z, BlockDict[blockId].Id, true);
                chunk.RenderChunk();
            }
            
        }

        private Vector3 RealPosToChunkPos(Vector3 pos, Chunk chunk) {
            int posX = (int)pos.x - chunk.X * ChunkLength;
            int posZ = (int)pos.z - chunk.Z * ChunkWidth;
            //use math.abs to make sure the value is always positive
            return new Vector3(Math.Abs(posX), pos.y, Math.Abs(posZ));
        }
    }
}