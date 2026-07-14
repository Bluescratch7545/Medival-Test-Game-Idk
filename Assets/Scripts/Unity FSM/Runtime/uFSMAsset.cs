using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unity FSM/FSM Asset")]
public class uFSMAsset : ScriptableObject
{
    public List<uFSMState> states = new List<uFSMState>();
}