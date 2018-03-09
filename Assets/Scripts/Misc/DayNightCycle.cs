using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

	public Light sun;
	public float secondsinoneday = 120f;
	[Range(0,1)]
	public float currenttimeofday = 0f;
	[HideInInspector]
	public float timemultiplier = 1f;

	float InitialSunIntensity;

	// Use this for initialization
	void Start () 
	{
		InitialSunIntensity = sun.intensity;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateSun ();

		currenttimeofday += (Time.deltaTime / secondsinoneday) * timemultiplier;

		if (currenttimeofday >= 1) 
		{
			currenttimeofday = 0;
		}

	}

	void UpdateSun() 
	{
		sun.transform.localRotation = Quaternion.Euler ((currenttimeofday * 360f) - 90, 170, 0);

		float intensitymultiplier = 1f;
		if (currenttimeofday <= 0.23f || currenttimeofday >= 0.75f) {
			intensitymultiplier = 0f;
		} 
		else if (currenttimeofday <= 0.25f) {
			intensitymultiplier = Mathf.Clamp01 ((currenttimeofday - 0.23f) * (1 / 0.02f));
		} 
		else if (currenttimeofday >= 0.73f) {
			intensitymultiplier = Mathf.Clamp01 (1 - ((currenttimeofday - 0.73f) * (1 / 0.02f)));
		}

		sun.intensity = InitialSunIntensity * intensitymultiplier;
	}

}
