using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LylekGames.RPGCharacter;

namespace LylekGames.RPGCharacter {
	[RequireComponent(typeof(CharacterMovement))]
	public class PlayerInput : MonoBehaviour {

		//MOVEMENT KEYS
		[HideInInspector]
		public KeyCode wKey;
		[HideInInspector]
		public KeyCode aKey;
		[HideInInspector]
		public KeyCode sKey;
		[HideInInspector]
		public KeyCode dKey;
		[HideInInspector]
		public KeyCode sprintKey;

		//COMBAT KEYS
		[HideInInspector]
		public KeyCode toggleCombat;

		//EMOTE KEYS
		public KeyCode Emote1;
		[HideInInspector]
		public KeyCode Emote2;
		[HideInInspector]
		public KeyCode Emote3;
		[HideInInspector]
		public KeyCode Emote4;
		[HideInInspector]
		public KeyCode Emote5;
		[HideInInspector]
		public KeyCode Emote6;
		[HideInInspector]
		public KeyCode Emote7;
		[HideInInspector]
		public KeyCode Emote8;

		public Animator anim;
		public CharacterMovement characterMovement;
		public GameObject mainCamera;
		public GameObject focusPoint;

		void Awake () {
			GetDefaultKeyBindings();
			characterMovement = GetComponent<CharacterMovement> ();
			if (!focusPoint) {
				if (transform.Find ("FocusPoint")) {
					focusPoint = transform.Find ("FocusPoint").gameObject;
					mainCamera = focusPoint.transform.GetChild (0).gameObject;
					focusPoint.GetComponent<FollowScript> ().target = gameObject;
				}
			}
		}
		void Start() {
			if (focusPoint) {
				focusPoint.transform.parent = null;
			}
		}
		void Update () {
			RegisterMovementInput();
			RegisterAttackInput ();
			RegisterEmoteInput ();
		}
		public void RegisterAttackInput() {
			if (Input.GetKeyDown (toggleCombat)) {
				characterMovement.SetInCombat (!characterMovement.inCombat());
			}
		}
		public void RegisterMovementInput() {
			if(Input.GetKey(wKey)) {
				if(characterMovement) {
					Vector3 newPos = transform.position + 1 * focusPoint.transform.forward;
					Quaternion neededRotation = Quaternion.LookRotation(newPos - transform.position);
					Quaternion slerpRotation = Quaternion.Slerp(transform.rotation, neededRotation, 1);
					float newRotY =  slerpRotation.eulerAngles.y;
					Quaternion lookRotation = Quaternion.Euler(transform.rotation.x, newRotY, transform.rotation.z);
					if(Input.GetKey(sprintKey)) {
						characterMovement.MoveForward (1.5f);
						characterMovement.ChangeDirection (lookRotation);
						characterMovement.SetAnimatorParameter ("Movement", 1.0f);
					}
					else {
						characterMovement.MoveForward (1.0f);
						characterMovement.ChangeDirection (lookRotation);
						characterMovement.SetAnimatorParameter ("Movement", 0.5f);
					}
				}
			}
			else if(Input.GetKey(aKey)) {
				Vector3 newPos = transform.position + 1 * -focusPoint.transform.right;
				Quaternion neededRotation = Quaternion.LookRotation(newPos - transform.position);
				Quaternion slerpRotation = Quaternion.Slerp(transform.rotation, neededRotation, 1);
				float newRotY =  slerpRotation.eulerAngles.y;
				Quaternion lookRotation = Quaternion.Euler(transform.rotation.x, newRotY, transform.rotation.z);

				if(Input.GetKey(sprintKey)) {
					characterMovement.MoveForward (1.5f);
					characterMovement.ChangeDirection (lookRotation);
					characterMovement.SetAnimatorParameter ("Movement", 1.0f);
				}
				else if(characterMovement) {
					characterMovement.MoveForward (1.0f);
					characterMovement.ChangeDirection (lookRotation);
					characterMovement.SetAnimatorParameter ("Movement", 0.5f);
				}
			}
			else if(Input.GetKey(sKey)) {
				Vector3 newPos = transform.position + 1 * -focusPoint.transform.forward;
				Quaternion neededRotation = Quaternion.LookRotation(newPos - transform.position);
				Quaternion slerpRotation = Quaternion.Slerp(transform.rotation, neededRotation, 1);
				float newRotY =  slerpRotation.eulerAngles.y;
				Quaternion lookRotation = Quaternion.Euler(transform.rotation.x, newRotY, transform.rotation.z);

				if(Input.GetKey(sprintKey)) {
					characterMovement.MoveForward (1.5f);
					characterMovement.ChangeDirection (lookRotation);
					characterMovement.SetAnimatorParameter ("Movement", 1.0f);
				}
				else if(characterMovement) {
					characterMovement.MoveForward (1.0f);
					characterMovement.ChangeDirection (lookRotation);
					characterMovement.SetAnimatorParameter ("Movement", 0.5f);
				}
			}
			else if(Input.GetKey(dKey)) {
				Vector3 newPos = transform.position + 1 * focusPoint.transform.right;
				Quaternion neededRotation = Quaternion.LookRotation(newPos - transform.position);
				Quaternion slerpRotation = Quaternion.Slerp(transform.rotation, neededRotation, 1);
				float newRotY =  slerpRotation.eulerAngles.y;
				Quaternion lookRotation = Quaternion.Euler(transform.rotation.x, newRotY, transform.rotation.z);
				if(Input.GetKey(sprintKey)) {
					characterMovement.MoveForward (1.5f);
					characterMovement.ChangeDirection (lookRotation);
					characterMovement.SetAnimatorParameter ("Movement", 1.0f);
				}
				else if(characterMovement) {
					characterMovement.MoveForward (1.0f);
					characterMovement.ChangeDirection (lookRotation);
					characterMovement.SetAnimatorParameter ("Movement", 0.5f);
				}
			}
			else if(Input.GetKeyDown(wKey) || Input.GetKeyDown(aKey) || Input.GetKeyDown(sKey) || Input.GetKeyDown(dKey)) {
				if(characterMovement) {
					if (characterMovement.animator.GetBool ("actionInProgress") == true) {
						characterMovement.animator.SetBool ("actionInProgress", false);
					}
				}
			}
			else if(Input.GetKeyUp(wKey) || Input.GetKeyUp(aKey) || Input.GetKeyUp(sKey) || Input.GetKeyUp(dKey)) {
				if(characterMovement) {
					characterMovement.Idle();
					characterMovement.SetAnimatorParameter ("Movement", 0.0f);
				}
			}
		}
		public void RegisterEmoteInput() {
			if (Input.GetKeyDown (Emote1)) {
				anim.Play ("Bored");
			}
			else if (Input.GetKeyDown (Emote2)) {
				anim.Play ("Bye");
			}
			else if (Input.GetKeyDown (Emote3)) {
				anim.Play ("Cry");
			}
			else if (Input.GetKeyDown (Emote4)) {
				anim.Play ("Cheer");
			}
			else if (Input.GetKeyDown (Emote5)) {
				anim.Play ("Point");
			}
			else if (Input.GetKeyDown (Emote6)) {
				anim.Play ("Greet");
			}
			else if (Input.GetKeyDown (Emote7)) {
				anim.Play ("Shrug");
			}
			else if (Input.GetKeyDown (Emote8)) {
				anim.Play ("Sigh");
			}
		}
		public void GetDefaultKeyBindings() {
			wKey = KeyCode.W;
			aKey = KeyCode.A;
			sKey = KeyCode.S;
			dKey = KeyCode.D;
			sprintKey = KeyCode.LeftShift;

			Emote1 = KeyCode.Alpha1;
			Emote2 = KeyCode.Alpha2;
			Emote3 = KeyCode.Alpha3;
			Emote4 = KeyCode.Alpha4;
			Emote5 = KeyCode.Alpha5;
			Emote6 = KeyCode.Alpha6;
			Emote7 = KeyCode.Alpha7;
			Emote8 = KeyCode.Alpha8;

			toggleCombat = KeyCode.Tab;
		}
	}
}
