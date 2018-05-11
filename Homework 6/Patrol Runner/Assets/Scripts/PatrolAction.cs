using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : Action
{
    private float pos_x, pos_z;                 // 移动前的初始x和z方向坐标
    private bool turn = true;                   // 是否选择新方向
    private PatrolData data;                    // 巡逻兵的数据

    public static PatrolAction GetAction(Vector3 location) {
        PatrolAction action = CreateInstance<PatrolAction>();
        action.pos_x = location.x;
        action.pos_z = location.z;
        return action;
    }

    public override void Start() {
        data = this.gameObject.GetComponent<PatrolData>();
    }

    public override void Update() {
        if (Director.GetInstance().CurrentSceneController.getGameState().Equals(GameState.RUNNING)) {
            // 巡逻兵巡逻
            Patrol();
            // 如果满足要求，而巡逻兵未开始追击，则停止巡逻，开始追击
            if (!data.isFollowing && data.isPlayerInRange && data.patrolRegion == data.playerRegion && !data.isCollided) {
                this.destroy = true;
                this.enable = false;
                this.callback.ActionEvent(this);
                this.gameObject.GetComponent<PatrolData>().isFollowing = true;
                Singleton<GameEventManager>.Instance.FollowPlayer(this.gameObject);
            }
        }
    }

    void Patrol() {
        if (turn) {
            pos_x = this.transform.position.x + Random.Range(-5f, 5f);
            pos_z = this.transform.position.z + Random.Range(-5f, 5f);
            this.transform.LookAt(new Vector3(pos_x, 0, pos_z));
            this.gameObject.GetComponent<PatrolData>().isCollided = false;
            turn = false;
        }
        float distance = Vector3.Distance(transform.position, new Vector3(pos_x, 0, pos_z));

        if (this.gameObject.GetComponent<PatrolData>().isCollided) {
            // 碰撞，则向后转，寻找新位置
            this.transform.Rotate(Vector3.up, 180);
            GameObject temp = new GameObject();
            temp.transform.position = this.transform.position;
            temp.transform.rotation = this.transform.rotation;
            temp.transform.Translate(0, 0, Random.Range(0.5f, 3f));
            pos_x = temp.transform.position.x;
            pos_z = temp.transform.position.z;
            this.transform.LookAt(new Vector3(pos_x, 0, pos_z));
            this.gameObject.GetComponent<PatrolData>().isCollided = false;
            Destroy(temp);
        } else if (distance <= 0.1) {
            turn = true;
        } else {
            // 向前移动巡逻兵
            this.transform.Translate(0, 0, Time.deltaTime);
        }
    }
}
