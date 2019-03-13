using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private Dictionary<int, Action> actions = new Dictionary<int, Action>();
    private List<Action> waitingAdd = new List<Action>();
    private List<int> waitingDelete = new List<int>();

    protected void Update() {
        foreach (Action action in waitingAdd) {
            actions[action.GetInstanceID()] = action;
        }
        waitingAdd.Clear();
        foreach (KeyValuePair<int, Action> kv in actions) {
            Action action = kv.Value;
            if (action.enable) {
                action.Update(); // update action
            } else if (action.destroy) {
                waitingDelete.Add(action.GetInstanceID()); // release action
            }
        }
        foreach (int key in waitingDelete) {
            Action action = actions[key];
            actions.Remove(key);
            Destroy(action);
        }
        waitingDelete.Clear();
    }

    // 执行动作
    public void RunAction(GameObject gameObject, Action action, ActionCallback callback) {
        action.gameObject = gameObject;
        action.transform = gameObject.transform;
        action.callback = callback;
        waitingAdd.Add(action);
        action.Start();
    }

    public void DestroyAll() {
        foreach (KeyValuePair<int, Action> kv in actions) {
            Action ac = kv.Value;
            ac.destroy = true;
        }
    }

    protected void Start() { }
}
