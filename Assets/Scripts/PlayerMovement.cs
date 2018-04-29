using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


	public float WalkSpeed;

	Rigidbody _rb;
	Vector3 _moveDirection;

	void Awake()
	{
		_rb = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float _horizontalMovement = Input.GetAxisRaw ("Horizontal");
		float _verticalMovement = Input.GetAxisRaw ("Vertical");

		_moveDirection = (_horizontalMovement * transform.right + _verticalMovement * transform.forward).normalized;
		Move ();
	}

	void FixedUpdate() 
	{
		
	}

	void Move() 
	{
		Vector3 _yVelFix = new Vector3 (0, _rb.velocity.y, 0);
		_rb.velocity = _moveDirection * WalkSpeed * Time.deltaTime;
		_rb.velocity += _yVelFix;
	}
}
