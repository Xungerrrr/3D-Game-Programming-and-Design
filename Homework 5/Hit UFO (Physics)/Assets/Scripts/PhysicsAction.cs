using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsAction : Action {

    private Vector3 startDirection;                     // 初速度方向
    private float power;                                // 控制飞碟速度的变量

    public static PhysicsAction GetAction(Vector3 direction, float angle, float power) {
        // 初始化飞碟的初速度方向
        PhysicsAction action = CreateInstance<PhysicsAction>();
        if (direction.x == -1) {
            action.startDirection = Quaternion.Euler(new Vector3(0, 0, -angle)) * Vector3.left * power;
        }
        else {
            action.startDirection = Quaternion.Euler(new Vector3(0, 0, angle)) * Vector3.right * power;
        }
        action.power = power;
        return action;
    }

    // 设定初速度并添加重力
    public override void Start() {
        gameObject.GetComponent<Rigidbody>().velocity = startDirection * power / 10;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    public override void FixedUpdate() {
        if (this.transform.position.y < -20) {
            this.destroy = true;
            this.enable = false;
            this.callback.ActionEvent(this);
        }
    }
    public override void Update() { }
}
