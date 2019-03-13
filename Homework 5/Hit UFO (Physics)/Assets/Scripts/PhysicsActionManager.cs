using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsActionManager : ActionManager, ActionCallback {

    public PhysicsAction physics; // 物理动作

    // 管理飞行
    public void Fly(GameObject disk, float angle, float power) {
        physics = PhysicsAction.GetAction(disk.GetComponent<DiskData>().direction, angle, power);
        this.RunAction(disk, physics, this);
    }
    public void ActionEvent(Action source, ActionEventType events = ActionEventType.Completed,
                            int intParam = 0, string strParam = null, object objectParam = null) { }
}
