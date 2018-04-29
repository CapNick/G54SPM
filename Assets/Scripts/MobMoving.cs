using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Reference -
//https://docs.unity3d.com/ScriptReference/Physics.Raycast.html

[RequireComponent(typeof(NavMeshAgent))]
public class MobMoving : MonoBehaviour {

    NavMeshAgent _agent;
	public GameObject Player;
    public float Range = 10;
    public float AttackRange = 5;
	public float AttackCoolDown = 3f;
	public float Damage = 5f;
	float _coolDownTimer = 0;
	
    // Use this for initialization
    void Start () {
        _agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Vector3.Distance(transform.position, Player.transform.position ) < Range) {
       
			MoveTo (Player.transform.position);
            AttackPlayer();
		}
        AttackTimer();
    }

    public void MoveTo(Vector3 point)
    {
        _agent.SetDestination(point);
    }

    public void OnDrawGizmos() {
		if (_agent != null) {
			Gizmos.color = new Color (255, 0, 0, 0.5f);
			Gizmos.DrawSphere (transform.position, Range);
			Gizmos.DrawLine (transform.position, _agent.destination);
		}
	}

    public void AttackPlayer()
    {
        RaycastHit _hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit, AttackRange) && _coolDownTimer == 0)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
			if (_hit.transform.CompareTag ("Player")) {
				_hit.transform.GetComponent<Health> ().TakeDamage (Damage);
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
        if (_coolDownTimer == 0)
        {
            _coolDownTimer = AttackCoolDown;
        }

        if (_coolDownTimer > 0)
        {
            _coolDownTimer -= Time.deltaTime;
        }

		if (_coolDownTimer < 0)
        {
            _coolDownTimer = 0;
        }
	}
}
