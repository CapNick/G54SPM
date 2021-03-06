﻿using UnityEngine;

namespace Player {
	[RequireComponent(typeof(CharacterController))]
	public class PlayerMovement : MonoBehaviour 

	{
		public Animator Animator;
		public float Speed = 6.0F;
		public float JumpSpeed = 8.0F;
		public float Gravity = 20.0F;
		private Vector3 _moveDirection = Vector3.zero;
	
		void Update() {
			
			CharacterController controller = GetComponent<CharacterController>();
			if (controller.isGrounded) {
				_moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
				_moveDirection = transform.TransformDirection(_moveDirection);
				_moveDirection *= Speed;
				if (Input.GetButton ("Jump")) 
				{
					_moveDirection.y = JumpSpeed;
				}
			}
//			Debug.Log (controller.velocity.magnitude);
			Animator.SetFloat("Speed", controller.velocity.magnitude);
			_moveDirection.y -= Gravity * Time.deltaTime;
			controller.Move(_moveDirection * Time.deltaTime);
		}
	}
}


