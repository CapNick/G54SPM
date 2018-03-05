using System;

namespace World {
    [Serializable]
    public struct Block {
        public int Id;
        public bool IsActive;
        public Block(int id, bool active) {
            Id = id;
            IsActive = active;
        }

    }
}