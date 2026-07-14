using System;

[Serializable]
public class uFSMTransition
{
    public string eventName;
    public uFSMState targetState;
    public string targetGuid;
}