using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace World {
    public class BlockDictionary : MonoBehaviour {
        public string Filename = "block_dictionary.json";
        [ShowInInspector]
        public Dictionary<int, BlockType> BlockDict { get; private set; } = new Dictionary<int, BlockType>();

        // on awake load json file with each of the blocks
        public void Awake() {
            Debug.Log(LoadAllData());
        }
        
        
        public string LoadAllData () {
            string filePath = Path.Combine(Application.streamingAssetsPath, Filename);
            string _dataAsJson;
            List<BlockType> types;
            if (File.Exists(filePath)) {
                _dataAsJson = File.ReadAllText(filePath);
                // Pass the json to JsonUtility, and tell it to create a GameData object from it.
                types = JsonConvert.DeserializeObject<List<BlockType>>(_dataAsJson);

                foreach (var blockType in types) {
                    BlockDict.Add(blockType.Id, blockType);
                }
                
                return "BlockDictionary ==> Block Data Loaded Sucessfully";
            }
            else {
                return "BlockDictionary ==> ERROR ==>There was an issue with loading the block data";
            }
        }
        
    }
}