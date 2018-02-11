using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
	public bool Breakable = false;
	public int X { private set; get; }
	public int Y { private set; get; }

	public void SetPosition(int x, int y) {
		X = x;
		Y = y;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
