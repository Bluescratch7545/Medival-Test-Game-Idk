using UnityEngine;

[System.Serializable]
public abstract class uFSMAction
{
    public virtual void OnEnter(uFSMContext ctx) { }
    public virtual void OnUpdate(uFSMContext ctx) { }
    public virtual void OnExit(uFSMContext ctx) { }
}