using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolData : MonoBehaviour
{
    public bool isPlayerInRange;    // 玩家是否在侦测范围里
    public bool isFollowing;        // 是否正在追击
    public bool isCollided;         // 是否发生碰撞
    public int patrolRegion;        // 巡逻兵所在区域
    public int playerRegion;        // 玩家所在区域
    public GameObject player;       // 所追击的玩家
}
