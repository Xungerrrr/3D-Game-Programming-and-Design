using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManagerAdapter : MonoBehaviour, IActionManager {

    // 要适配的类
    public FlyActionManager flyActionManager;
    public PhysicsActionManager physicsActionManager;

    public void Start() {
        flyActionManager = (FlyActionManager)gameObject.AddComponent<FlyActionManager>();
        physicsActionManager = (PhysicsActionManager)gameObject.AddComponent<PhysicsActionManager>();
    }

    // 实现通用接口
    public void Fly(GameObject disk, float angle, float power, bool physics) {
        if (physics) {
            physicsActionManager.Fly(disk, angle, power);
        }
        else {
            flyActionManager.Fly(disk, angle, power);
        }
    }
}
