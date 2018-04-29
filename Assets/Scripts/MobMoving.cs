using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Reference -
//https://docs.unity3d.com/ScriptReference/Physics.Raycast.html

[RequireComponent(typeof(NavMeshAgent))]
public class MobMoving : MonoBehaviour {

    NavMeshAgent agent;
	public GameObject player;
    public float range = 10;
    public float attackRange = 5;
	public float attackCoolDown = 3f;
	public float Damage = 5f;
	float coolDownTimer = 0;
	
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
        AttackTimer();
    }

    public void MoveTo(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void OnDrawGizmos() {
		if (agent != null) {
			Gizmos.color = new Color (255, 0, 0, 0.5f);
			Gizmos.DrawSphere (transform.position, range);
			Gizmos.DrawLine (transform.position, agent.destination);
		}
	}

    public void AttackPlayer()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, attackRange) && coolDownTimer == 0)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
			if (hit.transform.CompareTag ("Player")) {
				hit.transform.GetComponent<Health> ().TakeDamage (Damage);
			}
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    }

	public void AttackTimer()
	{
        if (coolDownTimer == 0)
        {
            coolDownTimer = attackCoolDown;
        }

        if (coolDownTimer > 0)
        {
            coolDownTimer = coolDownTimer - Time.deltaTime;
        }

		if (coolDownTimer < 0)
        {
            coolDownTimer = 0;
        }
	}
}
