using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickGUI : MonoBehaviour {
	IUserAction action;
	MyCharacterController characterController;

	public void setController(MyCharacterController characterCtrl) {
		characterController = characterCtrl;
	}

	void Start() {
		action = SSDirector.getInstance ().currentSceneController as IUserAction;
	}

	void OnMouseDown() {
		if (SSDirector.getInstance().state == State.START) {
			if (gameObject.name == "boat") {
				action.moveBoat ();
			} else {
				action.characterIsClicked (characterController);
			}
		}
		
	}
}
