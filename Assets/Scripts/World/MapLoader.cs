using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace World {
    public class MapLoader {
        // loads map from json file
        // along with settings for map (seed, other generation properties)
        
        private static string _settingName = "settings.json";
        private static string _chunksDictionary = "chunks.json";
        private string _folderPath;

        public MapLoader(string mapName) {
            _folderPath = Path.Combine(Application.streamingAssetsPath, "Maps", mapName);            
        }

        public bool LoadMapSettings() {
            string filename = Path.Combine(_folderPath, _settingName);
            if (File.Exists(filename)) {
                string dataAsJson = File.ReadAllText(filename);
                return true;
            }
            return false;
        }

        public bool LoadMapChunks() {
            string filename = Path.Combine(_folderPath, _chunksDictionary);
            if (File.Exists(filename)) {
                string dataAsJson = File.ReadAllText(filename);
                return true;
            }
            return false;
        }
        
        
        
    }
}