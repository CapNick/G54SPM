using NUnit.Framework;
using UnityEngine;
using World;

namespace Tests {
    public class MapUnloaderTest {
        private Map _map;

        [SetUp]
        public void Init() {
            Vector3Int position = new Vector3Int(0,0,0);
            
            // Needs to have mono behaviour in the tests? so we can create the chunk and save it to file?.
//            _map.Chunks.Add(position.ToString(),);
        }

        [Test]
        public void TestMapSavesToFile() {
            
        }
        

    }
}