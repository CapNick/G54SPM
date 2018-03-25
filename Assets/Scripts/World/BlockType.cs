using System;

namespace World {
    [Serializable]
    public struct BlockType {
        public int Id;
        public string _Name;
        public bool IsTransparent;
        public int BottomId;
        public int TopId;
        public int FrontId;
        public int BackId;
        public int LeftId;
        public int RightId;
    }
}