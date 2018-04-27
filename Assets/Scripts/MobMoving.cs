using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MobMoving : MonoBehaviour {

    NavMeshAgent agent;
	public GameObject player;
    public float range = 10;
    public float attackRange = 2;
	public float attackCoolDown = 1f;
	private float CoolDownTimer = 0;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Vector3.Distance(transform.position, player.transform.position ) < range) {
       
			MoveTo (player.transform.position);
            AttackPlayer();
		}
	}

    public void MoveTo(Vector3 point)
    {
        agent.SetDestination(point);
    }

    /* public void OnTriggerEnter(Collider col)
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
    */

    public void OnDrawGizmos() {
		if (agent != null) {
			Gizmos.color = new Color (255, 0, 0, 0.5f);
			Gizmos.DrawSphere (transform.position, range);
			Gizmos.DrawLine (transform.position, agent.destination);
		}
	}

    public void AttackPlayer()
    {
        //Ray ray = new Ray(transform.position,new Vector3(1f,0,0));
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, attackRange))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

	public void AttackTimer()
	{
		
	}
}
