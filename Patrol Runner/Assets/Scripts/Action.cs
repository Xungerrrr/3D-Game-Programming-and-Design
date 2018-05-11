using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : ScriptableObject
{
    public bool enable = true;
    public bool destroy = false;

    public GameObject gameObject { get; set; }
    public Transform transform { get; set; }
    public ActionCallback callback { get; set; }

    protected Action() { }

    // Use this for initialization
    public virtual void Start() {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    public virtual void Update() {
        throw new System.NotImplementedException();
    }

    public void reset() {
        enable = false;
        destroy = false;
        gameObject = null;
        transform = null;
        callback = null;
    }
}
