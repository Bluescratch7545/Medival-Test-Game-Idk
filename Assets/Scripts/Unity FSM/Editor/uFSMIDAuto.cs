#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System;

public class uFSMIdAuto : MonoBehaviour
{
    public string id;

    void Awake()
    {
        if (string.IsNullOrEmpty(id))
            id = Guid.NewGuid().ToString();
    }

#if UNITY_EDITOR
    [ContextMenu("Generate New ID")]
    void Generate()
    {
        id = Guid.NewGuid().ToString();
        EditorUtility.SetDirty(this);
    }
#endif
}