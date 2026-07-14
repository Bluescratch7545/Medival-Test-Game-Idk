using System.Collections.Generic;
using UnityEngine;

public class uFSM : MonoBehaviour
{
    public uFSMAsset asset;
    public string FsmName = "FSM";

    private uFSMState currentState;
    private uFSMContext ctx;

    Dictionary<string, GameObject> idMap = new Dictionary<string, GameObject>();
    void Awake()
    {
        if (asset == null || asset.states.Count == 0)
            return;

        ctx = new uFSMContext();
        ctx.owner = gameObject;

        EnterState(asset.states[0]);

        BuildMap();
    }

    public GameObject GetObject(string id)
    {
        idMap.TryGetValue(id, out var go);
        return go;
    }

    void Update()
    {
        if (currentState == null)
            return;

        foreach (var instance in currentState.actions)
        {
            if (instance == null || instance.action == null)
                continue;

            instance.action.OnUpdate(ctx);
        }
    }

    void EnterState(uFSMState state)
    {
        if (currentState != null)
        {
            foreach (var instance in currentState.actions)
            {
                if (instance?.action != null)
                    instance.action.OnExit(ctx);
            }
        }

        currentState = state;

        foreach (var instance in currentState.actions)
        {
            if (instance?.action != null)
                instance.action.OnEnter(ctx);
        }
    }

    void BuildMap()
    {
        idMap.Clear();

        var all = FindObjectsOfType<uFSMID>();

        foreach (var obj in all)
        {
            if (!string.IsNullOrEmpty(obj.ID))
            {
                idMap[obj.ID] = obj.gameObject;
            }
        }
    }

    public void SendEvent(string eventName)
    {
        if (currentState == null)
            return;

        foreach (var transition in currentState.transitions)
        {
            if (transition == null)
                continue;

            if (transition.eventName != eventName)
                continue;

            EnterState(transition.targetState);
            return;
        }
    }
}