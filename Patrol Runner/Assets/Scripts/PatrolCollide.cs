using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCollide : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            // 当玩家与巡逻兵相撞
            this.GetComponent<Animator>().SetTrigger("shoot");
            Singleton<GameEventManager>.Instance.OnPlayerCatched();
        } else {
            // 当巡逻兵碰到其他障碍物
            this.GetComponent<PatrolData>().isCollided = true;
        }
    }
}
