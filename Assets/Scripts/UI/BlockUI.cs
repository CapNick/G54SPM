using Player;
using UnityEngine;
using UnityEngine.UI;
using World;

namespace UI {
	public class BlockUI : MonoBehaviour {
		public BlockController Cont;
		public Text BlockText;

		public Text BlockType;
		// Update is called once per frame
		void Update () {
			BlockText.text = BlockDictionary.Instance.GetBlockType(Cont.Id)._Name;
			BlockType.text = BlockDictionary.Instance.GetBlockType(Cont.SelectedBlock)._Name;
		}
	}
}
