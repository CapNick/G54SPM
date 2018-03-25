using UnityEngine;

namespace Player {
	public class CameraController : MonoBehaviour {
		public float MinimumY = -70f;
		public float MaximumY = 70f;

		public float SensitivityX = 15f;
		public float SensitivityY = 15f;

		public Camera Cam;

		float _rotationY = 0f;
		float _rotationX = 0f;

		// Use this for initialization
		void Start () 
		{
			Cursor.lockState = CursorLockMode.Locked;

            
		}
	
		// Update is called once per frame
		void Update () 
		{ 
			if (Input.GetKey (KeyCode.Escape)) 
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			_rotationX += Input.GetAxis ("Mouse X") * SensitivityX;
			_rotationY += Input.GetAxis ("Mouse Y") * SensitivityY;
			_rotationY = Mathf.Clamp (_rotationY, MinimumY, MaximumY);

			transform.localEulerAngles = new Vector3 (0, _rotationX, 0);
			Cam.transform.localEulerAngles = new Vector3 (-_rotationY, 0, 0);
		}
	}
}
