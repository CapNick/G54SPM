using System;

namespace World {
    [Serializable]
    public struct Block {
        public int Id;
        public bool IsActive;
        public bool IsTransparent;
        public Block(int id, bool active, bool isTransparent) {
            Id = id;
            IsActive = active;
            IsTransparent = isTransparent;
        }
    }
}