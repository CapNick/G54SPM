using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


	public float walkSpeed;

	Rigidbody rb;
	Vector3 MoveDirection;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float horizontalMovement = Input.GetAxisRaw ("Horizontal");
		float verticalMovement = Input.GetAxisRaw ("Vertical");

		MoveDirection = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;
		Move ();
	}

	void FixedUpdate() 
	{
		
	}

	void Move() 
	{
		Vector3 yVelFix = new Vector3 (0, rb.velocity.y, 0);
		rb.velocity = MoveDirection * walkSpeed * Time.deltaTime;
		rb.velocity += yVelFix;
	}
}
