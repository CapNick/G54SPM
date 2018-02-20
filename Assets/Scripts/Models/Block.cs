namespace Models {
    public struct Block {
        public bool IsActive;
        public int X,Y,Z;

        public Block(int x, int y, int z, bool active) {
            X = x;
            Y = y;
            Z = z;
            IsActive = active;
        }

    }
}