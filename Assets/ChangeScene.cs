﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour {

	
	// Update is called once per frame
	public void ChangeToScene (int SceneToChangeTo) {
		Application.LoadLevel (SceneToChangeTo);
	}
}
