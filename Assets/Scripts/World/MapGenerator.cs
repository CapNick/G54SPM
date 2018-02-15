using UnityEngine;

namespace World {
	public class MapGenerator : MonoBehaviour {
		public GameObject[] PrefabBlocks;
		
		//map key information
		public int Height;
		public int Width;
		public int Length;
		
		//generation variables
		public int Seed;
		
		
		//stores the information on the map including; blocks, width, height, length and seed.


		public void Start() {
			//set the map container for saving
			//generate map
			for (int l = 0; l < Length; l++) {
				for (int w = 0; w < Width; w++) {
					for (int h = 0; h < Height; h++) {
						GameObject block;
						if (h == Height-2) {
							block = Instantiate(PrefabBlocks[2], new Vector3(l, h, w), transform.rotation);
						}
						else if (h == Height-1) {
							block = Instantiate(PrefabBlocks[3], new Vector3(l, h, w), transform.rotation);
						}
						else {
							block = Instantiate(PrefabBlocks[1], new Vector3(l, h, w), transform.rotation);
						}
						block.transform.SetParent(transform);
						block.name = "Block" + ":" + l + ", " + h + ", " + w;

					}
				}
			}
		}

		public void Update() {
			
		}

	}
}
