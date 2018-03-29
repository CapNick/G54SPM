using System;

namespace World {
    [Serializable]
    public struct Block {
        public byte Id;
        public bool IsActive;
        public Block(byte id, bool active) {
            Id = id;
            IsActive = active;
        }
    }
}