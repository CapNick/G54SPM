﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float minimumX = -60f;
	public float maximumX = 60f;
	public float minimumY = -360f;
	public float maximumY = 360f;

	public float sensitivityX = 15f;
	public float sensitivityY = 15f;

	public Camera cam;

	float rotationY = 0f;
	float rotationx = 0f;

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
		rotationY += Input.GetAxis ("Mouse X") * sensitivityY;
		rotationx += Input.GetAxis ("Mouse Y") * sensitivityX;

		rotationx = Mathf.Clamp (rotationx, minimumX, maximumX);

		transform.localEulerAngles = new Vector3 (0, rotationY, 0);
		cam.transform.localEulerAngles = new Vector3 (-rotationx, rotationY, 0);
	}
}
