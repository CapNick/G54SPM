using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float MinimumX = -60f;
	public float MaximumX = 60f;
	public float MinimumY = -360f;
	public float MaximumY = 360f;

	public float SensitivityX = 15f;
	public float SensitivityY = 15f;

	public Camera Cam;

	float _rotationY = 0f;
	float _rotationx = 0f;

	// Use this for initialization
	void Start () 
	{
		Cursor.lockState = CursorLockMode.Locked;

		if (Input.GetKey (KeyCode.Escape)) 
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		_rotationY += Input.GetAxis ("Mouse X") * SensitivityY;
		_rotationx += Input.GetAxis ("Mouse Y") * SensitivityX;

		_rotationx = Mathf.Clamp (_rotationx, MinimumX, MaximumX);

		transform.localEulerAngles = new Vector3 (0, _rotationY, 0);
		Cam.transform.localEulerAngles = new Vector3 (-_rotationx, 0, 0);
	}
}
