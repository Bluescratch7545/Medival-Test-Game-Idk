using System;
using UnityEngine;

public interface IData
{
    string Name { get; }
    void InitValue();
    void UninitValue();
    void ValSetTo(object value);
    object GetValue();
}

[Serializable]
public class INT : IData
{
    [SerializeField]
    private string name;
    public string Name => name;

    [SerializeField]
    private int val;

    [SerializeField]
    private int setTo;

    public int Val => val;
    public int SetTo { set => setTo = value; }

    public void InitValue()
    {
        val = setTo;
    }

    public void UninitValue()
    {
        val = 0;
    }

    public void ValSetTo(object value)
    {
        value = setTo;
        this.InitValue();
    }
    public object GetValue()
    {
        return val;
    }
}

[Serializable]
public class FLOAT : IData
{
    [SerializeField]
    private string name;
    public string Name => name;

    [SerializeField]
    private float val;

    [SerializeField]
    private float setTo;

    public float Val => val;
    public float SetTo { set => setTo = value; }

    public void InitValue()
    {
        val = setTo;
    }

    public void UninitValue()
    {
        val = 0;
    }
    public void ValSetTo(object value)
    {
        value = setTo;
        this.InitValue();
    }
    public object GetValue()
    {
        return val;
    }
}

[Serializable]
public class BOOL : IData
{
    [SerializeField]
    private string name;
    public string Name => name;

    [SerializeField]
    private bool val;

    [SerializeField]
    private bool setTo;

    public bool Val => val;
    public bool SetTo { set => setTo = value; }

    public void InitValue()
    {
        val = setTo;
    }

    public void UninitValue()
    {
        val = false;
    }
    public void ValSetTo(object value)
    {
        value = setTo;
        this.InitValue();
    }
    public object GetValue()
    {
        return val;
    }
}

[Serializable]
public class OBJ : IData
{
    [SerializeField]
    private string name;
    public string Name => name;

    [SerializeField] 
    private UnityEngine.Object val;

    [SerializeField] 
    private UnityEngine.Object setTo;

    public UnityEngine.Object Val => val;
    public UnityEngine.Object SetTo { set => setTo = value; }

    public void InitValue()
    {
        val = setTo;
    }

    public void UninitValue()
    {
        val = null;
    }
    public void ValSetTo(object value)
    {
        value = setTo;
        this.InitValue();
    }
    public object GetValue()
    {
        return val;
    }
}

// Make what you want with this idc
[Serializable]
public class REF<T> : IData where T : class
{
    public string Name { get; }

    [SerializeField] 
    private T val;

    [SerializeField] 
    private T setTo;

    public T Val => val;
    public T SetTo { set => setTo = value; }

    public void InitValue()
    {
        val = setTo;
    }

    public void UninitValue()
    {
        val = null;
    }
    public void ValSetTo(object value)
    {
        value = setTo;
        this.InitValue();
    }
    public object GetValue()
    {
        return val;
    }
}

[Serializable]
public class MONOBEH : IData
{
    [SerializeField]
    private string name;
    public string Name => name;

    [SerializeField]
    private MonoBehaviour val;

    [SerializeField]
    private MonoBehaviour setTo;

    public MonoBehaviour Val => val;
    public MonoBehaviour SetTo { set => setTo = value; }

    public void InitValue()
    {
        val = setTo;
    }

    public void UninitValue()
    {
        val = null;
    }
    public void ValSetTo(object value)
    {
        value = setTo;
        this.InitValue();
    }
    public object GetValue()
    {
        return val;
    }
}

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Try fixing it, i dare you
/// </remarks>
public class SERIALIZECLASS //: IData
{
    public string Name { get; }

    //[SerializeField]
    //private SERIALIZABLECLASS val;

    //[SerializeField]
    //private SERIALIZABLECLASS setTo;

    //public SERIALIZABLECLASS Val => val;
    //public SERIALIZABLECLASS SetTo { set => setTo = value; }

    public void InitValue()
    {
    //    val = setTo;
    }

    public void UninitValue()
    {
    //    val = null;
    }
    public void ValSetTo(object value)
    {
    //    value = setTo;
     //   this.InitValue();
    }
    /*public object GetValue()
    {
    //    return val;
    }*/
}