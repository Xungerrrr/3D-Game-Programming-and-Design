using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAction : Action {
    private float acceleration = -9.8f;                 // 飞碟的加速度
    private float time;                                 // 过去的时间
    private Vector3 startDirection;                     // 初速度方向
    private Vector3 currentAngle = Vector3.zero;        // 当前的角度
    private Vector3 accelerateDirection = Vector3.zero; // 加速度的方向


    public static FlyAction GetAction(Vector3 direction, float angle, float power) {
        // 初始化飞碟的初速度方向
        FlyAction action = CreateInstance<FlyAction>();
        if (direction.x == -1) {
            action.startDirection = Quaternion.Euler(new Vector3(0, 0, -angle)) * Vector3.left * power;
        }
        else {
            action.startDirection = Quaternion.Euler(new Vector3(0, 0, angle)) * Vector3.right * power;
        }
        return action;
    }

    public override void Start() { }

    public override void Update() {
        if (Director.getInstance().currentSceneController.getGameState() == GameState.PAUSE) { } // 暂停游戏
        else {
            // 计算物体的向下的速度, v = at
            time += Time.fixedDeltaTime;
            accelerateDirection.y = acceleration * time;

            // 位移模拟
            transform.position += (startDirection + accelerateDirection) * Time.fixedDeltaTime;
            currentAngle.z = Mathf.Atan((startDirection.y + accelerateDirection.y) / startDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = currentAngle;

            // y 坐标小于 -20，销毁动作
            if (this.transform.position.y < -20) {
                this.destroy = true;
                this.enable = false;
                this.callback.ActionEvent(this);
            }
        }
    }
    public override void FixedUpdate() { }
}
