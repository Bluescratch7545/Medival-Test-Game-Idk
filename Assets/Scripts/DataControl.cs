using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DataControl : MonoBehaviour
{
    [SerializeReference]
    public List<IData> data = new List<IData>();

    public virtual void Awake()
    {
        InitAllValues();
    }

    public abstract void Update();

    public void SetData<T, TValue>(string Name, TValue Value) where T : class, IData
    {
        data.FirstOrDefault(x => x.Name == Name).ValSetTo(Value);
    }
    public IData GetData(string Name)
    {
        return data.FirstOrDefault(x => x.Name == Name);
    }

    public virtual void OnDestroy()
    {
        UninitAllValues();
    }

    public virtual void InitAllValues()
    {
        foreach (IData data in data)
        {
            data.InitValue();
        }
    }

    public virtual void UninitAllValues()
    {
        foreach(IData data in data)
        {
            data.UninitValue();
        }
    }
}