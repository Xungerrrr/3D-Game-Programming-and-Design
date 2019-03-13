using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LylekGames.RPGCharacter {
	public class FollowScript : MonoBehaviour {
		public bool findPlayerOnStart = true;
		public bool unparentOnStart = true;
		[Range(0, 20)]
		public float lerpSpeed = 10;
		public bool followRotation = true;
		public bool followPosition = true;
		public GameObject target;
		[Range(0, 3)]
		public float yOffset = 0.0f;

		void Start() {
			if(findPlayerOnStart) {
				if(GameObject.FindGameObjectWithTag("Player")) {
					target = GameObject.FindGameObjectWithTag("Player");
				}
			}
			if(target.GetComponent<CharacterController>()) {
				float conrollerHeight = target.GetComponent<CharacterController>().height;
				yOffset += conrollerHeight * 0.8f;
			}
			if(unparentOnStart) {
				transform.parent = null;
			}
		}
		void Update() {
			if(followPosition) {
				Vector3 moveToPos = new Vector3 ();
				moveToPos.x = target.transform.position.x;
				moveToPos.y = target.transform.position.y + yOffset;
				moveToPos.z = target.transform.position.z;
				transform.position = Vector3.MoveTowards(transform.position, moveToPos, lerpSpeed * Time.deltaTime);
			}
			if(followRotation) {
				transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, lerpSpeed);
			}
		}
	}
}
