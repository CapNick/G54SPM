using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 

{
	
	public float walkSpeed = 500f;
	public float sprintSpeed = 1500f;
	public float crouchSpeed = 200f;
	public float jumpForce = 10f;
	public CapsuleCollider col;
	public Transform tr;
	Rigidbody rb;
	Vector3 MoveDirection;
	private bool Grounded;



	void Awake()
	{
		rb = GetComponent<Rigidbody>();

	}

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		col = GetComponent<CapsuleCollider> ();
		tr = GetComponent<Transform> ();
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
		tr.localScale = new Vector3 (1f, 1f, 1f);


		if (Input.GetKeyDown(KeyCode.Space) && Grounded == true) 
		{
			Vector3 jump = new Vector3 (0f, 500f, 0f);
			GetComponent<Rigidbody> ().AddForce (jump);
			Grounded = false;
		}

		if (Input.GetKey(KeyCode.LeftShift)) 
		{
			rb.velocity = MoveDirection * sprintSpeed * Time.deltaTime;
		}
			
		if (Input.GetKey(KeyCode.C)) 
		{
			tr.transform.localScale = new Vector3 (1f,0.5f,1f);
			rb.velocity = MoveDirection * crouchSpeed * Time.deltaTime;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		Grounded = true;
	}

}


