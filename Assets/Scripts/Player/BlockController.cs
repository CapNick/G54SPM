using System;
using System.Collections.Generic;
using UnityEngine;
using World;

namespace Player {
	public class BlockController : MonoBehaviour {

		public Map Map;
		public Camera Cam;
		public float Range;
		public LayerMask Mask;
		[Range(1,15)]
		public int Id = 1;
		
		private Vector3 _destroyPoint;
		private Vector3 _placePoint;

		public Vector3 norm;
		
		// Use this for initialization
		void Start () {
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
					Debug.Log("<color=blue>BlockController ==> True Place Block at ("+hit.point.x+","+hit.point.y+","+hit.point.z+")</color>");
					Vector3 position = new Vector3(
						Mathf.FloorToInt(hit.point.x), 
						Mathf.FloorToInt(hit.point.y), 
						Mathf.FloorToInt(hit.point.z));
					Vector3 normal = hit.normal;

					// make sure we are on the outside on the block
					if ((int)hit.normal.x == 1) {
						normal.x = 0;
					}
					if ((int)hit.normal.y == 1) {
						normal.y = 0;
					}
					if ((int)hit.normal.z == 1) {
						normal.z = 0;
					}
					position += normal;
					Debug.Log("<color=blue>BlockController ==> Place Block at ("+position.x+","+position.y+","+position.z+")</color>");
					// add the block
					_placePoint = position;
					Map.AddBlock(position, Id);
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
					Vector3 position = new Vector3(
						Mathf.FloorToInt(hit.point.x), 
						Mathf.FloorToInt(hit.point.y), 
						Mathf.FloorToInt(hit.point.z));
					Vector3 normal = hit.normal;

					// make sure we are on the outside on the block
					if ((int)hit.normal.x == -1) {
						normal.x = 0;
					}
					if ((int)hit.normal.y == -1) {
						normal.y = 0;
					}
					if ((int)hit.normal.z == -1) {
						normal.z = 0;
					}
					position -= normal;
					Debug.Log("<color=blue>BlockController ==> Remove Block at ("+position.x+","+position.y+","+position.z+")</color>");
					// remove the block
					_destroyPoint = position;
					Map.RemoveBlock(position);
				}
				catch (Exception e) {
					Debug.Log("BlockController ==> Remove Block ERROR: "+e);
				}
			}
		}

		public void OnDrawGizmos() {
			if (_destroyPoint != null) {
				Gizmos.color = new Color(255,0,0,0.5f);
				Gizmos.DrawCube(new Vector3(_destroyPoint.x+0.5f,_destroyPoint.y+0.5f,_destroyPoint.z+0.5f), new Vector3(1f,1f,1f));
				Gizmos.color = new Color(0,255,0,0.5f);
				Gizmos.DrawCube(new Vector3(_placePoint.x+0.5f,_placePoint.y+0.5f,_placePoint.z+0.5f), new Vector3(1f,1f,1f));
			}
		}
	}
}
