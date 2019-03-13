using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, SceneController, IUserAction {

	public ShoreController fromShore;
	public ShoreController toShore;
	public BoatController boat;
	private MyCharacterController[] characters;
    private FirstActionManager actionManager;
	UserGUI userGUI;

	void Awake() {
		SSDirector director = SSDirector.getInstance();
		director.currentSceneController = this;
		userGUI = gameObject.AddComponent <UserGUI>() as UserGUI;
		characters = new MyCharacterController[6];
		director.setFPS(60);
        director.leaveSeconds = director.totalSeconds;
		director.currentSceneController.loadResources();
	}
    void Start()
    {
        actionManager = GetComponent<FirstActionManager>();
    }
	public void loadResources() {
		GameObject water = Instantiate (Resources.Load ("Prefabs/WaterProDaytime", 
			typeof(GameObject)), new Vector3(0,1,0), Quaternion.identity, null) as GameObject;
		water.name = "water";

		fromShore = new ShoreController ("from");
		toShore = new ShoreController ("to");
		boat = new BoatController ();

		for (int i = 0; i < 3; i++) {
			MyCharacterController cha = new MyCharacterController ("priest");
			cha.setName("priest" + i);
			cha.setPosition (fromShore.getEmptyPosition ());
			cha.getOnShore (fromShore);
			fromShore.getOnShore (cha);

			characters [i] = cha;
		}

		for (int i = 0; i < 3; i++) {
			MyCharacterController cha = new MyCharacterController ("devil");
			cha.setName("devil" + i);
			cha.setPosition (fromShore.getEmptyPosition ());
			cha.getOnShore (fromShore);
			fromShore.getOnShore (cha);

			characters [i+3] = cha;
		}
	}

	public void moveBoat() {
		if (boat.isEmpty ())
			return;
        actionManager.MoveBoat(boat); // 移动船
        boat.ChangePosition(); // 修改船的位置属性
        userGUI.status = check_game_over ();
	}

	public void characterIsClicked(MyCharacterController characterCtrl) {
		if (characterCtrl.isOnBoat ()) {
			ShoreController whichShore;
			if (boat.get_to_or_from () == -1) { // to->-1; from->1
				whichShore = toShore;
			} else {
				whichShore = fromShore;
			}

			boat.GetOffBoat (characterCtrl.getName());
            // 移动角色
            actionManager.MoveCharacter(characterCtrl, whichShore.getEmptyPosition());
			characterCtrl.getOnShore (whichShore);
			whichShore.getOnShore (characterCtrl);

		} else {									// character on shore
			ShoreController whichShore = characterCtrl.getShoreController ();

			if (boat.getEmptyIndex () == -1) {		// boat is full
				return;
			}

			if (whichShore.get_to_or_from () != boat.get_to_or_from ())	// boat is not on the side of character
				return;

			whichShore.getOffShore(characterCtrl.getName());
            // 移动角色
            actionManager.MoveCharacter(characterCtrl, boat.getEmptyPosition());
			characterCtrl.getOnBoat (boat);
			boat.GetOnBoat (characterCtrl);
		}
		userGUI.status = check_game_over ();
	}

	int check_game_over() {	// 0->not finish, 1->lose, 2->win
		int from_priest = 0;
		int from_devil = 0;
		int to_priest = 0;
		int to_devil = 0;

		int[] fromCount = fromShore.getCharacterNum ();
		from_priest += fromCount[0];
		from_devil += fromCount[1];

		int[] toCount = toShore.getCharacterNum ();
		to_priest += toCount[0];
		to_devil += toCount[1];

		if (to_priest + to_devil == 6)		// win
			return 2;

		int[] boatCount = boat.getCharacterNum ();
		if (boat.get_to_or_from () == -1) {	// boat at toShore
			to_priest += boatCount[0];
			to_devil += boatCount[1];
		} else {	// boat at fromShore
			from_priest += boatCount[0];
			from_devil += boatCount[1];
		}
		if (from_priest < from_devil && from_priest > 0) {		// lose
			return 1;
		}
		if (to_priest < to_devil && to_priest > 0) {
			return 1;
		}
		return 0;			// not finish
	}

	public void restart() {
		SSDirector.getInstance().state = State.START;
		SSDirector.getInstance().leaveSeconds = 100;
		SSDirector.getInstance().countDownTitle = "Pause";
		boat.reset ();
		fromShore.reset ();
		toShore.reset ();
		for (int i = 0; i < characters.Length; i++) {
			characters [i].reset ();
		}
	}
	public void pause() {
		SSDirector.getInstance().state = State.PAUSE;
	}
	public void resume() {
		SSDirector.getInstance().state = State.START;
	}
	void Update() {
		if (SSDirector.getInstance().leaveSeconds == 0) {
			userGUI.status = 1;
		}
	}
}
