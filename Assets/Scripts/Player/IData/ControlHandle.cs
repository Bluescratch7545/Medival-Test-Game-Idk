using System;
using System.Collections.Generic;
using System.Linq;

public interface IControlHandle
{
    string Name { get; }
    void Execute();
}

public static class ControlHandleRegistry
{
    private static readonly List<IControlHandle> registry = new List<IControlHandle>();

    public static void Register(IControlHandle handle) => registry.Add(handle);

    public static void ExecuteHandle(string handleName)
    {
        var found = registry.FirstOrDefault(h => h.Name == handleName);
        if (found == null)
        {
            UnityEngine.Debug.Log($"ControlHandle: no handle found named \"{handleName}\"");
            return;
        }
        found.Execute();
    }
}

public class ControlHandle : IControlHandle
{
    public string Name { get; }
    private readonly Action handle;

    public ControlHandle(string name, Action handle)
    {
        Name = name;
        this.handle = handle;
        ControlHandleRegistry.Register(this);
    }

    public void Execute() => handle();
}

public class ControlHandle<T1> : IControlHandle
{
    public string Name { get; }
    public T1 data1 { get; }
    private readonly Action<T1> handle;

    public ControlHandle(string name, T1 data1, Action<T1> handle)
    {
        Name = name;
        this.data1 = data1;
        this.handle = handle;
        ControlHandleRegistry.Register(this);
    }

    public void Execute() => handle(data1);
}

public class ControlHandle<T1, T2> : IControlHandle
{
    public string Name { get; }
    public T1 data1 { get; }
    public T2 data2 { get; }
    private readonly Action<T1, T2> handle;

    public ControlHandle(string name, T1 data1, T2 data2, Action<T1, T2> handle)
    {
        Name = name;
        this.data1 = data1;
        this.data2 = data2;
        this.handle = handle;
        ControlHandleRegistry.Register(this);
    }

    public void Execute() => handle(data1, data2);
}

public class ControlHandle<T1, T2, T3> : IControlHandle
{
    public string Name { get; }
    public T1 data1 { get; }
    public T2 data2 { get; }
    public T3 data3 { get; }
    private readonly Action<T1, T2, T3> handle;

    public ControlHandle(string name, T1 data1, T2 data2, T3 data3, Action<T1, T2, T3> handle)
    {
        Name = name;
        this.data1 = data1;
        this.data2 = data2;
        this.data3 = data3;
        this.handle = handle;
        ControlHandleRegistry.Register(this);
    }

    public void Execute() => handle(data1, data2, data3);
}

public class ControlHandle<T1, T2, T3, T4> : IControlHandle
{
    public string Name { get; }
    public T1 data1 { get; }
    public T2 data2 { get; }
    public T3 data3 { get; }
    public T4 data4 { get; }
    private readonly Action<T1, T2, T3, T4> handle;

    public ControlHandle(string name, T1 data1, T2 data2, T3 data3, T4 data4, Action<T1, T2, T3, T4> handle)
    {
        Name = name;
        this.data1 = data1;
        this.data2 = data2;
        this.data3 = data3;
        this.data4 = data4;
        this.handle = handle;
        ControlHandleRegistry.Register(this);
    }

    public void Execute() => handle(data1, data2, data3, data4);
}

public class ControlHandle<T1, T2, T3, T4, T5> : IControlHandle
{
    public string Name { get; }
    public T1 data1 { get; }
    public T2 data2 { get; }
    public T3 data3 { get; }
    public T4 data4 { get; }
    public T5 data5 { get; }
    private readonly Action<T1, T2, T3, T4, T5> handle;

    public ControlHandle(string name, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, Action<T1, T2, T3, T4, T5> handle)
    {
        Name = name;
        this.data1 = data1;
        this.data2 = data2;
        this.data3 = data3;
        this.data4 = data4;
        this.data5 = data5;
        this.handle = handle;
        ControlHandleRegistry.Register(this);
    }

    public void Execute() => handle(data1, data2, data3, data4, data5);
}

public class ControlHandle<T1, T2, T3, T4, T5, T6> : IControlHandle
{
    public string Name { get; }
    public T1 data1 { get; }
    public T2 data2 { get; }
    public T3 data3 { get; }
    public T4 data4 { get; }
    public T5 data5 { get; }
    public T6 data6 { get; }
    private readonly Action<T1, T2, T3, T4, T5, T6> handle;

    public ControlHandle(string name, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, Action<T1, T2, T3, T4, T5, T6> handle)
    {
        Name = name;
        this.data1 = data1;
        this.data2 = data2;
        this.data3 = data3;
        this.data4 = data4;
        this.data5 = data5;
        this.data6 = data6;
        this.handle = handle;
        ControlHandleRegistry.Register(this);
    }

    public void Execute() => handle(data1, data2, data3, data4, data5, data6);
}

public class ControlHandle<T1, T2, T3, T4, T5, T6, T7> : IControlHandle
{
    public string Name { get; }
    public T1 data1 { get; }
    public T2 data2 { get; }
    public T3 data3 { get; }
    public T4 data4 { get; }
    public T5 data5 { get; }
    public T6 data6 { get; }
    public T7 data7 { get; }
    private readonly Action<T1, T2, T3, T4, T5, T6, T7> handle;

    public ControlHandle(string name, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7, Action<T1, T2, T3, T4, T5, T6, T7> handle)
    {
        Name = name;
        this.data1 = data1;
        this.data2 = data2;
        this.data3 = data3;
        this.data4 = data4;
        this.data5 = data5;
        this.data6 = data6;
        this.data7 = data7;
        this.handle = handle;
        ControlHandleRegistry.Register(this);
    }

    public void Execute() => handle(data1, data2, data3, data4, data5, data6, data7);
}

public class ControlHandle<T1, T2, T3, T4, T5, T6, T7, T8> : IControlHandle
{
    public string Name { get; }
    public T1 data1 { get; }
    public T2 data2 { get; }
    public T3 data3 { get; }
    public T4 data4 { get; }
    public T5 data5 { get; }
    public T6 data6 { get; }
    public T7 data7 { get; }
    public T8 data8 { get; }
    private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8> handle;

    public ControlHandle(string name, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7, T8 data8, Action<T1, T2, T3, T4, T5, T6, T7, T8> handle)
    {
        Name = name;
        this.data1 = data1;
        this.data2 = data2;
        this.data3 = data3;
        this.data4 = data4;
        this.data5 = data5;
        this.data6 = data6;
        this.data7 = data7;
        this.data8 = data8;
        this.handle = handle;
        ControlHandleRegistry.Register(this);
    }

    public void Execute() => handle(data1, data2, data3, data4, data5, data6, data7, data8);
}

public class ControlHandle<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IControlHandle
{
    public string Name { get; }
    public T1 data1 { get; }
    public T2 data2 { get; }
    public T3 data3 { get; }
    public T4 data4 { get; }
    public T5 data5 { get; }
    public T6 data6 { get; }
    public T7 data7 { get; }
    public T8 data8 { get; }
    public T9 data9 { get; }
    private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> handle;

    public ControlHandle(string name, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7, T8 data8, T9 data9, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> handle)
    {
        Name = name;
        this.data1 = data1;
        this.data2 = data2;
        this.data3 = data3;
        this.data4 = data4;
        this.data5 = data5;
        this.data6 = data6;
        this.data7 = data7;
        this.data8 = data8;
        this.data9 = data9;
        this.handle = handle;
        ControlHandleRegistry.Register(this);
    }

    public void Execute() => handle(data1, data2, data3, data4, data5, data6, data7, data8, data9);
}

public class ControlHandle<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IControlHandle
{
    public string Name { get; }
    public T1 data1 { get; }
    public T2 data2 { get; }
    public T3 data3 { get; }
    public T4 data4 { get; }
    public T5 data5 { get; }
    public T6 data6 { get; }
    public T7 data7 { get; }
    public T8 data8 { get; }
    public T9 data9 { get; }
    public T10 data10 { get; }
    private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> handle;

    public ControlHandle(string name, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7, T8 data8, T9 data9, T10 data10, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> handle)
    {
        Name = name;
        this.data1 = data1;
        this.data2 = data2;
        this.data3 = data3;
        this.data4 = data4;
        this.data5 = data5;
        this.data6 = data6;
        this.data7 = data7;
        this.data8 = data8;
        this.data9 = data9;
        this.data10 = data10;
        this.handle = handle;
        ControlHandleRegistry.Register(this);
    }

    public void Execute() => handle(data1, data2, data3, data4, data5, data6, data7, data8, data9, data10);
}