using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToe : MonoBehaviour {

	private int[,] pad = new int[3, 3]; // 棋盘数据
	private int turn = 1; // 游戏回合

	// 重置
	private void reset () {
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				pad [i, j] = 0;
			}
		}
	}

	// 检查游戏是否结束
	private int check() {
		// 检查纵向
		for (int i = 0; i < 3; i++) {
			if (pad [i, 0] == pad [i, 1] && pad [i, 1] == pad [i, 2] && pad [i, 0] != 0)
				return pad [i, 0];
		}

		// 检查横向
		for (int i = 0; i < 3; i++) {
			if (pad [0, i] == pad [1, i] && pad [1, i] == pad [2, i] && pad [0, i] != 0)
				return pad [0, i];
		}
		
		// 检查对角线
		if (pad [1, 1] != 0 && (pad [0, 0] == pad [1, 1] && pad [1, 1] == pad [2, 2]) || (pad [0, 2] == pad [1, 1] && pad [1, 1] == pad [2, 0]))
				return pad [1, 1];

		// 判断游戏是否为平局
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (pad [i, j] == 0)
					return 0;
			}
		}
		return 2;
	}

	// Use this for initialization
	void Start () {
		reset ();
	}

	void OnGUI() {
		if (GUI.Button(new Rect(400, 300, 150, 50), "Reset"))
			reset ();
		int result = check ();
		if (result == 1) {
			GUI.Label (new Rect (450, 350, 100, 50), "O wins!");
			GUI.Label (new Rect (410, 365, 150, 50), "Please reset the game.");
		} else if (result == -1) {
			GUI.Label (new Rect (450, 350, 100, 50), "X wins!");
			GUI.Label (new Rect (410, 365, 150, 50), "Please reset the game.");
		} else if (result == 2) {  
			GUI.Label (new Rect (450, 350, 100, 50), "Game Over!");
			GUI.Label (new Rect (410, 365, 150, 50), "Please reset the game.");
		}
		// 绘制棋盘
		for (int i = 0; i < 3; i++) {  
			for (int j = 0; j < 3; j++) {  
				if (pad [i, j] == 1)  
					GUI.Button(new Rect(400 + i * 50, 100 + j * 50,50,50), "O");
				if (pad [i, j] == -1)
					GUI.Button(new Rect(400 + i * 50, 100 + j * 50,50,50), "X");
				if (GUI.Button(new Rect(400 + i * 50, 100 + j * 50,50,50), "")) {
					if (result == 0) {
						pad [i, j] = turn;
						turn = -turn;
					}
				}
			}
		}
	}
}