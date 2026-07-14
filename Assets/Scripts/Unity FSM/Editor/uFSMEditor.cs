using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class uFSMEditor : EditorWindow
{
    public uFSMAsset asset;

    private Vector2 drag;
    private bool draggingCanvas;

    private uFSMState connectingFrom;
    private bool creatingTransition;

    Dictionary<uFSMAction, UnityEditor.Editor> actionEditors = new Dictionary<uFSMAction, UnityEditor.Editor>();

    private Vector2 actionScroll;
    private bool showActionBrowser;
    private System.Type[] actionTypes;

    private uFSMState selected;

    [MenuItem("Tools/Unity FSM Editor")]
    public static void Open()
    {
        GetWindow<uFSMEditor>("Unity FSM");
    }

    void OnGUI()
    {
        asset = (uFSMAsset)EditorGUILayout.ObjectField("FSM Asset", asset, typeof(uFSMAsset),  false);

        if (asset == null)
            return;

        foreach (var state in asset.states)
        {
            if (string.IsNullOrEmpty(state.guid))
            {
                state.guid = Guid.NewGuid().ToString();
                EditorUtility.SetDirty(asset);
            }
        }

        DrawConnections();
        DrawPendingConnection();
        DrawToolbar();
        DrawStates();
        DrawInspector();

        ProcessEvents(Event.current);

        if (GUI.changed) 
        {
            Repaint();
            EditorUtility.SetDirty(asset);
            //
            //EditorUtility.SetDirty(selected);
        };
    }

    void DrawConnections()
    {
        if (asset == null || asset.states == null)
            return;

        Handles.BeginGUI();

        foreach (var state in asset.states)
        {
            if (state == null || state.transitions == null)
                continue;

            foreach (var t in state.transitions)
            {
                if (t == null || t.targetState == null)
                    continue;

                uFSMState target = asset.states.Find(s => s.guid == t.targetGuid);

                if (target != null)
                {
                    DrawBezier(state.rect, target.rect);
                }
            }
        }

        Handles.EndGUI();
    }

    void DrawBezier(Rect from, Rect to)
    {
        Vector3 startPos = new Vector3(from.xMax, from.center.y);
        Vector3 endPos = new Vector3(to.xMin, to.center.y);

        Vector3 startTan = startPos + Vector3.right * 50f;
        Vector3 endTan = endPos + Vector3.left * 50f;

        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.yellow, null, 2f);
    }
    void DrawToolbar()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Create New State"))
        {
            asset.states.Add(new uFSMState
            {
                name = "State " + asset.states.Count,
                guid = Guid.NewGuid().ToString(),
                rect = new Rect(200, 200, 160, 80)
            });
        }

        GUILayout.EndHorizontal();

    }

    void DrawStates()
    {
        foreach (var state in asset.states)
        {
            state.rect.position += drag;

            GUI.Box(state.rect, state.name);

            Rect outputHandle = new Rect(
                state.rect.xMax - 10,
                state.rect.center.y - 5,
                10,
                10
            );

            GUI.Box(outputHandle, ">");

            if (Event.current.type == EventType.MouseDown)
            {
                if (state.rect.Contains(Event.current.mousePosition))
                {
                    selected = state;
                }

                if (outputHandle.Contains(Event.current.mousePosition))
                {
                    connectingFrom = state;
                    creatingTransition = true;
                    Event.current.Use();
                }
            }

            if (Event.current.type == EventType.MouseDrag &&
                state.rect.Contains(Event.current.mousePosition))
            {
                state.rect.position += Event.current.delta;
                Event.current.Use();
            }
        }
    }

    void DrawPendingConnection()
    {
        if (!creatingTransition || connectingFrom == null)
            return;

        Handles.BeginGUI();

        Vector3 startPos = new Vector3(
            connectingFrom.rect.xMax,
            connectingFrom.rect.center.y
        );

        Vector3 endPos = Event.current.mousePosition;

        Handles.DrawBezier(
            startPos,
            endPos,
            startPos + Vector3.right * 50,
            endPos + Vector3.left * 50,
            Color.yellow,
            null,
            2f
        );

        Handles.EndGUI();

        Repaint();
    }

    void DrawInspector()
    {
        GUILayout.BeginArea(new Rect(position.width - 250, 0, 250, position.height), GUI.skin.window);

        if (selected == null)
        {
            GUILayout.Label("No State Selected");
            GUILayout.EndArea();
            return;
        }

        GUILayout.Label("STATE: " + selected.name);

        selected.name = EditorGUILayout.TextField("Name", selected.name);

        GUILayout.Space(10);

        DrawActions();

        DrawTransitions();

        GUILayout.EndArea();
    }
    void DrawActions()
    {
        GUILayout.Label("Actions:");

        if (GUILayout.Button("+ Add Action"))
            showActionBrowser = true;

        actionScroll = GUILayout.BeginScrollView(actionScroll, GUILayout.Height(200));

        for (int i = 0; i < selected.actions.Count; i++)
        {

            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal();
            var instance = selected.actions[i];
            var action = instance.action;

            var fields = action.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            GUILayout.EndHorizontal();

            foreach (var f in fields)
            {
                var fieldValue = f.GetValue(action);

                if (f.FieldType == typeof(uFSMValue))
                {
                    uFSMValue val = fieldValue as uFSMValue;

                    if (val == null)
                    {
                        val = new uFSMValue();
                    }

                    val = (uFSMValue)uFSMFieldDrawers.DrawValue(f.Name, val);

                    f.SetValue(action, val);
                }
                else
                {
                    object value = fieldValue;

                    value = uFSMFieldDrawers.DrawField(
                        f.Name,
                        f.FieldType,
                        value
                    );

                    f.SetValue(action, value);
                }
            }
            GUILayout.EndVertical();
        }

        GUILayout.EndScrollView();

        if (showActionBrowser)
            DrawActionBrowser();
    }

    void DrawTransitions()
    {
        GUILayout.Space(10);
        GUILayout.Label("Transitions:");

        if (GUILayout.Button("+ Add Transition"))
        {
            if (selected != null)
            {
                selected.transitions.Add(new uFSMTransition
                {
                    eventName = "NewEvent",
                    targetState = null
                });
            }
        }

        for (int i = 0; i < selected.transitions.Count; i++)
        {
            var t = selected.transitions[i];
            GUILayout.BeginHorizontal();

            t.eventName = GUILayout.TextField(t.eventName);

            string[] stateNames = asset.states
                .ConvertAll(s => s.name)
                .ToArray();

            int index = asset.states.FindIndex(s => s.guid == t.targetGuid);

            index = EditorGUILayout.Popup(index, stateNames);

            if (index >= 0)
            {
                t.targetGuid = asset.states[index].guid;
            }

            GUILayout.EndHorizontal();
        }
    }
    string GetCleanTypeName(string typeName)
    {
        if (string.IsNullOrEmpty(typeName))
            return "Missing Action";

        var type = System.Type.GetType(typeName);

        if (type == null)
            return "Unknown Action";

        return type.Name;
    }
    void DrawActionBrowser()
    {
        GUILayout.BeginArea(new Rect(20, 20, 300, 400), GUI.skin.window);

        GUILayout.Label("Add Action");

        if (actionTypes == null)
            actionTypes = GetAllActionTypes();

        for (int i = 0; i < actionTypes.Length; i++)
        {
            var t = actionTypes[i];

            if (GUILayout.Button(t.Name))
            {
                var action = (uFSMAction)Activator.CreateInstance(t);

                selected.actions.Add(new uFSMActionInstance
                {
                    action = action
                });

                showActionBrowser = false;

                GUI.FocusControl(null);
                Repaint();

                break;
            }
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Close"))
            showActionBrowser = false;

        GUILayout.EndArea();
    }
    System.Type[] GetAllActionTypes()
    {
        var list = new System.Collections.Generic.List<System.Type>();

        foreach (var asm in System.AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var t in asm.GetTypes())
            {
                if (t.IsSubclassOf(typeof(uFSMAction)) && !t.IsAbstract)
                {
                    list.Add(t);
                }
            }
        }

        return list.ToArray();
    }

    void ProcessEvents(Event e)
    {
        if (e.type == EventType.MouseDrag && e.button == 2)
        {
            drag += e.delta;
            e.Use();
        }
        if (creatingTransition && e.type == EventType.MouseUp)
        {
            foreach (var state in asset.states)
            {
                if (state == connectingFrom)
                    continue;

                if (state.rect.Contains(e.mousePosition))
                {
                    connectingFrom.transitions.Add(new uFSMTransition
                    {
                        eventName = "NewEvent",
                        targetGuid = state.guid
                    });

                    break;
                }
            }

            connectingFrom = null;
            creatingTransition = false;

            e.Use();
        }
    }
}