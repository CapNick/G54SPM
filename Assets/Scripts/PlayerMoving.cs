using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMoving : MonoBehaviour {

    NavMeshAgent agent;
	GameObject player;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (player != null) {
			MoveTo (player.transform.position);
		}
	}

    public void MoveTo(Vector3 point)
    {
        agent.SetDestination(point);
    }

	public void OnTriggerEnter(Collider col)
	{
		if (col.transform.CompareTag ("Player"))
		{
			player = col.gameObject;
		}
	}

	public void OnTriggerExit(Collider col)
	{
		Debug.Log ("Not Being Triggered By ... " + col.gameObject.name);
		if (col.transform.CompareTag ("Player"))
		{
			agent.SetDestination (transform.position);
			player = null;
		}
	}
}
