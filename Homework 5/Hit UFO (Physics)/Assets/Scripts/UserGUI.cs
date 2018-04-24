using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
    private UserAction action;
    // 界面样式
    GUIStyle title;
    GUIStyle scoreStyle;
    GUIStyle buttonStyle;

    private void Start() {
        action = Director.getInstance().currentSceneController as UserAction;
        title = new GUIStyle();
        title.fontSize = 30;
        title.normal.textColor = Color.white;
        scoreStyle = new GUIStyle();
        scoreStyle.fontSize = 20;
        scoreStyle.normal.textColor = Color.white;
        buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize = 15;
        buttonStyle.normal.textColor = Color.white;
        
    }
    private void OnGUI() {
        // 点击鼠标触发Hit函数
        if (Input.GetButtonDown("Fire1") && Director.getInstance().currentSceneController.getGameState() != GameState.PAUSE) {
            Vector3 mp = Input.mousePosition;
            action.Hit(mp);
        }
        // 设置按钮的内容
        string buttonText = "";
        if (Director.getInstance().currentSceneController.getGameState() == GameState.START ||
            Director.getInstance().currentSceneController.getGameState() == GameState.ROUND_START)
            buttonText = "Pause";
        if (Director.getInstance().currentSceneController.getGameState() == GameState.FINISH)
            buttonText = "Restart";
        if (Director.getInstance().currentSceneController.getGameState() == GameState.PAUSE)
            buttonText = "Start";
        if (Director.getInstance().currentSceneController.getGameState() == GameState.ROUND_FINISH)
            buttonText = "Next Round";
        
        GUI.Label(new Rect(Screen.width / 2 - 30, Screen.width / 2 - 550, 100, 100), "Hit UFO", title);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.width / 2 - 480, 100, 100), 
            "Round " + Director.getInstance().currentSceneController.GetRound().ToString(), scoreStyle);
        GUI.Label(new Rect(Screen.width / 2 + 60, Screen.width / 2 - 480, 50, 50), 
            "Score: " + Director.getInstance().currentSceneController.GetScore().ToString(), scoreStyle);
        // 按下按钮的操作
        if (GUI.Button(new Rect(Screen.width / 2 - 30, Screen.width / 2 - 410, 100, 50), buttonText, buttonStyle)) {
            if (buttonText == "Pause") {
                Director.getInstance().currentSceneController.Pause();
            } else if (buttonText == "Start") {
                Director.getInstance().currentSceneController.Begin();
            } else if (buttonText == "Next Round") {
                Director.getInstance().currentSceneController.NextRound();
            } else if (buttonText == "Restart") {
                Director.getInstance().currentSceneController.Restart();
            }
        }
        if (Director.getInstance().currentSceneController.getGameState() == GameState.ROUND_START) {

        }
    }
}
