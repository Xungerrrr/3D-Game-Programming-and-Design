using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionEventType : int { Started, Completed } // 事件类型
public enum GameState { RUNNING, PAUSE, START, LOSE, WIN } // 游戏状态

public interface SceneController
{
    void LoadResources();            // 加载资源
    int GetScore();                  // 获取分数
    GameState getGameState();        // 获取游戏状态
    void setGameState(GameState gs); // 设置游戏状态
    void Restart();                  // 重新开始游戏
    void Pause();                    // 暂停游戏
    void Begin();                    // 开始游戏
}

public interface UserAction
{
    // 控制玩家移动
    void MovePlayer(float translationX, float translationZ);
}

public interface ActionCallback
{
    void ActionEvent(Action source, ActionEventType events = ActionEventType.Completed,
        int intParam = 0, string strParam = null, object objectParam = null);
}
