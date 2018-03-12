using Player;
using UnityEngine;
using UnityEngine.UI;
using World;

namespace UI {
	public class BlockUI : MonoBehaviour {
		public Map Map;
		public BlockController cont;
		public Text BlockText;
		// Update is called once per frame
		void Update () {
			BlockText.text = Map.BlockDict[cont.Id]._Name;
		}
	}
}
