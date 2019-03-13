using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    public int score; // 分数

    void Start() {
        score = 0;
    }

    // 记录分数的增加
    public void Record() {
        score++;
    }

    public void Reset() {
        score = 0;
    }
}
