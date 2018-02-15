using System.Collections.Generic;
namespace World {
    //Container Class which holds the world information so that it can be saved easilly
    public class Map {
        
        public int Seed { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Length { get; private set; }

        public Dictionary<int,Block> Blocks;
        
        public Map(int seed, int width, int height, int length) {
            Seed = seed;
            Width = width;
            Height = height;
            Length = length;
            Blocks = new Dictionary<int,Block>();
        }

    }
}