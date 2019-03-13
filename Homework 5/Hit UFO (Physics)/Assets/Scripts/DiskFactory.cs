using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour {

    public GameObject diskPrefab = null;                // 飞碟预制，是工厂返回的对象
    private List<DiskData> used = new List<DiskData>(); // 正在使用的飞碟
    private List<DiskData> free = new List<DiskData>(); // 未在使用的飞碟

    // 获取飞碟
    public GameObject GetDisk(int round) {

        int choice = 0; // 决定飞碟种类的变量
        diskPrefab = null;

        // 若存在正在使用的飞碟，则从 used 列表中获取；若不存在，则新建飞碟
        if (free.Count > 0) {
            diskPrefab = free[0].gameObject;
            free.Remove(free[0]);
        } else {
            diskPrefab = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Disk"), Vector3.zero, Quaternion.identity);
            diskPrefab.SetActive(false);
        }

        // 根据回合随机选出飞碟种类
        switch (round) {
            case 1:
                choice = Random.Range(0, 2);
                break;
            case 2:
                choice = Random.Range(0, 4);
                break;
            case 3:
                choice = Random.Range(0, 8);
                break;
        }
        // 设置不同种类飞碟的属性
        if (choice < 2) {
            diskPrefab.GetComponent<DiskData>().color = Color.yellow;
            diskPrefab.GetComponent<DiskData>().speed = 7.0f + round;
            diskPrefab.GetComponent<DiskData>().scale = new Vector3(4, 0.2f, 4);
        } else if (choice > 2 && choice < 4) {
            diskPrefab.GetComponent<DiskData>().color = Color.red;
            diskPrefab.GetComponent<DiskData>().speed = 9.0f + round;
            diskPrefab.GetComponent<DiskData>().scale = new Vector3(2, 0.1f, 2);
        } else if (choice > 4) {
            diskPrefab.GetComponent<DiskData>().color = Color.white;
            diskPrefab.GetComponent<DiskData>().speed = 11.0f + round;
            diskPrefab.GetComponent<DiskData>().scale = new Vector3(1, 0.05f, 1);
        }
        float RanX = Random.Range(-1f, 1f) < 0 ? -1 : 1;
        diskPrefab.GetComponent<DiskData>().direction = new Vector3(RanX, 1, 0);
        diskPrefab.transform.localScale = diskPrefab.GetComponent<DiskData>().scale;
        diskPrefab.GetComponent<Renderer>().material.color = diskPrefab.GetComponent<DiskData>().color;

        // 将飞碟添加到 used 列表并返回
        used.Add(diskPrefab.GetComponent<DiskData>());
        diskPrefab.name = diskPrefab.GetInstanceID().ToString();
        return diskPrefab;
    }

    // 回收飞碟
    public void FreeDisk(GameObject disk) {
        foreach (DiskData data in used) {
            if (data.gameObject.GetInstanceID() == disk.GetInstanceID()) {
                data.gameObject.SetActive(false);
                free.Add(data);
                used.Remove(data);
                break;
            }
        }
    }

}
