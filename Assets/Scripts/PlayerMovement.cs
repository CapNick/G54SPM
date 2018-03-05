using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 

{
	
	public float movementSpeed;
	public float walkSpeed = 500f;
	public float sprintSpeed = 1500f;
	public float crouchSpeed = 250f;
	public float jumpForce = 10f;
	public CapsuleCollider col;
	public Camera cam;
	Rigidbody rb;
	Vector3 MoveDirection;
	public bool Grounded;
	public bool Crouching;



	void Awake()
	{
		rb = GetComponent<Rigidbody>();

	}

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		col = GetComponent<CapsuleCollider> ();
		movementSpeed = walkSpeed;
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
		movementSpeed = walkSpeed;
		
		if (Input.GetKeyDown(KeyCode.Space) && Grounded) 
		{
			Vector3 jump = new Vector3 (0f, 500f, 0f);
			GetComponent<Rigidbody> ().AddForce (jump);
			Grounded = false;
		}

		if (Input.GetKey(KeyCode.LeftShift)) 
		{
			movementSpeed = sprintSpeed;
		}

		if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl)) 
		{
			movementSpeed = crouchSpeed;
		}
			
			
		if ((Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftControl)) && !Crouching) 
		{
			cam.transform.localPosition = Vector3.zero;
			Crouching = true;
		}


		if ((Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.LeftControl)) && Crouching) 
		{
			cam.transform.localPosition = new Vector3(0, 0.5f,0);
			Crouching = false;
		}
			
		Vector3 yVelFix = new Vector3 (0, rb.velocity.y, 0);
		rb.velocity = MoveDirection * movementSpeed * Time.deltaTime;
		rb.velocity += yVelFix;


	}

	void OnCollisionEnter(Collision other)
	{
		Grounded = true;
	}

}


