using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSAction : ScriptableObject
{

    public bool enable = true;
    public bool destroy = false;

    public GameObject GameObject { get; set; }
    public Transform Transform { get; set; }
    public ISSActionCallback Callback { get; set; }

    protected SSAction() { }

    // Use this for initialization
    public virtual void Start()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }
}

public class SSMoveToAction : SSAction
{

    public Vector3 target; // 移动目标
    public float speed; // 移动速度

    // 创建并返回动作的实例
    public static SSMoveToAction GetSSMoveToAction(Vector3 target, float speed)
    {
        SSMoveToAction action = ScriptableObject.CreateInstance<SSMoveToAction>();
        action.target = target;
        action.speed = speed;
        return action;
    }

    // Use this for initialization
    public override void Start()
    {

    }

    // 在 Update 函数中用 Vector3.MoveTowards 实现直线运动
    public override void Update()
    {
        this.Transform.position = Vector3.MoveTowards(this.Transform.position, target, speed * Time.deltaTime);
        if (this.Transform.position == target)
        {
            this.destroy = true;
            // 完成动作后进行动作回掉
            this.Callback.SSActionEvent(this);
        }
    }
}

public class SSSequenceAction : SSAction, ISSActionCallback
{

    public List<SSAction> sequence; // 动作队列
    public int repeat = -1; // 循环次数，-1表示无限循环
    public int start = 0; // 当前执行的动作

    // 创建并返回动作序列的实例
    public static SSSequenceAction GetSSSequenceAction(int repeat, int start, List<SSAction> sequence)
    {
        SSSequenceAction action = ScriptableObject.CreateInstance<SSSequenceAction>();
        action.repeat = repeat;
        action.sequence = sequence;
        action.start = start;
        return action;
    }

    // Use this for initialization
    public override void Start()
    {
        foreach (SSAction action in sequence)
        {
            action.GameObject = this.GameObject;
            action.Transform = this.Transform;
            action.Callback = this;
            action.Start();
        }
    }

    // 在 Update 中执行当前动作
    public override void Update()
    {
        if (sequence.Count == 0) return;
        if (start < sequence.Count)
        {
            sequence[start].Update();
        }
    }

    // 执行完毕后销毁动作
    void OnDestroy()
    {
        foreach (SSAction action in sequence)
        {
            DestroyObject(action);
        }
    }

    // 更新当前执行的动作
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, object objectParam = null)
    {
        source.destroy = false;
        this.start++;
        if (this.start >= sequence.Count)
        {
            this.start = 0;
            if (repeat > 0) repeat--;
            if (repeat == 0)
            {
                this.destroy = true;
                this.Callback.SSActionEvent(this);
            }
        }
    }
}

public class SSActionManager: MonoBehaviour
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> waitingAdd = new List<SSAction>();
    private List<int> waitingDelete = new List<int>();

    protected void Update()
    {
        foreach (SSAction action in waitingAdd)
        {
            actions[action.GetInstanceID()] = action;
        }
        waitingAdd.Clear();

        foreach (KeyValuePair<int, SSAction> KeyValue in actions)
        {
            SSAction action = KeyValue.Value;
            if (action.destroy)
            {
                waitingDelete.Add(action.GetInstanceID()); // release action
            }
            else if (action.enable)
            {
                action.Update(); // update action
            }
        }

        foreach (int key in waitingDelete)
        {
            SSAction action = actions[key];
            actions.Remove(key);
            DestroyObject(action);
        }
        waitingDelete.Clear();
    }

    // 执行动作
    public void RunAction(GameObject gameObject, SSAction action, ISSActionCallback callback)
    {
        action.GameObject = gameObject;
        action.Transform = gameObject.transform;
        action.Callback = callback;
        waitingAdd.Add(action);
        action.Start();
    }

    protected void Start()
    {    }
}
