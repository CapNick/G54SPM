namespace World {
    public struct Block {
        public int X,Y,Z;
        public int Id;
//        public bool IsActive;


        public Block(int x, int y, int z, int id) {
            X = x;
            Y = y;
            Z = z;
            Id = id;
//            IsActive = active;
        }

    }
}