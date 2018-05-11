using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 导演类采用单例模式
public class Director : System.Object
{
    private static Director _instance;                          // 导演的实例
    public SceneController CurrentSceneController { get; set; } // 当前的场记
    public int leaveSeconds = 60;                               // 当前剩余时间

    private Director() {}

    // 获取导演实例
    public static Director GetInstance() {
        if (_instance == null) {
            _instance = new Director();
        }
        return _instance;
    }

    public int GetFPS() {
        return Application.targetFrameRate;
    }

    public void SetFPS(int fps) {
        Application.targetFrameRate = fps;
    }

    // 游戏倒计时
    public IEnumerator CountDown() {
        while (leaveSeconds > 0) {
            yield return new WaitForSeconds(1f);
            leaveSeconds--;
            if (leaveSeconds == 0) {
                Singleton<GameEventManager>.Instance.TimeIsUP();
            }
        }
    }
}
