using System;
using UnityEngine;
using World;

namespace Player {
	public class BlockController : MonoBehaviour {

		public Map Map;
		public Camera Cam;
		public float Range;
		[Range(1,15)]
		public int Id = 1;
		public bool Debug = true;
		public GameObject blockTrace;
		public int SelectedBlock;
		
		private Vector3 _destroyPoint;
		private Vector3 _placePoint;
		
		// Update is called once per frame
		void Update () {
			Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
			if (Debug) {
				DisplayBlockPlacement(ray);
			}

			SelectBlockInView();
			PlaceBlockTrace(ray);
			if (Input.GetMouseButtonDown(0)) {
				PlaceBlock(ray);
			}

			if (Input.GetMouseButtonDown(1)) {
				RemoveBlock(ray);
			}
			//quick implimentation of block id changing using the 1 and 2 keys
			
			if (Input.GetKeyDown(KeyCode.Alpha2) && Id < Map.BlockDict.Count-1) {
				Id++;
			}
			if (Input.GetKeyDown(KeyCode.Alpha1) && Id > 1) {
				Id--;
			}
		}

		private void SelectBlockInView() {
			if (_destroyPoint != null) {
				SelectedBlock = Map.GetBlock(_destroyPoint).Id;
			}
		}

		private void PlaceBlockTrace(Ray ray) {
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, Range, LayerMask.GetMask("World"))) {
				try {
					Vector3 position = new Vector3(
						Mathf.FloorToInt(hit.point.x)+0.5f, 
						Mathf.FloorToInt(hit.point.y)+0.5f, 
						Mathf.FloorToInt(hit.point.z)+0.5f);
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
					blockTrace.SetActive(true);
					blockTrace.transform.position = position;
				}
				catch (Exception) {
					// ignored
				}
			}
			else {
				blockTrace.SetActive(false);
			}
		}

		private void DisplayBlockPlacement(Ray ray) {
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, Range, LayerMask.GetMask("World"))) {
				Vector3 position = new Vector3(
					Mathf.FloorToInt(hit.point.x),
					Mathf.FloorToInt(hit.point.y),
					Mathf.FloorToInt(hit.point.z));
				Vector3 normal = hit.normal;
				Vector3 normalDel = hit.normal;

				// make sure we are on the outside on the block
				if ((int) hit.normal.x == 1) {
					normal.x = 0;
				}

				if ((int) hit.normal.y == 1) {
					normal.y = 0;
				}

				if ((int) hit.normal.z == 1) {
					normal.z = 0;
				}
				
				if ((int)hit.normal.x == -1) {
					normalDel.x = 0;
				}
				if ((int)hit.normal.y == -1) {
					normalDel.y = 0;
				}
				if ((int)hit.normal.z == -1) {
					normalDel.z = 0;
				}

				_placePoint = position + normal;
				_destroyPoint = position - normalDel;
			}
		}

		private void PlaceBlock(Ray ray) {
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, Range, LayerMask.GetMask("World"))) {
				try {
					Vector3 position = new Vector3(
						Mathf.FloorToInt(hit.point.x), 
						Mathf.FloorToInt(hit.point.y), 
						Mathf.FloorToInt(hit.point.z));
					Vector3 playerPosition = new Vector3(
						Mathf.FloorToInt(transform.position.x), 
						Mathf.FloorToInt(transform.position.y), 
						Mathf.FloorToInt(transform.position.z));
					
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
					
					UnityEngine.Debug.Log("<color=blue>BlockController ==> Place Block at ("+position.x+","+position.y+","+position.z+")</color>");
					// add the block
					
					UnityEngine.Debug.Log((int)position.x != (int)playerPosition.x && (int)position.y != (int)playerPosition.y && (int)position.z != (int)playerPosition.z);
					
					if ((int)position.x != (int)playerPosition.x || (int)position.y != (int)playerPosition.y || (int)position.z != (int)playerPosition.z) {
						Map.AddBlock(position, Id);
					}
				}
				catch (Exception e) {
					UnityEngine.Debug.Log("BlockController ==> Place Block ERROR: "+e);
				}	
			}
		}
		
		private void RemoveBlock(Ray ray) {
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, Range, LayerMask.GetMask("World"))) {
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
					UnityEngine.Debug.Log("<color=blue>BlockController ==> Remove Block at ("+position.x+","+position.y+","+position.z+")</color>");
					// remove the block
					Map.RemoveBlock(position);
				}
				catch (Exception e) {
					UnityEngine.Debug.Log("BlockController ==> Remove Block ERROR: "+e);
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
