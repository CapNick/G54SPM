using System;
using System.Collections.Generic;
using UnityEngine;

namespace World {
    //Container Class which holds the world information so that it can be saved easilly
    public class Map : MonoBehaviour{
        public Dictionary<int, BlockType> BlockDict;

        
        [Space, Header("Chunk Information")]
        public Dictionary<string, Chunk> Chunks = new Dictionary<string, Chunk>();

        public int Height = 256;
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
            int chunkY = Mathf.FloorToInt(position.y / ChunkHeight);
            int chunkZ = Mathf.FloorToInt(position.z / ChunkWidth);
//            Debug.Log("<color=blue>Map ==> Get Chunk ==> x:"+chunkX+" y:"+chunkY+" z:"+chunkZ+"</color>");
            string positionKey = new Vector3Int(chunkX,chunkY,chunkZ).ToString();
            if (Chunks.ContainsKey(positionKey)) {
                return Chunks[positionKey];
            }

            return null;
        }

        public int GetBlock(Vector3 worldPosition) {
            Chunk chunk = GetChunk(worldPosition);
            if (chunk != null) {
                Vector3 chunkPos = WorldPosToChunkPos(worldPosition, chunk);
                return chunk.GetBlock((int)chunkPos.x, (int)chunkPos.y, (int)chunkPos.z);
            }
            return 0;
        }

        public void UpdateChunk(Vector3 position) {
//            Debug.Log("<color=blue>Map ==> Update Chunk ==> x:"+position.x+" z:"+position.z+"</color>");
            GetChunk(position).CreateMesh();
        }

        public void RemoveBlock(Vector3 worldPosition) {
            //get the current chunk this block is located on
            Chunk chunk = GetChunk(worldPosition);
            if (chunk != null) {
                Vector3 chunkPos = WorldPosToChunkPos(worldPosition, chunk);

//            Debug.Log("<color=blue>Map ==> Remove Block ==> Pos: (" + (int)pos.x + ", " + (int)pos.y + ", " +
//                      (int)pos.z+"), Chunk: ("+chunk.Position+")</color>");
                //set block to air
                int block = chunk.GetBlock((int) chunkPos.x, (int) chunkPos.y, (int) chunkPos.z);
                if (block != 0) {
                    chunk.UpdateBlock((int) chunkPos.x, (int) chunkPos.y, (int) chunkPos.z, BlockDict[0].Id);
                    chunk.CreateMesh();
                }
            }
        }

        public void AddBlock(Vector3 worldPosition, int blockId) {
            //get the current chunk this block is located on           
            Chunk chunk = GetChunk(worldPosition);
            //check if the chunk even exsists
            if (chunk != null) {
                Vector3 chunkPos = WorldPosToChunkPos(worldPosition, chunk);

//            Debug.Log("<color=blue>Map ==> Add Block ==> ID:" + blockId + ", Pos: (" + (int)pos.x + ", " + (int)pos.y + ", " +
//                      (int)pos.z+"), Chunk: ("+chunk.Position+")</color>");
                //set block to air
                int block = chunk.GetBlock((int) chunkPos.x, (int) chunkPos.y, (int) chunkPos.z);
                if (block == 0 && chunkPos.y < Height) {

                    chunk.UpdateBlock((int) chunkPos.x, (int) chunkPos.y, (int) chunkPos.z, BlockDict[blockId].Id);
                    chunk.CreateMesh();
                }
            }

        }

        private Vector3 WorldPosToChunkPos(Vector3 pos, Chunk chunk) {
            int posX = (int)pos.x - chunk.Position.x * ChunkLength;
            int posY = (int)pos.y - chunk.Position.y * ChunkHeight;
            int posZ = (int)pos.z - chunk.Position.z * ChunkWidth;
            //use math.abs to make sure the value is always positive
            return new Vector3(Math.Abs(posX), Math.Abs(posY), Math.Abs(posZ));
        }
    }
}