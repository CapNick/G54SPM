using System;

namespace World {
    [Serializable]
    public struct BlockType {
        public byte Id;
        public string _Name;
        public bool IsTransparent;
        public int Hardness;
        public int BottomId;
        public int TopId;
        public int FrontId;
        public int BackId;
        public int LeftId;
        public int RightId;
    }
}