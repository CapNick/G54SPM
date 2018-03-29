using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace World {
    public sealed class BlockDictionary {
        private static volatile BlockDictionary instance;
        private static object syncRoot = new Object();
        private Dictionary<int, BlockType> blockDict = new Dictionary<int, BlockType>();
        private BlockDictionary() {
            
        }
        
        public static BlockDictionary Instance {
            get {
                if (instance == null) {
                    lock (syncRoot) {
                        if (instance == null) 
                            instance = new BlockDictionary();
                    }
                }
                return instance;
            }
        }
        
        public void LoadAllData (string filename) {
            string filePath = Path.Combine(Application.streamingAssetsPath, filename);
            string _dataAsJson;
            List<BlockType> types;
            if (File.Exists(filePath)) {
                _dataAsJson = File.ReadAllText(filePath);
                // Pass the json to JsonUtility, and tell it to create a GameData object from it.
                types = JsonConvert.DeserializeObject<List<BlockType>>(_dataAsJson);

                foreach (var blockType in types) {
                    blockDict.Add(blockType.Id, blockType);
                }
                
            }
            
        }

        public Dictionary<int, BlockType> GetAllData() {
            return blockDict; 
        }

        public BlockType GetBlockType(int id) {
            if (blockDict.ContainsKey(id)) {
                return blockDict[id];
            }
            BlockType blank = new BlockType();
            blank.Id = 255;
            return blank;
        }
        
    }
}