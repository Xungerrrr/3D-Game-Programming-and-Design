using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour {

    public int score; // 分数

    private Dictionary<Color, int> scoreTable = new Dictionary<Color, int>(); // 用字典来记录不同飞碟对应的分数

    void Start() {
        score = 0;
        scoreTable.Add(Color.yellow, 1);
        scoreTable.Add(Color.red, 2);
        scoreTable.Add(Color.white, 4);
    }

    // 记录分数的增加
    public void Record(GameObject disk) {
        score += scoreTable[disk.GetComponent<DiskData>().color];
    }

    public void Reset() {
        score = 0;
    }
}
