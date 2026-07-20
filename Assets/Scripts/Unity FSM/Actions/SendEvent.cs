using UnityEngine;

public class SendEvent : uFSMAction
{
    public string eventName;

    public override void OnEnter(uFSMContext ctx)
    {
        ctx.owner.GetComponent<uFSM>().SendEvent(eventName);
    }
}