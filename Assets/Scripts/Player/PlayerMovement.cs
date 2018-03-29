using UnityEngine;

namespace Player {
	[RequireComponent(typeof(CharacterController))]
	public class PlayerMovement : MonoBehaviour 

	{
		public float Speed = 6.0F;
		public float JumpSpeed = 8.0F;
		public float Gravity = 20.0F;
		private Vector3 _moveDirection = Vector3.zero;
		private int _drawDistance = 6;
	
		void Update() {
			CharacterController controller = GetComponent<CharacterController>();
			if (controller.isGrounded) {
				_moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
				_moveDirection = transform.TransformDirection(_moveDirection);
				_moveDirection *= Speed;
				if (Input.GetButton("Jump"))
					_moveDirection.y = JumpSpeed;
		
			}
			_moveDirection.y -= Gravity * Time.deltaTime;
			controller.Move(_moveDirection * Time.deltaTime);
		}

		private void OnDrawGizmos() {
			Gizmos.color = new Color(0,255,24,0.2f);
			Gizmos.DrawCube(transform.position, new Vector3(_drawDistance*16,_drawDistance,_drawDistance*16));
		}
	}
}


