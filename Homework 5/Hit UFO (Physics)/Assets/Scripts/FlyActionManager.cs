using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyActionManager : ActionManager, ActionCallback {

    public FlyAction fly; // 飞行动作

    // 管理飞行
    public void Fly (GameObject disk, float angle, float power) {
        fly = FlyAction.GetAction(disk.GetComponent<DiskData>().direction, angle, power);
        this.RunAction(disk, fly, this);
    }

    public void ActionEvent(Action source, ActionEventType events = ActionEventType.Completed, 
                            int intParam = 0, string strParam = null, object objectParam = null) { }
}
