using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FOR OUR DEMO, THIS SCRIPT WILL HOLD OUR CHARACTER PROPERITES, SUCH AS ARMORS, BODY PARTS, HAIR, ETC
namespace LylekGames.RPGCharacter {
	public class CharacterProperties : MonoBehaviour {

		public static CharacterProperties characterProperties;
		public Transform character; //animated gameObject
		public GameObject focusPoint;

		public CapsuleCollider[] boneColliders; //an array of all colliders attached to our character's bones | these colliders may be a prefered in collision detection (head, leg, arm, etc)
		//the legs colliders are used for robes and other clothings that use the Cloth component

		public CharacterType characterType;
		public enum CharacterType { //an enumeration of our possible character types
			Human,
			Dwarf,
			Elf,
		}
		public GenderType gender;
		public enum GenderType { //an enumeration of our possible character types
			Male,
			Female,
		}
		public string resourcesPath; //directory determined by our character type (used to orginize our characters' armors/hair/etc)
		public Transform head; //character body parts
		public Transform eyes;
		public Transform torso;
		public Transform legs;
		public Transform hands;
		public Transform feet;

		public Transform headArmor; //player's equipped armors
		public Transform torsoArmor;
		public Transform legArmor;
		public Transform handArmor;
		public Transform feetArmor;

		public Transform hair;
		public Transform beard;
		public GameObject skinTone;
		public Material hairColor;
		public Material headMaterial;
		public Material chestMaterial;
		public Material legMaterial;
		public Material handMaterial;
		public Material feetMaterial;
		public Material eyeColor;

		void Awake() {
			characterProperties = GetComponent<CharacterProperties> ();
			//Cursor.visible = false;
			UpdateCharacterPath();
		}
		public void UpdateCharacter() {
			//This function will retrieve our new character skin, including head, torso, eyes, etc.
			Transform newCharacter = Resources.Load<Transform>(resourcesPath + "RPGCharacter_skin") as Transform;
			if(character) {
				Destroy(character.gameObject);
			}
			character = Instantiate(newCharacter, transform.position, transform.rotation);
			character.name = "Player";
			if (character.GetComponent<PlayerInput> ().focusPoint) {
				Destroy (character.GetComponent<PlayerInput> ().focusPoint.gameObject);
			}
			character.GetComponent<PlayerInput> ().focusPoint = focusPoint;
			character.GetComponent<PlayerInput> ().mainCamera = focusPoint.transform.GetChild(0).gameObject;
			focusPoint.transform.parent = null;

			Component[] myChildren;
			myChildren = character.transform.GetComponentsInChildren<Transform>();

			//assign our character components / body parts
			foreach(Transform children in myChildren) {
				switch (children.gameObject.name) {
				case "Character_head":
					head = children;
					break;
				case "Character_eyes":
					eyes = children;
					break;
				case "Character_torso":
					torso = children;
					break;
				case "Character_legs":
					legs = children;
					break;
				case "Character_hands":
					hands = children;
					break;
				case "Character_feet":
					feet = children;
					break;
				default:
					break;
				}
			}
			if(!eyeColor) {
				eyeColor = eyes.GetComponent<Renderer>().sharedMaterial;
			}
			if (!hairColor) {
				//hairColor = hair.GetComponent<Renderer> ().sharedMaterial;
			}
			UpdateCharacterColliders();
		}
		public void UpdateCharacterColliders() {
			//This function will retrieve our character colliders, from our character gameObject
			Component[] myChildren;
			myChildren = character.transform.GetComponentsInChildren<Transform>();

			//assign our character components / body parts
			foreach(Transform child in myChildren) {
				switch (child.gameObject.name) {
				case "Hip_L":
					boneColliders[0] = child.GetComponent<CapsuleCollider>();
					break;
				case "Hip_R":
					boneColliders[1] = child.GetComponent<CapsuleCollider>();
					break;
				case "UpperLeg_L":
					boneColliders[2] = child.GetComponent<CapsuleCollider>();
					break;
				case "UpperLeg_R":
					boneColliders[3] = child.GetComponent<CapsuleCollider>();
					break;
				case "LowerLeg_L":
					boneColliders[4] = child.GetComponent<CapsuleCollider>();
					break;
				case "LowerLeg_R":
					boneColliders[5] = child.GetComponent<CapsuleCollider>();
					break;
				case "Foot_L":
					boneColliders[6] = child.GetComponent<CapsuleCollider>();
					break;
				case "Foot_R":
					boneColliders[7] = child.GetComponent<CapsuleCollider>();
					break;
				default:
					break;
				}
			}
		}
		public void UpdateCharacterPath() {
			switch (gender) {
			case GenderType.Male:
				switch (characterType) {
				case CharacterType.Human:
					resourcesPath = "Human_male/";
					break;
				case CharacterType.Dwarf:
					resourcesPath = "Dwarf_male/";
					break;
				case CharacterType.Elf:
					resourcesPath = "Elf_male/";
					break;
				default:
					resourcesPath = "Human_male/";
					break;
				}
				break;
			case GenderType.Female:
				switch (characterType) {
				case CharacterType.Human:
					resourcesPath = "Human_female/";
					break;
				case CharacterType.Dwarf:
					resourcesPath = "Dwarf_female/";
					break;
				case CharacterType.Elf:
					resourcesPath = "Elf_female/";
					break;
				default:
					resourcesPath = "Human_female/";
					break;
				}
				break;
			default:
				resourcesPath = "Human_male/";
				break;
			}
			UpdateCharacter();
		}
	}
}




