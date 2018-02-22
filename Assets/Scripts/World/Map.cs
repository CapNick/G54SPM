namespace World {
    //Container Class which holds the world information so that it can be saved easilly
    public class Map {

        public const int Width = 2;
        public const int Length = 2;
        
        private Chunk[,] _chunks = new Chunk[Length,Width];

    }
}