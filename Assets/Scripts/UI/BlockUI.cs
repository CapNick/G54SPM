using Player;
using UnityEngine;
using UnityEngine.UI;
using World;

namespace UI {
	public class BlockUI : MonoBehaviour {
		public BlockController cont;
		public Text BlockText;
		// Update is called once per frame
		void Update () {
			BlockText.text = BlockDictionary.Instance.GetBlockType(cont.Id)._Name;
		}
	}
}
