using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 导演类采用单例模式
public class Director : System.Object {

    private static Director _instance; // 导演的实例
    public SceneController currentSceneController { get; set; } // 当前的场记

    private Director() { }

    // 获取导演实例
    public static Director getInstance() {
        if (_instance == null) {
            _instance = new Director();
        }
        return _instance;
    }

    public int getFPS() {
        return Application.targetFrameRate;
    }

    public void setFPS(int fps) {
        Application.targetFrameRate = fps;
    }

}
