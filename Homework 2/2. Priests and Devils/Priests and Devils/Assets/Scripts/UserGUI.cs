using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
	private IUserAction action;
	public int status = 0;
	GUIStyle style;
	GUIStyle buttonStyle;
	GUIStyle countDownStyle;

	void Start() {
		action = SSDirector.getInstance ().currentSceneController as IUserAction;

		style = new GUIStyle();
		style.fontSize = 40;
		style.alignment = TextAnchor.MiddleCenter;

		buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 30;

		countDownStyle = new GUIStyle();
		countDownStyle.fontSize = 20;
	}

	void OnGUI() {
        GUI.Label(new Rect(Screen.width/2-10, Screen.height/2-120, 50, 50), 
        	SSDirector.getInstance().leaveSeconds.ToString(), countDownStyle);
        if (SSDirector.getInstance().state != State.WIN && SSDirector.getInstance().state != State.LOSE
            && GUI.Button(new Rect(Screen.width/2-60, Screen.height/2+100, 140, 70), SSDirector.getInstance().countDownTitle, buttonStyle)) {
        	if (SSDirector.getInstance().countDownTitle == "Start") {
                SSDirector.getInstance().currentSceneController.resume();
                SSDirector.getInstance().countDownTitle = "Pause";
                StartCoroutine(SSDirector.getInstance().CountDown());
            } else {
                SSDirector.getInstance().currentSceneController.pause();
                SSDirector.getInstance().countDownTitle = "Start";
                StopAllCoroutines();
            }
        }
		if (status == 1) {
			StopAllCoroutines();
			SSDirector.getInstance().state = State.LOSE;
			SSDirector.getInstance().totalSeconds = 100;
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-85, 100, 50), "Game Over!", style);
			if (GUI.Button(new Rect(Screen.width/2-60, Screen.height/2+100, 140, 70), "Restart", buttonStyle)) {
				SSDirector.getInstance().currentSceneController.resume();
                SSDirector.getInstance().countDownTitle = "Pause";
                StartCoroutine(SSDirector.getInstance().CountDown());
				status = 0;
				action.restart ();
			}
		} else if(status == 2) {
			StopAllCoroutines();
			SSDirector.getInstance().state = State.WIN;
			SSDirector.getInstance().totalSeconds = 100;
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-85, 100, 50), "You win!", style);
			if (GUI.Button(new Rect(Screen.width/2-60, Screen.height/2+100, 140, 70), "Restart", buttonStyle)) {
				SSDirector.getInstance().currentSceneController.resume();
                SSDirector.getInstance().countDownTitle = "Pause";
                StartCoroutine(SSDirector.getInstance().CountDown());
				status = 0;
				action.restart ();
			}
		}
	}
}
