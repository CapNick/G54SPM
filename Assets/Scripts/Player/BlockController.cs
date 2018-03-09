using System;
using System.Collections.Generic;
using UnityEngine;
using World;

namespace Player {
	public class BlockController : MonoBehaviour {

		public Map Map;
		public Dictionary<int, BlockType> BlockDict;
		public Camera Cam;
		public float Range;
		public LayerMask Mask;
		[Range(1,15)]
		public int Id = 1;

		// Use this for initialization
		void Start () {
			BlockDict = Map.BlockDict;
		}
	
		// Update is called once per frame
		void Update () {
			if (Input.GetMouseButtonDown(0)) {
				PlaceBlock();
			}

			if (Input.GetMouseButtonDown(1)) {
				RemoveBlock();
			}
			//quick implimentation of block id changing using the 1 and 2 keys
			
			if (Input.GetKeyDown(KeyCode.Alpha2) && Id < 15) {
				Id++;
			}
			if (Input.GetKeyDown(KeyCode.Alpha1) && Id > 1) {
				Id--;
			}
		}
		
		private void PlaceBlock() {
			Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, Range)) {
				try {
					
					Vector3 position = new Vector3Int(
						Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.y), Mathf.FloorToInt(hit.point.z));
					// make sure we are on the outside on the block
					position += hit.normal * 0.5f;
					Debug.Log("<color=blue>BlockController ==> Place Block at ("+position.x+","+position.y+","+position.z+")</color>");
					// add the block
					Map.AddBlock(position, Id);
					Debug.DrawLine(ray.origin,ray.origin+( ray.direction*hit.distance),Color.green,2);

				}
				catch (Exception e) {
					Debug.Log("BlockController ==> Place Block ERROR: "+e);
				}
				
			}
		}
		
		private void RemoveBlock() {
			Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, Range)) {
				try {
					Vector3 position = new Vector3Int(
						Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.y), Mathf.FloorToInt(hit.point.z));
					// make sure we are on the inside on the block
					position += hit.normal * -0.5f;
					Debug.Log("<color=blue>BlockController ==> Remove Block at ("+position.x+","+position.y+","+position.z+")</color>");
					// remove the block
					Map.RemoveBlock(position);
					Debug.DrawLine(ray.origin,ray.origin+( ray.direction*hit.distance),Color.red,2);

				}
				catch (Exception e) {
					Debug.Log("BlockController ==> Remove Block ERROR: "+e);
				}
				
			}
		}
	}
}
