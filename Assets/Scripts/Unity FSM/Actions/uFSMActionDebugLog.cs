using UnityEngine;

[CreateAssetMenu(menuName = "Unity FSM/Actions/Debug Log")]
public class uFSMActionDebugLog : uFSMAction
{
    public uFSMValue message = new uFSMValue() { stringValue = "Hello FSM" };

    public override void OnEnter(uFSMContext ctx)
    {
        Debug.Log("[FSM ENTER] " + message);
    }

    public override void OnUpdate(uFSMContext ctx)
    {
        Debug.Log("[FSM UPDATE] " + message);
    }
}