using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class uFSMState
{
    public string name;
    public Rect rect;
    public string guid;

    public List<uFSMTransition> transitions = new List<uFSMTransition>();

    public List<uFSMActionInstance> actions = new List<uFSMActionInstance>();
}