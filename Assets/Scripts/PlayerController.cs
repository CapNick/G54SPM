using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobMoving))]
public class PlayerController : MonoBehaviour {

    MobMoving moving;
    Camera cam;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
        moving = GetComponent<MobMoving>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit))
            {
                //check if player detected another player
                //interact accordingly

                moving.MoveTo(hit.point);
            }
        }
		
	}
}
