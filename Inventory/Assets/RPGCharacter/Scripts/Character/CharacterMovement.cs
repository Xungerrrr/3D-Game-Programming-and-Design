using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LylekGames.RPGCharacter {
	public class CharacterMovement : MonoBehaviour {
		
		private bool sliding = false;
		private Vector3 slidingDirection;

		public float baseSpeed = 2.0f;
		[HideInInspector]
		public float gravity = 9.81f;
		[HideInInspector]
		public float turnSpeed = 6.0f;

		private Vector3 moveDirection = Vector3.zero;
		public CharacterController controller;
		public Animator animator;

		void Awake() {
			controller = GetComponent<CharacterController>();
			animator = GetComponent<Animator>();
		}
		public void Update() {
			//Apply Gravity
			if (moveDirection.y > gravity * -1) {
				moveDirection.y -= gravity * Time.deltaTime;
			}
			controller.Move (moveDirection * Time.deltaTime);

			//Slide down slope
			if(sliding) {
				controller.Move(slidingDirection * 2 * Time.deltaTime);
			}
		}

		public void ChangeDirection(Quaternion newRot) {
			transform.rotation = Quaternion.RotateTowards (transform.localRotation, newRot, turnSpeed);
		}
		public void SetAnimatorParameter(string name, float value) {
			float curValue = animator.GetFloat (name);
			if (curValue != value) {
				StartCoroutine (LerpAnimationValue (name, value));
			}
		}
		public bool SetInCombat(bool inCombat) {
			animator.SetBool ("inCombat", inCombat);
			return inCombat;
		}
		public bool inCombat() {
			bool inCombat = animator.GetBool ("inCombat");
			return inCombat;
		}
		public void MoveForward(float speed) {
			controller.SimpleMove(transform.forward * baseSpeed * speed);
		}
		public void MoveBackward(float speed) {
			controller.SimpleMove(-transform.forward * baseSpeed * speed);
		}
		public void MoveLeft(float speed) {
			controller.SimpleMove(-transform.right * baseSpeed * speed);
		}
		public void MoveRight(float speed) {
			controller.SimpleMove(transform.right * baseSpeed * speed);
		}
		public void Idle() {
			controller.SimpleMove(transform.forward * 0.0f);
		}
		public IEnumerator LerpAnimationValue(string name, float value) {
			float curValue = animator.GetFloat (name);
			if (value > curValue) {
				while (value > curValue) {
					curValue += 2 * Time.deltaTime;
					animator.SetFloat (name, curValue);
					if (curValue >= value) {
						animator.SetFloat (name, value);
						break;
					}
					yield return null;
				}
			} else if (value < curValue) {
				while (value < curValue) {
					curValue -= 2 * Time.deltaTime;
					animator.SetFloat (name, curValue);
					if (curValue <= value) {
						animator.SetFloat (name, value);
						break;
					}
					yield return null;
				}
			}
		}
		void OnControllerColliderHit(ControllerColliderHit hit) {
			//PUSH PHYSICS-BASED OBJECTS OUT OF THE WAY
			if(hit.transform.GetComponent<Rigidbody>()) {
				hit.transform.GetComponent<Rigidbody>().AddForce(transform.forward * 20);
			}

			//GET SLOPE ANGLE
			var angle = Vector3.Angle(Vector3.up, hit.normal);
			if(angle > 45f) {
				//IF SLOPE IS HIGH, GET OUR SLIDING ANGLE
				//sliding is true
				sliding = true;

				//get sliding direction
				Vector3 normal = hit.normal;
				Vector3 c = Vector3.Cross(Vector3.up, normal);
				Vector3 u = Vector3.Cross(c, normal);
				slidingDirection = u * 4f;

			}
			else {
				//sliding is false
				sliding = false;
				//sliding direction is zero
				slidingDirection = Vector3.zero;
			}
		}

	}
}
