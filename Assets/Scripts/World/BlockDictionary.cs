using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace World {
    public static class BlockDictionary {
        public static Dictionary<int, BlockType> LoadAllData (string filename) {
            Dictionary<int, BlockType> blockDict = new Dictionary<int, BlockType>(); 
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
                
                return blockDict;
            }
            else {
                return null;
            }
        }
        
    }
}