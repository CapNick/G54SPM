using NUnit.Framework;
using World;

namespace Tests {
    public class MapLoaderTest {        
        [Test]
        public void FailsDetectMapChunks() {
            MapLoader loader = new MapLoader("bla");
            Assert.IsFalse(loader.LoadMapChunks());
        }
        
        [Test]
        public void SucceedsDetectMapChunks() {
            MapLoader loader = new MapLoader("Test");
            Assert.IsTrue(loader.LoadMapChunks());
        }
        
        [Test]
        public void FailsDetectMapSettings() {
            MapLoader loader = new MapLoader("bla");
            Assert.IsFalse(loader.LoadMapSettings());
        }
        
        [Test]
        public void SucceedsDetectMapSettings() {
            MapLoader loader = new MapLoader("Test"); 
            Assert.IsTrue(loader.LoadMapSettings());
        }
    }
}