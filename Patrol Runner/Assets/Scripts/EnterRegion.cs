using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRegion : MonoBehaviour
{
    public int region;                         // 当前区域的区域编号
    FirstSceneController sceneController;      // 当前的场记

    void OnTriggerEnter(Collider collider) {
        sceneController = Director.GetInstance().CurrentSceneController as FirstSceneController;
        if (collider.gameObject.tag == "Player") {
            // 如果玩家进入区域，则标记玩家当前区域为该区域
            sceneController.playerRegion = region;
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Patrol") {
            // 如果巡逻兵尝试离开区域，则标记巡逻兵发生了碰撞，以控制转向
            collider.gameObject.GetComponent<PatrolData>().isCollided = true;
        }
    }
}
